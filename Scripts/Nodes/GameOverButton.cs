using Godot;
using System;

public class GameOverButton : TextButton
{
    [Export]
    public IntValue score;

    public void OnButtonUp()
    {
        if (SaveDataHandler.IsNewRecord(score.Value))
        {
            GetTree().ChangeScene("res://Scenes/NewRecord.tscn");
        }
        else
        {
            GetTree().ChangeScene("res://Scenes/Start.tscn");
        }
    }
}
