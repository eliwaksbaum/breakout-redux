using Godot;

public class HBadAngleFixer : StaticBody2D
{
    [Export]
    public BallData ballData;

    float max => Mathf.Pi/2 - Mathf.Deg2Rad(ballData.maxAngle);

    public void OnBodyExited(PhysicsBody2D other)
    {
        if (other.IsInGroup("Balls"))
        {
            Ball ball = (Ball) other;
            Vector2 velocity = ball.LinearVelocity;
            Vector2 newAngle = Vector2.Zero;

            if (Vector2.Up.AngleTo(velocity) > 0) // Bouncing to the right off of the left wall
            {
                if (Mathf.Abs(velocity.AngleTo(Vector2.Right)) < max - Mathf.Pi/180)
                {
                    float multiplier = Mathf.Abs(Vector2.Right.AngleTo(velocity))/Vector2.Right.AngleTo(velocity);
                    newAngle = Vector2.Right.Rotated(max * multiplier);
                }
            }
            else if (Vector2.Up.AngleTo(velocity) < 0) //Bouncing to the left off of the right wall
            {
                if (Mathf.Abs(velocity.AngleTo(Vector2.Left)) < max - Mathf.Pi/180)
                {
                    float multiplier = Mathf.Abs(Vector2.Left.AngleTo(velocity))/Vector2.Left.AngleTo(velocity);
                    newAngle = Vector2.Left.Rotated(max * multiplier);
                }
            }

            if (newAngle != Vector2.Zero)
            {
                ball.LinearVelocity = Vector2.Zero;
                ball.ApplyCentralImpulse(ballData.force * newAngle);
            }
        }
    }
}
