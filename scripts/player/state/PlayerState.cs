using Godot;
using System;
using System.Collections.Generic;
public partial class PlayerState : State
{
    public PlayerState lastState;
    public Player player;
    public Player.State index;
    public PlayerState currentState;
    public Dictionary<Player.State,PlayerState> states = new Dictionary<Player.State, PlayerState>();

    public override void _Ready()
    {
        base._Ready();
        if(GetChildren().Count != 0)
        {
            foreach(PlayerState state in GetChildren())
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
    public override void PhysicUpdate(double delta)
    {
        base.PhysicUpdate(delta);
        player.stats.speed = speed;
        player.stats.acceleration = acceleration;
    }
    public override void Exit()
    {
        if(lastState != null)
            lastState.currentState = null;
        lastState = null;
    }
    public virtual PlayerState GetBaseState()
    {
        return currentState;
    }
}
