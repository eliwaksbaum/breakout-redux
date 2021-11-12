using Godot;
using System;

public class TextButton : Button
{
    [Export]
    AudioStream buttonClip;

    public override void _Ready()
    {
        this.Connect("button_up", this, "Play");
    }

    public void Play()
    {
        AudioPlayer.Clip(GetTree().Root, buttonClip).Play();
    }
}
