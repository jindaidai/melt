using Godot;
using System;
namespace Game.PlayerAttackEffect;
public partial class AttackEffect : AttackEffectBase
{
     private PlayerAttackEffectPool _pool;
    public void SetPool(PlayerAttackEffectPool pool)
    {
        _pool = pool;
    }

    public override void OnFinished()
    {
        _pool.ReturnObject(this);
    }
}
