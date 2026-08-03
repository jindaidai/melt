using Godot;
using System;

public partial class AttackState : PlayerState
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
        player.stats.fallSpeed = 0;
        player.stats.fallAcceleration = 1000;
        comboCount += 1;
        player.timers.StartTimer(Player.TimerType.HitContinue);
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
                GD.Print("进入"+currentState.Name);
            }
        }
        currentState?.PhysicUpdate(delta);
        Transition();

    }

    public override PlayerState GetBaseState()
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
        bool isSkill = Input.IsActionJustPressed("skill");
        bool isHit = Input.IsActionPressed("hit");
        bool isAir = !player.IsOnFloor() && player.timers.IsTimerStopped(Player.TimerType.Coyote);
        bool isGround = player.IsOnFloor();
        bool isTransform = Input.IsActionJustPressed("transform");
        if (isTransform)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Transform);
        }
       else if (isSkill)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Skill);
        }
        
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
