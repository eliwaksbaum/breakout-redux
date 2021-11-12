using Godot;

public class LifeUp : Power
{
    [Export]
    public IntValue health;

    public override void DoPower(Paddle paddle)
    {
        health.Value += health.Value < 3 ? 1 : 0;
    }
}