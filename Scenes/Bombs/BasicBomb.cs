using Godot;
using System;

public partial class BasicBomb : MeshInstance3D
{

	[Export] MeshInstance3D Fireball { get; set; }
	[Export] MeshInstance3D NorthPillar { get; set; }
    [Export] MeshInstance3D WestPillar { get; set; }

	[Export] MeshInstance3D NorthCap { get; set; }
	[Export] MeshInstance3D SouthCap { get; set; }
	[Export] MeshInstance3D EastCap { get; set; }
	[Export] MeshInstance3D WestCap { get; set; }



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
		CylinderMesh North = (CylinderMesh)NorthPillar.Mesh;
		CylinderMesh West = (CylinderMesh)WestPillar.Mesh;

		NorthCap.Position = new Vector3(0, 0, -North.Height / 2.0f);
		SouthCap.Position = new Vector3(0, 0, North.Height / 2.0f);

		WestCap.Position = new Vector3(-West.Height / 2.0f, 0, 0);
		EastCap.Position = new Vector3(West.Height / 2.0f, 0, 0);
    }

	public void Explode()
	{

		GetTree().CreateTimer(0.9).Timeout += () => QueueFree();

		
		Pillars.Visible = true;
		NorthHitbox.Disabled = false; WestHitbox.Disabled = false;
		Particles.Emitting = true;

        
		Tween NorthPillarTween = GetTree().CreateTween();
		NorthPillarTween.TweenProperty(NorthPillar.Mesh, "height", 8, 0.2).SetTrans(Tween.TransitionType.Sine);

        Tween WestPillarTween = GetTree().CreateTween();
        WestPillarTween.TweenProperty(WestPillar.Mesh, "height", 8, 0.2).SetTrans(Tween.TransitionType.Sine);

        Tween NorthHitboxTween = GetTree().CreateTween();
        NorthHitboxTween.TweenProperty(NorthHitbox.Shape, "height", 8, 0.2).SetTrans(Tween.TransitionType.Sine);

        Tween WestHitboxTween = GetTree().CreateTween();
        WestHitboxTween.TweenProperty(WestHitbox.Shape, "height", 8, 0.2).SetTrans(Tween.TransitionType.Sine);
		
        /*
        //elastic acceleration
        Tween NorthPillarTween = GetTree().CreateTween();
        NorthPillarTween.TweenProperty(NorthPillar.Mesh, "height", 8, 0.4).SetTrans(Tween.TransitionType.Elastic);

        Tween WestPillarTween = GetTree().CreateTween();
        WestPillarTween.TweenProperty(WestPillar.Mesh, "height", 8, 0.4).SetTrans(Tween.TransitionType.Elastic);

        Tween NorthHitboxTween = GetTree().CreateTween();
        NorthHitboxTween.TweenProperty(NorthHitbox.Shape, "height", 8, 0.4).SetTrans(Tween.TransitionType.Elastic);

        Tween WestHitboxTween = GetTree().CreateTween();
        WestHitboxTween.TweenProperty(WestHitbox.Shape, "height", 8, 0.4).SetTrans(Tween.TransitionType.Elastic);



        //weird bounce effect
        Tween NorthPillarTween = GetTree().CreateTween();
        NorthPillarTween.TweenProperty(NorthPillar.Mesh, "height", 8, 0.6).SetTrans(Tween.TransitionType.Bounce);

        Tween WestPillarTween = GetTree().CreateTween();
        WestPillarTween.TweenProperty(WestPillar.Mesh, "height", 8, 0.6).SetTrans(Tween.TransitionType.Bounce);

        Tween NorthHitboxTween = GetTree().CreateTween();
        NorthHitboxTween.TweenProperty(NorthHitbox.Shape, "height", 8, 0.6).SetTrans(Tween.TransitionType.Bounce);

        Tween WestHitboxTween = GetTree().CreateTween();
        WestHitboxTween.TweenProperty(WestHitbox.Shape, "height", 8, 0.6).SetTrans(Tween.TransitionType.Bounce);
        */

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
