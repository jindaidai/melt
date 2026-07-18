using Godot;
using System;

public partial class GrassBreakEffect : GpuParticles2D,IPoolable
{
    private GrassBreakPool _pool;

    public void SetPool(GrassBreakPool pool)
    {
        _pool = pool;
    }
    public void Activate()
    {
        Visible = true;
        ProcessMode = ProcessModeEnum.Inherit;

        Restart();
        Emitting = true;
    }

    public void Deactivate()
    {
        Emitting = false;
        Visible = false;
        ProcessMode = ProcessModeEnum.Disabled;
    }
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
        _pool.ReturnObject(this);
    }
}
