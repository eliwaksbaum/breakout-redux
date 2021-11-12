using Godot;

public class VBadAngleFixer : StaticBody2D
{
    [Export]
    public float tolerance;

    public void OnBodyExited(PhysicsBody2D other)
    {
        if (other.IsInGroup("Balls"))
        {
            Ball ball = (Ball) other;
            if (Mathf.Abs(ball.LinearVelocity.x) < tolerance)
            {
                float dir = ball.LinearVelocity.x != 0 ? Mathf.Abs(ball.LinearVelocity.x)/ball.LinearVelocity.x : 1;
                ball.LinearVelocity = new Vector2((1.1f * tolerance) * dir, ball.LinearVelocity.y);
            }
        }
    }
}