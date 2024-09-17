using Godot;
using System;
using System.Collections.Generic;

public partial class SceneSwitcher : Node
{
	static public SceneSwitcher instance = null;
	static public Node Root = null;

	static Stack<Node> nodes = new Stack<Node>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (instance == null)
		{
			instance = this;
			Root = GetTree().Root;
			PushScene("res://Scenes/Menus/Main.tscn");

        }
		else
		{
			QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void PushScene(string ScenePath)
	{
		Node previousScene = null;
		if (nodes.Count > 0) 
		{
			previousScene = nodes.Pop();
			RemoveChild(previousScene);
		}
		Node scene = GD.Load<PackedScene>(ScenePath).Instantiate<Node>();
		nodes.Push(scene);
		AddChild(scene);
	}

	public void PopScene()
	{
		if (nodes.Count == 0)
		{
			return;
		}

		Node node = nodes.Pop();

		if(node.GetParent() == this)
		{
			this.RemoveChild(node);
			node.QueueFree();
		}

		if (nodes.Count > 0)
		{
			Node previousScene=nodes.Peek();
			if(previousScene.GetParent() != this)
			{
				this.AddChild(previousScene);
			}
		}
	}
}
