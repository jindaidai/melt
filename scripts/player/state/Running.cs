using Godot;
using System;

public partial class Running : State
{
    public Running()
    {
        index = Player.State.Running;
        speed = 100.0f;
        acceleration = 400.0f;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("running");
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
        float getDirection = Input.GetAxis("move_left","move_right");
        if(getDirection != 0)
        {
            player.Direction = getDirection;
        }
        
    }

}
