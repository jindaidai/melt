using Godot;
using System;
using System.Threading.Tasks;

public partial class Idle : PlayerState
{
    public Idle()
    {
        index = Player.State.Idle;
        speed = 0.0f;
        acceleration = 1000.0f;
    }

    public override async void Enter()
    {
        base.Enter();
        player.PlayAnimation("idle");
    }

    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
    }

}
