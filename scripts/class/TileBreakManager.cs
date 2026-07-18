using Godot;
using System;

public partial class TileBreakManager : TileMapLayer
{
    [Export]
    GrassBreakPool grassBreakPool;
    public void BreakTile(Vector2I cell)
    {
        EraseCell(cell);

        Vector2 localPos = MapToLocal(cell);
        Vector2 globalPosition = ToGlobal(localPos);

        GrassBreakEffect grassBreakEffect = grassBreakPool.GetObject();
        grassBreakEffect.GlobalPosition = globalPosition;
    }
}
