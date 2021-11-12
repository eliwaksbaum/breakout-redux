using Godot;
using System;

public class PaddleSizeChanger : Node2D
{
    [Export]
    public float[] sizes;
    [Export]
    public int startSize;

    int currentSize;
    Node2D paddle;

    public override void _Ready()
    {
        currentSize = startSize;
        paddle = GetParent<Node2D>();
    }

    public void SizeUp()
    {
        if (currentSize < sizes.Length - 1)
        {
            currentSize += 1;
            paddle.Scale = new Vector2(sizes[currentSize], 1);
        }
    }

    public void SizeDown()
    {
        if (currentSize > 0)
        {
            currentSize -= 1;
            paddle.Scale = new Vector2(sizes[currentSize], 1);
        }
    }
}
