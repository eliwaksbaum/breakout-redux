using Godot;

public class Screen : VBoxContainer
{
    public override void _Ready()
    {
        GetNode("Button").Connect("button_up", this, "OnButtonUp");
    }

    public void OnButtonUp()
    {
        Hide();
    }
}
