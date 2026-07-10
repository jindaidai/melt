using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class ObjectPool<T> : Node where T : Node
{
    [Export]
    public PackedScene objectScene;
    [Export]
    public int initialSize = 20;

    private readonly Queue<T> _pool = new();

    public override void _Ready()
    {
        base._Ready();
        for(int i = 0;i < initialSize; i++)
        {
            T obj = CreateObject();
            _pool.Enqueue(obj);
        }
    }

    public T CreateObject()
    {
        T obj = objectScene.Instantiate<T>();
        AddChild(obj);
        
        return obj;
    }

    public T GetObject()
    {
        if(_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        return null;
    }

    public void ReturnObject(T obj)
    {
        _pool.Enqueue(obj);
    }
}
