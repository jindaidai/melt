using Godot;
using System;

public partial class Fall : State
{
    public Fall()
    {
        index = Player.State.Fall;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("fall");
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
    }
}
