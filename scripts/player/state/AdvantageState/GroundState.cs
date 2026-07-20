using Godot;
using System;

public partial class GroundState : State
{
    bool wasOnFloor;
    bool WasOnfloor
    {
        get
        {
            return wasOnFloor;
        }
        set
        {
            if(wasOnFloor != value)
            {
                if(wasOnFloor == true && currentState.index != Player.State.Jump)
                {
                    player.timers.StartTimer(Player.TimerType.Coyote);
                }
                wasOnFloor = value;
            }
        }
    }
    
    GroundState()
    {
        index = Player.State.Ground;
    }

    public override async void Enter()
    {
        base.Enter();
        player.currentJumpCount = 0;
        player.stats.fallSpeed = Player.MaxFallSpeed;
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
        //Jump状态在Ground只进入不更新
        if(currentState != states[Player.State.Jump])
        {
            currentState?.PhysicUpdate(delta);
        }
        Transition();

    }

    public State GetBaseState()
    {
        float getDirection = Input.GetAxis("move_left","move_right");
        bool isRunning = (getDirection != 0.0f);
        bool isIdle = (getDirection == 0.0f);
        bool isJump = Input.IsActionJustPressed("jump")||player.Velocity.Y < 0;
        //优先jump
        if(isJump)
        {
            return states[Player.State.Jump];
        }
        if(isIdle)
        {
            return states[Player.State.Idle];
        }
        if (isRunning)
        {
            return states[Player.State.Running];
        }
        return currentState;
    }

    public override void Transition()
    {   
        base.Transition();
        WasOnfloor = player.IsOnFloor();
        bool isSprint = Input.IsActionJustPressed("sprint");
        bool isHit = Input.IsActionPressed("hit");
        bool isAir = !player.IsOnFloor() && player.timers.IsTimerStopped(Player.TimerType.Coyote);
        if (isHit)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Attack);
        }
        else if (isSprint)
        {
            EmitSignal(State.SignalName.TransitionRequested,(int)Player.State.Sprint);
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


    public override void Exit()
    {
        base.Exit();
    }
}
