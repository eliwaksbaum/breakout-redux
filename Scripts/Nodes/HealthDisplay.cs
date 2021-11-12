using Godot;

public class HealthDisplay : Control
{
	[Export]
	public IntValue health;

	Sprite[] hearts = new Sprite[3];
	int localHealth;

	public override void _Ready()
	{
		for (int i = 0; i < 3; i++)
		{
			Sprite heart = GetNode<Sprite>("Heart" + i + "/Front");
			hearts[i] = heart;
		}
		localHealth = health.Value;
	}

	public override void _Process(float delta)
	{
		if (localHealth != health.Value)
		{
			SetHearts();
		}
	}

	void SetHearts()
	{
		for (int i = 0; i < 3; i++)
		{
			if (i + 1 > health.Value)
			{
				hearts[i].Hide();
			}
			else
			{
				hearts[i].Show();
			}
		}
		localHealth = health.Value;
	}
}