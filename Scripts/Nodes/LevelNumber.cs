using Godot;

public class LevelNumber : Node2D
{
    public override void _Process(float delta)
    {
        if (Input.IsActionJustReleased("Fire"))
        {
            Hide();
        }
    }

    public void Set(int number)
    {
        GetNode<Label>("Box/Label").Text = number.ToString();
        Show();
    }
}
