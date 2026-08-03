using Godot;
using System;
using System.Collections.Generic;

public partial class DevilState : State
{
    public DevilState lastState;
    public Devil devil;
    public Devil.State index;
    public DevilState currentState;
    public Dictionary<Devil.State,DevilState> states = new Dictionary<Devil.State, DevilState>();
     public virtual DevilState GetBaseState()
    {
        return currentState;
    }
}
