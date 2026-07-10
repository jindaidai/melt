using Godot;
using System;

public partial class GrassBreakEffect : GpuParticles2D
{
    public override void _Ready()
    {
        base._Ready();
        OneShot = true;
        Connect(SignalName.Finished,new Callable(this,nameof(OnFinished)));
        Restart();
        Emitting = true;
    }

    public void OnFinished()
    {
        QueueFree();
    }
}
