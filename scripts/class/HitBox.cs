using Godot;
using System;

[GlobalClass]
public partial class HitBox : Area2D
{
    [Signal]
    public delegate void HitEventHandler(HurtBox hurtBox);
    HitBox()
    {
        Connect(SignalName.BodyShapeEntered,new Callable(this,nameof(Destory)));
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public void Destory(Rid bodyRid,Node2D body,int bodyShapeIndex,int localShapeIndex)
    {
        if(body is TileBreakManager)
        {
            TileBreakManager manager = body as TileBreakManager;
            Vector2I cellPosition = manager.GetCoordsForBodyRid(bodyRid);
            manager.BreakTile(cellPosition);
        }
    }
}
