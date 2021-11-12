using Godot;

public class SizeUp : Power
{
    public override void DoPower(Paddle paddle)
    {
        paddle.GetNode<PaddleSizeChanger>("Resizer").SizeUp();
    }
}