using Godot;
using System;

public partial class BasicBomb : MeshInstance3D
{

	[Export] MeshInstance3D Fireball { get; set; }
	[Export] MeshInstance3D NorthPillar { get; set; }
    [Export] MeshInstance3D WestPillar { get; set; }
    [Export] Node3D Pillars { get; set; }
    [Export] GpuParticles3D Particles { get; set; }

	[Export] CollisionShape3D NorthHitbox { get; set; }
    [Export] CollisionShape3D WestHitbox { get; set; }

    [Export] MeshInstance3D Player { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		GetTree().CreateTimer(0.7f).Timeout += () => Explode();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Explode()
	{

		GetTree().CreateTimer(0.8).Timeout += () => QueueFree();

		Fireball.Visible = true;
		Pillars.Visible = true;
		NorthHitbox.Disabled = false; WestHitbox.Disabled = false;

		Tween t = GetTree().CreateTween();
		//Particles.Emitting = true;
		t.TweenProperty(Fireball, "scale", new Vector3(5.0f, 5.0f, 5.0f), 0.4);
		t.Finished += () => Fireball.Visible = false;


		Tween NorthPillarTween = GetTree().CreateTween();
		NorthPillarTween.TweenProperty(NorthPillar.Mesh, "height", 8, 0.2);

        Tween WestPillarTween = GetTree().CreateTween();
        WestPillarTween.TweenProperty(WestPillar.Mesh, "height", 8, 0.2);

        Tween NorthHitboxTween = GetTree().CreateTween();
        NorthHitboxTween.TweenProperty(NorthHitbox.Shape, "height", 8, 0.2);

        Tween WestHitboxTween = GetTree().CreateTween();
        WestHitboxTween.TweenProperty(WestHitbox.Shape, "height", 8, 0.2);
    }

	public void OnWestHitboxAreaEntered(Area3D meow)
	{
        GD.Print("meow");
    }

    private void T_Finished()
    {
        throw new NotImplementedException();
    }
}
