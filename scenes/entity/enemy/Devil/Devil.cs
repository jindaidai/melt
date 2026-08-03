using Godot;
using System;

public partial class Devil : CharacterBody2D
{
    public enum State
    {
        Idle,Running,Jump,Fall,Hurt,Dead,Ground,Air,Attack,Keep
    }
}
