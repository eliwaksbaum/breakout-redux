using Godot;

public class AudioPlayer
{
    public static AudioStreamPlayer Clip(Viewport root, AudioStream clip)
    {
        AudioStreamPlayer player = new AudioStreamPlayer();
        player.Stream = clip;
        root.AddChild(player);
        player.Connect("finished", player, "queue_free");
        return player;
    }
}