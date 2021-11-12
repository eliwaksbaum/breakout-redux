using Godot;
using System.Collections.Generic;

public class ScoreInput : LineEdit
{
    [Export]
    public IntValue score;

    public override void _Ready()
    {
        GrabFocus();
        GetNode<AudioStreamPlayer>("Audio").Play();
    }

    public override void _Process(float delta)
    {
        if(Input.IsActionJustReleased("ui_accept") && Text.Length > 0)
        {
            int highscore = score.Value;
            SaveDataHandler.AddNewScore(highscore, Text);

            GetTree().ChangeScene("res://Scenes/HighScores.tscn");
        }
    }

    public void OnTextChanged(string text)
    {
        Text = text.ToUpper();
        CaretPosition = text.Length;
    }
}
