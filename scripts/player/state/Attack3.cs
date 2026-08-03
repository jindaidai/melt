using Godot;
using System;

public partial class Attack3 : PlayerState
{
    Attack3()
    {
        index = Player.State.Attack3;
        speed = 0.0f;
        acceleration = 500.0f;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayAnimation("attack3");
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
    }
}
