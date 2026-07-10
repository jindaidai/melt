using Godot;
using System;

public partial class AirJump : State
{
    public AirJump()
    {
        index = Player.State.AirJump;
    }

    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount += 1;
        player.PlayAnimation("airJump");
        player.Velocity = new Vector2(player.Velocity.X,-300);
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
        float getDirection = Input.GetAxis("move_left","move_right");
        if(getDirection != 0)
        {
            player.Direction = getDirection;
            speed = 100.0f;
            acceleration = 1000.0f;
        }
        else
        {
            speed = 0.0f;
            acceleration = 500.0f;
        }
        
        if (Input.IsActionJustReleased("jump"))
        {
            if(player.Velocity.Y < -150)
            {
                player.Velocity = new Vector2(player.Velocity.X,-150);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
