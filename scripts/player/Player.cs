using Godot;
using System;

public partial class Player : CharacterBody2D
{
   [Export] public Stats stats;
    public AnimationPlayer fireAnimationPlayer;
    public AnimationPlayer iceAnimationPlayer;
    public Node2D graphics;
    public StateMachine stateMachine;
    public Timers timers;
    public CollisionShape2D hitCollision;
    public const float MaxFallSpeed = 500;
    public enum PlayerType
    {
        Fire,Ice
    }
    public enum State
    {
        Idle,Running,Jump,AirJump,Fall,Attack1,Attack2,Attack3,
        Ground,Air,Attack,Transform,Skill,Keep
    }
    public enum TimerType
    {
        Coyote,HitContinue,
    }
    public readonly State[] GroundState = [State.Idle,State.Running];
    public readonly State[] AirState = [State.Jump,State.AirJump,State.Fall];
    public readonly State[] AttackState = [State.Attack1,State.Attack2,State.Attack3];

    public PlayerType currentType;
    public PlayerType CurrentType
    {
        get{return currentType;}
        set
        {
            if(value == PlayerType.Fire)
            {
                SetNodeActive(iceAnimationPlayer,false);
                SetNodeActive(fireAnimationPlayer,true);
            }
            if(value == PlayerType.Ice)
            {
                SetNodeActive(fireAnimationPlayer,false);
                SetNodeActive(iceAnimationPlayer,true);
            }
            currentType = value;
        }
    }
    private float direction = 1;
    public float Direction
    {
        get{return direction;}
        set
        {
            if(direction == value)
            {
                return;
            }
            else
            {
                direction = value;
                graphics.Scale = new Vector2(direction,1);
            }
        }
    }
    public int currentJumpCount;
    public int maxJumpCount;
    
    Player()
    {
        currentJumpCount = 0;
        maxJumpCount = 2;
        currentType = PlayerType.Fire;
    }
    public override void _Ready()
    {
        base._Ready();

        graphics = GetNode<Node2D>("Graphics");
        fireAnimationPlayer = GetNode<AnimationPlayer>("FireAnimationPlayer");
        iceAnimationPlayer = GetNode<AnimationPlayer>("IceAnimationPlayer");
        stateMachine = GetNode<StateMachine>("StateMachine");
        timers = GetNode<Timers>("Timers");
        hitCollision = GetNode<CollisionShape2D>("Graphics/HitBox/CollisionShape2D");
    }

    public void PhysicUpdate(float delta)
    {
        Velocity = Velocity.MoveToward(new Vector2(stats.speed * Direction,Velocity.Y),stats.acceleration * delta);
        Velocity = Velocity.MoveToward(new Vector2(Velocity.X,stats.fallSpeed),stats.fallAcceleration * delta);
        
        MoveAndSlide();
    }

     public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        stateMachine.PhysicUpdate(delta);
        stateMachine.currentState?.PhysicUpdate(delta);
        PhysicUpdate((float)delta);  
    }


    public void PlayAnimation(string name)
    {
        if(currentType == PlayerType.Fire)
            fireAnimationPlayer.Play(name);
        if(currentType == PlayerType.Ice)
            iceAnimationPlayer.Play(name);
    }

    public bool IsAnimationPlaying()
    {
        if(currentType == PlayerType.Fire)
            return fireAnimationPlayer.IsPlaying();
        if(currentType == PlayerType.Ice)
            return iceAnimationPlayer.IsPlaying();

        return false;
    }
    private void SetNodeActive(Node node, bool active)
    {
        node.ProcessMode = active
            ? Node.ProcessModeEnum.Inherit
            : Node.ProcessModeEnum.Disabled;

        if (node is CanvasItem canvasItem)
        {
            canvasItem.Visible = active;
        }

        if (node is CollisionShape2D collisionShape)
        {
            collisionShape.Disabled = !active;
        }

        if (node is Area2D area)
        {
            area.Monitoring = active;
            area.Monitorable = active;
        }
    }
}




