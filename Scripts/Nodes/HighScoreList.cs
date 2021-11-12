using Godot;
using System.Collections.Generic;

public class HighScoreList : VBoxContainer
{
    public override void _Ready()
    {
        List<HighScore> highscores = SaveDataHandler.GetScores();
        
        for (int i = 0; i < GetChildCount(); i++)
        {
            HBoxContainer record = GetChild<HBoxContainer>(i);
            HighScore highscore = i < highscores.Count ? highscores[i] : new HighScore(-1, "");

            if (highscore.score == -1)
            {
                record.GetNode<Label>("Name").Text = "";
                record.GetNode<Label>("Score").Text = "";
            }
            else
            {
                record.GetNode<Label>("Name").Text = highscore.name;
                record.GetNode<Label>("Score").Text = highscore.score.ToString();
            }
        }

    }
}