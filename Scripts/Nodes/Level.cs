using Godot;

public class Level : Node2D
{
	[Export]
	public IntValue score, health;
	[Export]
	public int maxLevel;
	[Export]
	public AudioStream loseClip, winClip;

	Node currentLevel;
	int currentLevelInt;

	CanvasItem WinScreen;
	CanvasItem LoseScreen;
	CanvasItem LevelClearScreen;
	LevelNumber Number;
	PowerSpawner spawner;

	public override void _EnterTree()
	{
		Brick.BrickDeath += CheckWin;
		Ball.BallOut += OnBallOut;
		Ball.ClearSplit();
	}
	public override void _ExitTree()
	{
		Brick.BrickDeath -= CheckWin;
		Ball.BallOut -= OnBallOut;
	}

	public override void _Ready()
	{
		health.Value = 3;
		score.Value = 0;

		WinScreen = GetNode<CanvasItem>("Canvas/WinScreen");
		LevelClearScreen = GetNode<CanvasItem>("Canvas/LevelClearScreen");
		LoseScreen = GetNode<CanvasItem>("Canvas/LoseScreen");
		Number = GetNode<LevelNumber>("Number");
		spawner = GetNode<PowerSpawner>("PowerSpawner");

		LoadLevel(1);
	}

	void LoadLevel(int levelIndex)
	{
		GetTree().CallGroup("Clear", "queue_free");
		if (levelIndex <= maxLevel)
		{
			currentLevelInt = levelIndex;
			if (currentLevel != null)
			{
				currentLevel.QueueFree();
			}
			PackedScene level = GD.Load<PackedScene>("res://Levels/Level" + levelIndex + ".tscn");
			currentLevel = level.Instance();
			AddChild(currentLevel);
			Ball.ResetSplit();
			Number.Set(levelIndex);
			spawner.GetReady();
		}
	}

	public void LoadNext()
	{
		LoadLevel(currentLevelInt + 1);
	}

	void CheckWin()
	{
		if (GetTree().GetNodesInGroup("Bricks").Count == 1)
		{
			if (currentLevelInt == maxLevel)
			{
				WinScreen.Show();
			}
			else
			{
				LevelClearScreen.Show();
			}
			AudioPlayer.Clip(GetTree().Root, winClip).Play();
			GetTree().CallGroup("IStoppables", "Stop");
		}
	}

	void OnBallOut()
	{
		health.Value--;
		if (health.Value <= 0)
		{
			LoseScreen.Show();
			AudioPlayer.Clip(GetTree().Root, loseClip).Play();
			GetTree().CallGroup("IStoppables", "Stop");
		}
	}
}