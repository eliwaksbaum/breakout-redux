
public class TripleBall : Power
{
    public override void DoPower(Paddle paddle)
    {
        Ball.Split();
    }
}