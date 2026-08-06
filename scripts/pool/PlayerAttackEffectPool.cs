using Godot;
using System;
using Game.PlayerAttackEffect;

[GlobalClass]
public partial class PlayerAttackEffectPool : ObjectPool<AttackEffect>
{
    public override AttackEffect CreateObject()
    {
        AttackEffect effect = base.CreateObject();
        
        effect.SetPool(this);
        return effect;
    }
}