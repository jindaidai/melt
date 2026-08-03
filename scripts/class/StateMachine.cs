using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[GlobalClass]
public partial class StateMachine : Node
{
    [Export]
    PlayerState initState;
    public PlayerState currentState;
    Player player;


    Dictionary<Player.State,PlayerState> states = new Dictionary<Player.State, PlayerState>();
    public override async void _Ready()
    {
        base._Ready();
        player = Owner as Player;
        await ToSignal(player,Node.SignalName.Ready);
        foreach(PlayerState state in GetChildren())
        {
            states[state.index] = state;
            state.Connect(State.SignalName.TransitionRequested,new Callable(this,nameof(OnTransitionRequest)));
        }

        if(initState != null)
        {
            initState.Enter();
            currentState = initState;
        }
    }

    public void PhysicUpdate(double delta)
    {
        
    }
    
    public void OnTransitionRequest(int index)
    {
        Player.State key = (Player.State)index;
        if(!states.ContainsKey(key)||key == Player.State.Keep)
        {
            return;
        }
        currentState?.Exit();
        states[key].lastState = currentState;
        states[key]?.Enter();
        currentState = states[key];
        GD.Print("进入",currentState.Name);
    }
}
