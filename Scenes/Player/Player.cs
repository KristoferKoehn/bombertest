using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	[Export] PackedScene BombScene { get; set; }
    [Export] AnimationTree AnimationTree { get; set; }
    [Export] Node3D RigParent { get; set; }

	bool dead = false;

    public override void _PhysicsProcess(double delta)
	{
		
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
            velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() && !dead)
		{
			BasicBomb bb = BombScene.Instantiate<BasicBomb>();
			SceneSwitcher.Root.AddChild(bb);
            //bb.GlobalPosition = new Vector3((int)GlobalPosition.X, GlobalPosition.Y, (int)GlobalPosition.Z);
            bb.GlobalPosition = new Vector3(Mathf.Floor(GlobalPosition.X) + 0.5f, 0.5f, Mathf.Floor(GlobalPosition.Z) + 0.5f);
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero && !dead)
		{
			RigParent.LookAt(GlobalPosition - direction);
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			Tween t = GetTree().CreateTween();
			t.TweenProperty(AnimationTree, "parameters/RunningBlend/blend_amount", direction.Normalized().Length(), 0.2);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
            GetTree().CreateTween().TweenProperty(AnimationTree, "parameters/RunningBlend/blend_amount", 0, 0.2);
        }

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Dead()
	{
        GetTree().CreateTween().TweenProperty(AnimationTree, "parameters/Death/blend_amount", 1, 0.2);
		dead = true;
		GetTree().CreateTimer(1).Timeout += Alive;
    }

	public void Alive()
	{
		dead = false;
        GetTree().CreateTween().TweenProperty(AnimationTree, "parameters/Death/blend_amount", 0, 0.2);
    }

}
