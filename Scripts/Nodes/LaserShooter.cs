using Godot;

public class LaserShooter : Timer, IStoppable
{
    [Export]
    public int rounds;
    [Export]
    public Vector2 offset;
    [Export]
    public PackedScene laserPair;

    AudioStreamPlayer audio;
    int roundsFired;

    public override void _Ready()
    {
        audio = GetNode<AudioStreamPlayer>("Audio");
    }

    public void OnTimeout()
    {
        if (roundsFired >= rounds)
        {
            Stop();
        }
        {
            audio.Play();
            Node2D newPew = laserPair.Instance<Node2D>();
            newPew.GlobalPosition = GetParent<Node2D>().Position + offset;
            AddChild(newPew); 
            roundsFired += 1;
        }
    }

    new public void Stop()
    {
        base.Stop();
    }
}