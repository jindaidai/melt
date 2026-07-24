using Godot;
using System;

public partial class Transform : State
{
    Transform()
    {
        index = Player.State.Transform;
        speed = 0.0f;
        acceleration = 1000.0f;
    }

    public override async void Enter()
    {
        base.Enter();
        currentState = this;
        player.PlayAnimation("transform");  
        player.stats.fallSpeed = Player.MaxFallSpeed;
        player.stats.fallAcceleration = 1000;
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
        Transition();
    }

    public override void Transition()
    {   
        base.Transition();
        bool isHit = Input.IsActionPressed("hit");
        bool isGround = player.IsOnFloor();
         bool isAir = !player.IsOnFloor() && player.timers.IsTimerStopped(Player.TimerType.Coyote);
        if (!player.IsAnimationPlaying())
        { 
            if (isHit)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Attack);
            }
            else if (isGround)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Ground);
            }
            else if (isAir)
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Air);
            }
            else
            {
                EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Keep);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        if(player.CurrentType == Player.PlayerType.Fire)
            player.CurrentType = Player.PlayerType.Ice;
        else if(player.CurrentType == Player.PlayerType.Ice)
            player.CurrentType = Player.PlayerType.Fire;
    }
}

