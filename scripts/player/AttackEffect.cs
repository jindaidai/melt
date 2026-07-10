using Godot;
using System;

public partial class AttackEffect : AnimatedSprite2D
{
    public override void _Ready()
    {
        base._Ready();
        Visible = false;
        
        Connect(SignalName.AnimationFinished,new Callable(this,nameof(OnFinished)));
    }
    public void PlayFireHit1()
    {
        Visible = true;
        Play("fire_hit_1");
    }
    public void PlayFireHit2()
    {
        Visible = true;
        Play("fire_hit_2");
    }
    public void PlayFireHit3()
    {
        Visible = true;
        Play("fire_hit_3");
    }

    public void OnFinished()
    {
        Visible = false;
    }
}
