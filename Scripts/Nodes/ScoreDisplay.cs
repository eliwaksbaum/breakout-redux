using Godot;

public class ScoreDisplay : Label
{
    [Export]
    public IntValue score;

    public override void _Process(float delta)
    {
        Text = "Score: " + score.Value;
        Show();
    }
}