using Godot;
using System;

public class PowerSpawner : Timer, IStoppable
{
    [Export]
    public float spawnChance;
    [Export]
    public PowerChance[] powers;

    float[] spinner;
    bool serving = true;
    Random random = new Random();
    float minX;
    float maxX;

    public override void _EnterTree()
	{
		Ball.BallOut += OnBallOut;
	}
	public override void _ExitTree()
	{
		Ball.BallOut -= OnBallOut;
	}

    public override void _Ready()
    {
        Sprite powerSprite = powers[0].power.Instance().GetNode<Sprite>("Sprite");
        float powerX = powerSprite.Texture.GetWidth() * powerSprite.Scale.x;
        minX = powerX/2;
        maxX = GetViewport().Size.x - minX;
        spinner = DivvySpinner();
    }

    float[] DivvySpinner()
    {
        float[] markers = new float[powers.Length];
        float rolling = 0;
        for (int i = 0; i < powers.Length; i++)
        {
            rolling += powers[i].relativeChance;
            markers[i] = rolling;
        }
        return markers;
    }

    public void OnBallOut()
    {
        Stop();
    }

    public override void _Process(float delta)
    {
        if (serving && Input.IsActionJustReleased("Fire"))
        {
            serving = false;
            Start();
        }
    }

    public void OnTimeout()
    {
        if (random.NextDouble() < spawnChance)
        {
            int powerI = pickPower(random.NextDouble());
            float powerX = (float) random.NextDouble() * (maxX - minX) + minX;
            Node2D power = powers[powerI].power.Instance<Node2D>();
            power.Position = new Vector2(powerX, -10);
            AddChild(power);
        }
    }

    int pickPower(double spin)
    {
        for (int i = 0; i < spinner.Length; i++)
        {
            if (spin > spinner[i])
            {
                continue;
            }
            return i;
        }
        return spinner.Length;
    }

    new public void Stop()
    {
        base.Stop();
    }

    public void GetReady()
    {
        serving = true;
    }
}
