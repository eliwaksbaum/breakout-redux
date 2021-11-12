using Godot;

public class SizeDown : Power
{
    public override void DoPower(Paddle paddle)
    {
        paddle.GetNode<PaddleSizeChanger>("Resizer").SizeDown();
    }
}