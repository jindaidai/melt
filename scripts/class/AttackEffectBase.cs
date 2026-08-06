using Godot;
using System;
[GlobalClass]
public partial class AttackEffectBase : AnimatedSprite2D,IPoolable
{
    Vector2 position = Vector2.Zero;
    float direction = 1;
    public float Direction
    {
        get
        {
            return direction;
        }
        set
        {
            Scale = new Vector2(value, 1);
            direction = value;
        }
    }
    public override void _Ready()
    {
        base._Ready();
        Visible = false;
        
        Connect(SignalName.AnimationFinished,new Callable(this,nameof(OnFinished)));
    }
    
    public void Activate()
    {
        Visible = true;
        ProcessMode = ProcessModeEnum.Inherit;

    }

    public void Deactivate()
    {
        Visible = false;
        ProcessMode = ProcessModeEnum.Disabled;
    }
    public void PlayAnimation(string name)
    {
        if (SpriteFrames.HasAnimation(name))
        {
            Play(name);
        }
    }

    public void StopAnimation()
    {
        Stop();
    }
    public virtual void OnFinished()
    {
        Deactivate();
    }
}
