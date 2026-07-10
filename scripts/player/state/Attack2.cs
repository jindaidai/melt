using Godot;
using System;

public partial class Attack2 : State
{
    Attack2()
    {
        index = Player.State.Attack2;
        speed = 0.0f;
        acceleration = 500.0f;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("attack2");
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
    }
}
