using Godot;
using System;

public partial class AttackEffect : AnimatedSprite2D
{
    Player player;
    public override void _Ready()
    {
        base._Ready();
        Visible = false;
        player = Owner as Player;
        
        Connect(SignalName.AnimationFinished,new Callable(this,nameof(OnFinished)));
    }

    public void PlayAnimation(string name)
    {
        Vector2 offest = player.Direction > 0 ? new Vector2(7,0):new Vector2(-7,0);
        Scale = new Vector2(player.Direction, 1);
        GlobalPosition = player.GlobalPosition + offest;
        Visible = true;
        
        Play(name);
    }

    public void StopAnimation()
    {
        Visible = false;
        
        Stop();
    }
    public void OnFinished()
    {
        Visible = false;
    }
}
