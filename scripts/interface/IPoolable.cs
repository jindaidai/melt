using Godot;
using System;

public interface IPoolable
{
    void Activate();
    void Deactivate();
}