using Godot;

public class HSBackButton : TextButton
{
    public void OnButtonUp()
    {
        GetTree().ChangeScene("res://Scenes/Start.tscn");
    }
}
