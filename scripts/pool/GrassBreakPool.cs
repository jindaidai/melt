using Godot;
using System;

[GlobalClass]
public partial class GrassBreakPool : ObjectPool<GrassBreakEffect>
{
    public override GrassBreakEffect CreateObject()
    {
        GrassBreakEffect effect = base.CreateObject();
        effect.SetPool(this);

        return effect;
    }
}