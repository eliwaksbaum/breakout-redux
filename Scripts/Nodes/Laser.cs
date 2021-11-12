using Godot;

public class Laser : Area2D, IStoppable
{
    [Export]
    public int speed;

    bool active = true;

    public override void _Process(float delta)
    {
        if (active)
        {
            Position += Vector2.Up * speed;
        }
    }

    public void Stop()
    {
        active = false;
    }
}