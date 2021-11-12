using Godot;

public class BallData : Resource
{
    [Export]
    public int force;
    [Export]
    public int maxAngle;
    [Export]
    public float aimSpeed;
}