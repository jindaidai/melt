using Godot;
using System;

public partial class AttackState : State
{
    public int comboCount;
    AttackState()
    {
        index = Player.State.Attack;
    }

    public override async void _Ready()
    {
        base._Ready();
        await ToSignal(player,Node.SignalName.Ready);
        player.timers.ConnectTimer(Player.TimerType.HitContinue,new Callable(this,nameof(OnHitContinueTimeout)));
    }
    public override async void Enter()
    {
        base.Enter();
        comboCount += 1;
        player.timers.StartTimer(Player.TimerType.HitContinue);
        player.stats.fallSpeed = 0;
        player.stats.fallAcceleration = 1000;
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
        if(GetChildren().Count != 0)
        {   
            if(currentState != GetBaseState())
            {
                currentState?.Exit();
                currentState = GetBaseState();
                currentState?.Enter();
            }
        }
        currentState?.PhysicUpdate(delta);
        Transition();

    }

    public State GetBaseState()
    {
        int hitMode = comboCount % 3;
        switch (hitMode)
        {
            case 1:
                return states[Player.State.Attack1];
            case 2:
                return states[Player.State.Attack2];
            case 0:
                return states[Player.State.Attack3];
            
        }

        return currentState;
    }

    public override void Transition()
    {   
        base.Transition();
        bool isHit = Input.IsActionPressed("hit");
        bool isAir = !player.IsOnFloor() && player.timers.IsTimerStopped(Player.TimerType.Coyote);
        bool isGround = player.IsOnFloor();
        if (!player.IsAnimationPlaying())
        {
            if (isAir)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Air);
            }
            else if (isHit)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Attack);
            }
            else if (isGround)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Ground);
            }
            else
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Keep);
            }
        }
    }


    public void OnHitContinueTimeout()
    {
        comboCount = 0;
    }
    public override void Exit()
    {
        base.Exit();
        player.hitCollision.Disabled = true;
    }
}
