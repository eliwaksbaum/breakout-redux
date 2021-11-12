using Godot;

public abstract class Power : Area2D, IStoppable
{
    [Export]
    public IntValue speed;
    
    bool active = true;

    public abstract void DoPower(Paddle paddle);

    public override void _Process(float delta)
    {
        if (active)
        {
            Position += Vector2.Down * speed.Value;
        }
    }

    public void OnExitScreen()
    {
        if (Position.y > 0)
        {
            QueueFree();
        }
    }

    public void Stop()
    {
        active = false;
    }
}