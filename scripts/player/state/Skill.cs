using Godot;
using System;

public partial class Skill : State
{
    public Skill()
    {
        index = Player.State.Skill;
        speed = 240.0f;
        acceleration = 600.0f;
    }

    public override void Enter()
    {
        base.Enter();
        currentState = this;
        player.PlayAnimation("skill");
        player.stats.fallSpeed = 0;
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
}
