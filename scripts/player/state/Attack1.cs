using Godot;
using System;

public partial class Attack1 : State
{
    Attack1()
    {
        index = Player.State.Attack1;
        speed = 0.0f;
        acceleration = 500.0f;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("attack1");
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
    }
}
