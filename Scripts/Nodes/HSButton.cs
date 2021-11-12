using Godot;

public class HSButton : TextButton
{
    public void OnButtonUp()
    {
        GetTree().ChangeScene("res://Scenes/HighScores.tscn");
    }
}
