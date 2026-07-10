using Godot;
using System;

[GlobalClass]
public partial class HitBox : Area2D
{
    [Signal]
    public delegate void HitEventHandler(HurtBox hurtBox);

    static readonly PackedScene GrassDestrustionEffect = GD.Load<PackedScene>("uid://d6wmc84cjnjk");
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
        if(body is TileMapLayer)
        {
            TileMapLayer tileMapLayer = body as TileMapLayer;
            Vector2I cellPosition = tileMapLayer.GetCoordsForBodyRid(bodyRid);
            Vector2 localPos = tileMapLayer.MapToLocal(cellPosition);
            Vector2 globalPosition = tileMapLayer.ToGlobal(localPos);
            tileMapLayer.EraseCell(cellPosition);

            GrassBreakEffect destrustionEffect = GrassDestrustionEffect.Instantiate<GrassBreakEffect>();
            destrustionEffect.GlobalPosition = globalPosition;
            GetTree().CurrentScene.AddChild(destrustionEffect);
        }
    }
}
