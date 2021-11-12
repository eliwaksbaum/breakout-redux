
public class LaserPower : Power
{
    bool stopped;

    public override void DoPower(Paddle paddle)
    {
        if (!stopped)
        {
            paddle.GetNode<LaserShooter>("LaserShooter").Start();
        }
    }

    new public void Stop()
    {
        base.Stop();
        stopped = true;
    }
}