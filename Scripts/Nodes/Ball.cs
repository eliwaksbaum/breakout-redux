using Godot;
using System.Collections.Generic;
using System;

public class Ball : RigidBody2D, IStoppable
{
	public static event Action BallOut;

	[Export]
	public BallData data;
	[Export]
	public Vector2 servePosition;
	[Export]
	public AudioStream loselife, hit;

	bool serving;
	Node2D aim;

	static bool splitted;
	static List<Ball> activeSplits = new List<Ball>();

	public override void _Ready()
	{
		aim = GetNode<Node2D>("Aim");
		activeSplits.Add(this);
		if (!splitted)
		{
			Reset();
		}
	}

	public override void _Process(float delta)
	{
		if (serving)
		{
			if (Input.IsActionPressed("ui_right"))
			{
				aim.Rotation += data.aimSpeed;
				aim.Rotation = Mathf.Clamp(aim.Rotation, Mathf.Deg2Rad(-data.maxAngle), Mathf.Deg2Rad(data.maxAngle));
			}
			if (Input.IsActionPressed("ui_left"))
			{
				aim.Rotation -= data.aimSpeed;
				aim.Rotation = Mathf.Clamp(aim.Rotation, Mathf.Deg2Rad(-data.maxAngle), Mathf.Deg2Rad(data.maxAngle));
			}
			if (Input.IsActionJustReleased("Fire"))
			{
				Mode = ModeEnum.Rigid;
				ApplyCentralImpulse(Vector2.Up.Rotated(aim.Rotation) * data.force);
				serving = false;
				aim.Hide();
			}
		}
	}

	public void OnFallThrough()
	{
		if (!splitted && Position.y > GetViewport().GetVisibleRect().Size.y)
		{
			if (BallOut != null)
			{
				AudioPlayer.Clip(GetTree().Root, loselife).Play();
				BallOut();
			}
			Reset();
		}
		else
		{
			QueueFree();
			activeSplits.Remove(this);
			if (activeSplits.Count == 1)
			{
				splitted = false;
			}
		}
	}

	public static void ResetSplit()
	{
		splitted = false;
		if (activeSplits.Count > 1)
		{
			for (int i = 1; i < activeSplits.Count; i++)
			{
				activeSplits[i].QueueFree();
			}
			activeSplits = new List<Ball>(){activeSplits[0]};
			activeSplits[0].Reset();
		}
		else if (activeSplits.Count > 0)
		{
			activeSplits[0].Reset();
		}
	}
	public static void ClearSplit()
	{
		splitted = false;
		activeSplits = new List<Ball>();
	}

	public void Reset()
	{
		Mode = ModeEnum.Static;
		serving = true;
		Position = servePosition;
		aim.Rotation = 0;
		aim.Show();
	}

	public void Stop()
	{
		Mode = ModeEnum.Static;
		serving = false;
	}

	public static void Split()
	{
		splitted = true;
		Ball origin = activeSplits[0];
		Node level = origin.GetParent();
		level.AddChild(origin.Copy(2 * Mathf.Pi/3));
		level.AddChild(origin.Copy(4 * Mathf.Pi/3));
	}

	Ball Copy(float angle)
	{
		Ball ball = GD.Load<PackedScene>("res://Prefabs/Ball.tscn").Instance<Ball>();
		ball.Position = Position;
		ball.ApplyCentralImpulse(LinearVelocity.Rotated(angle));
		return ball;
	}

	public void OnAreaEntered(Area2D other)
	{
		if (other.Name == "Paddle")
		{
			AudioPlayer.Clip(GetTree().Root, hit).Play();;
		}
	}
	public void OnBodyEntered(PhysicsBody2D other)
	{
		if (other.IsInGroup("Walls") || other.IsInGroup("Blocks"))
		{
			AudioPlayer.Clip(GetTree().Root, hit).Play();
		}
	}
}