using Godot;
using System.Collections.Generic;

public class PaddleSelect : VBoxContainer
{
    [Export]
    public int numPaddles;
    [Export]
    public int[] thresholds;
    [Export]
    AudioStream playClip, lockedClip;

    Label message;
    Control padlock;
    Control paddle;
    int highscore;
    int current;

    public override void _Ready()
    {
        message = GetNode<Label>("Message");
        padlock = GetNode<Control>("HBox/Lock");
        paddle = GetNode<Control>("HBox/Paddle");
        List<HighScore> scores = SaveDataHandler.GetScores();
        highscore = scores.Count == 0 ? 0 : scores[0].score;
        Display(0);
    }

    public void OnRButtonUp()
    {
        Display(current + 1);
    }
    public void OnLButtonUp()
    {
        Display(current - 1);
    }

    public void Display(int i)
    {
        i = i%numPaddles;
        i = i < 0 ? i + numPaddles : i;
        if (highscore >= thresholds[i])
        {
            message.Text = "";
            padlock.Hide();
            paddle.GetNode<Sprite>("Sprite").Frame = i;
            paddle.Show();
        }
        else
        {
            message.Text = "score " + thresholds[i] + " points to unlock";
            paddle.Hide();
            padlock.Show();
        }
        current = i;
    }

    public void OnPlayButtonUp()
    {
        if (paddle.Visible)
        {
            Paddle.Color = current;
            AudioPlayer.Clip(GetTree().Root, playClip).Play();
            GetTree().ChangeScene("res://Scenes/Level.tscn");
        }
        else
        {
            AudioPlayer.Clip(GetTree().Root, lockedClip).Play();
        }
    }
}
