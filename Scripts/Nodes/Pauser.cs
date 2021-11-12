using Godot;
using System;

public class Pauser : Control
{
    [Export]
    Texture pause, play;

    Button button;
    CanvasItem message;
    bool paused;

    public override void _Ready()
    {
        button = GetNode<Button>("Button");
        message = GetNode<CanvasItem>("Message");
    }

    public void OnButtonUp()
    {
        paused = !paused;
        GetTree().Paused = paused;
        if (paused)
        {
            button.Icon = play;
            message.Show();
        }
        else
        {
            button.Icon = pause;
            message.Hide();
        }
    }
}
