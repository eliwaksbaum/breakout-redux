using Godot;
using System;

public class PlayButton : TextButton
{
    public void OnButtonUp()
    {
        GetTree().ChangeScene("res://Scenes/PaddleSelect.tscn");
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustReleased("ui_accept"))
        {
            GrabFocus();
        }
    }
}
