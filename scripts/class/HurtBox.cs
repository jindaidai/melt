using Godot;
using System;

[GlobalClass]
public partial class HurtBox : Area2D
{
    [Signal]
    public delegate void HurtEventHandler(HitBox hitBox);
    public HurtBox()
    {
        Connect(HurtBox.SignalName.AreaEntered,new Callable(this,nameof(OnHurt)));
    }

    public void OnHurt(HitBox hitBox)
    {
        hitBox.EmitSignal(HitBox.SignalName.Hit,this);
        this.EmitSignal(HurtBox.SignalName.Hurt,hitBox);
    }
}
