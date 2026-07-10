using Godot;
using System.Collections.Generic;
using System;

public partial class Timers : Node
{
    public Player player;
    public Timer coyoteTimer;
    public Timer hitContinueTimer;
    private Dictionary<Player.TimerType,Timer> timers;
    public override void _Ready()
    {

        base._Ready();
        player = Owner as Player;
        timers = new Dictionary<Player.TimerType, Timer>
        {
            { Player.TimerType.Coyote,GetNode<Timer>("CoyoteTimer") },
            { Player.TimerType.HitContinue,GetNode<Timer>("HitContinueTimer") },
        };
    }
    public void StartTimer(Player.TimerType type)
    {
        timers[type].Start();
    }

    public void StopTimer(Player.TimerType type)
    {
        timers[type].Stop();
    }

    public bool IsTimerStopped(Player.TimerType type)
    {
        return timers[type].IsStopped();
    }
    public void ConnectTimer(Player.TimerType type,Callable callback)
    {
        timers[type].Connect(Timer.SignalName.Timeout,callback);
    }
}
