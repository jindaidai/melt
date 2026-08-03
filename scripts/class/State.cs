using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class State : Node
{
    
    public float speed;
    public float acceleration;
    

    [Signal]
    public delegate void TransitionRequestedEventHandler(int state);

    public virtual void Enter(){}
    public virtual void PhysicUpdate(double delta)
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void Transition(){}

}
