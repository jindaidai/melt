using Godot;
using System;
[GlobalClass]
public partial class Stats : Resource
{
    [Export] public float maxHealth;
    [Export] public float damage;
    [Export] public float speed;
    [Export] public float acceleration;
    [Export] public float fallSpeed;
    [Export] public float fallAcceleration;
}
