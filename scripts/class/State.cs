using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class State : Node
{
    public State lastState;
    public Player player;
    public Player.State index;
    public float speed;
    public float acceleration;
    public State currentState;
    public Dictionary<Player.State,State> states = new Dictionary<Player.State, State>();
    public override void _Ready()
    {
        base._Ready();
        if(GetChildren().Count != 0)
        {
            foreach(State state in GetChildren())
            {
                states[state.index] = state;
            }
        }
        else
        {
            currentState = this;
        }
        player = Owner as Player;
    }

    [Signal]
    public delegate void TransitionRequestedEventHandler(int state);

    public virtual void Enter(){}
    public virtual void PhysicUpdate(double delta)
    {
        player.stats.speed = speed;
        player.stats.acceleration = acceleration;
    }
    public virtual void Exit()
    {
        if(lastState != null)
            lastState.currentState = null;
        lastState = null;
    }
    public virtual void Transition(){}

    public virtual State GetBaseState()
    {
        return currentState;
    }
}
