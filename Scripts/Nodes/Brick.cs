using Godot;
using System;

public class Brick : StaticBody2D
{
    [Export]
    public IntValue score;
    [Export]
    public AudioStream hit1, hit2, laserhit, die;

    Sprite sprite;
    int health;
    Random random = new Random();

    public static event Action BrickDeath;

    public override void _Ready()
    {
        sprite = GetNode<Sprite>("Sprite");
        health = sprite.Frame + 1;
    }

    public void OnBodyExited(PhysicsBody2D other)
    {
        if (other.IsInGroup("Balls"))
        {
            if (random.Next(2) == 0)
            {
                AudioPlayer.Clip(GetTree().Root, hit1).Play();
            }
            else
            {
                AudioPlayer.Clip(GetTree().Root, hit2).Play();
            }
            Hit();
        }
    }
    public void OnAreaEntered(Area2D other)
    {
        if (other.Name.Find("Laser") != -1)
        {
            AudioPlayer.Clip(GetTree().Root, laserhit).Play();
            Hit();
            other.QueueFree();
        }
    }

    void Hit()
    {
        if (health > 0)
        {
            health -= 1;
            score.Value += 5;
        }
        sprite.Frame -= sprite.Frame == 0 ? 0 : 1;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        QueueFree();
        AudioPlayer.Clip(GetTree().Root, die).Play();
        if (BrickDeath != null)
        {
            BrickDeath();
        }
    }
}
