using Godot;
using System;

public partial class AirState : PlayerState
{
    AirState()
    {
        index = Player.State.Air;
    }

    public override async void Enter()
    {
        
        base.Enter();
        player.stats.fallSpeed = Player.MaxFallSpeed;
        player.stats.fallAcceleration = 1000;
        if(lastState.currentState.index == Player.State.Jump)
        {
            currentState = states[Player.State.Jump];
        }
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
                //Jump在Air状态不进入只更新
                if(currentState != states[Player.State.Jump])
                {
                    currentState?.Enter();
                }
            }
        }
        currentState?.PhysicUpdate(delta);
        Transition();

    }

    public override PlayerState GetBaseState()
    {
        bool isAirJump = Input.IsActionJustPressed("jump") && player.currentJumpCount < player.maxJumpCount;
        bool isFall = player.Velocity.Y > 0;
        if(isAirJump)
        {
            return states[Player.State.AirJump];
        }
        if (isFall)
        {
            if(currentState == states[Player.State.AirJump]&&player.IsAnimationPlaying())
            {
                return currentState;
            }
        
            return states[Player.State.Fall];
        }
        return currentState;
    }

    public override void Transition()
    {   
        base.Transition();
        bool isSkill = Input.IsActionJustPressed("skill");
        bool isHit = Input.IsActionPressed("hit");
        bool isGround = player.IsOnFloor();
        bool isTransform = Input.IsActionJustPressed("transform");
        if (isTransform)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Transform);
        }
        else if (isHit)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Attack);
        }
        else if (isGround)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Ground);
        }
        else if (isSkill)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Skill);
        }
        
        else
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Keep);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}


