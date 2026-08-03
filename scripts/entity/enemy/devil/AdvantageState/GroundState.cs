using Godot;
using System;
namespace Game.DevilStates;
public partial class GroundState : DevilState
{
    GroundState()
    {
        index = Devil.State.Ground;
    }

    public override async void Enter()
    {
        base.Enter();
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
        Transition();

    }

    public override DevilState GetBaseState()
    {
        return currentState;
    }

    public override void Transition()
    {   
        base.Transition();
    }


    public override void Exit()
    {
        base.Exit();
    }
}
