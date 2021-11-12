using Godot;

public class Paddle : Area2D
{
    [Export]
    public float speed;
    [Export]
    public Vector2 servePosition;

    [Export]
    public BallData ballData;

    Sprite sprite;
    float HalfX => sprite.Texture.GetWidth() * sprite.Scale.x * Scale.x * .5f;
    bool serving;
    bool active;
    AudioStreamPlayer powerSound;

    static int color;
    public static int Color
    {
        set => color = value;
    }

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
        sprite = GetNode<Sprite>("Sprite");
        powerSound = GetNode<AudioStreamPlayer>("Audio");
        sprite.Frame = color;
        Reset();
    }

    public override void _Process(float delta)
    {
        if (active)
        {
            if (!serving)
            {
                Vector2 velocity = new Vector2();

                if (Input.IsActionPressed("ui_right"))
                {
                    velocity.x += 1;
                }
                if (Input.IsActionPressed("ui_left"))
                {
                    velocity.x += -1;
                }

                Position += velocity * speed;
                Position = new Vector2(Mathf.Clamp(Position.x, HalfX, GetViewport().GetVisibleRect().Size.x - HalfX), Position.y);
            }
            else
            {
                if (Input.IsActionJustReleased("Fire"))
                {
                    serving = false;
                }
            }
        }
    }

    public void Reset()
    {
        serving = true;
        active = true;
        Position = servePosition;
    }

    public void OnBallOut()
    {
        Reset();
    }

    public void OnBodyEntered(PhysicsBody2D other)
    {
        if (other.IsInGroup("Balls"))
        {
            BounceBall((Ball) other);
        }
    }
    void BounceBall(Ball ball)
    {
        ball.LinearVelocity = Vector2.Zero;
        float ratio = (ball.Position.x - Position.x)/HalfX;
        float angle = ratio * ballData.maxAngle;
        Vector2 direction = Vector2.Up.Rotated(Mathf.Deg2Rad(angle));
        ball.ApplyCentralImpulse(direction * ballData.force);
    }

    public void OnAreaEntered(Area2D other)
    {
        if (other.IsInGroup("Powers") && !serving)
        {
            powerSound.Play();
            Power power = (Power) other;
            power.CallDeferred("DoPower", this);
            power.QueueFree();
        }
    }

    public void Stop()
    {
        active = false;
    }
}