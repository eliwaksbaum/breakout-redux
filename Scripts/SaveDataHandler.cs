using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveDataHandler
{
    static string path = Godot.OS.GetUserDataDir() + "/high_scores.dat";
    static int maxScores = 5;

    public static bool IsNewRecord(int score)
    {
        if (!File.Exists(path))
        {
            return true;
        }
        else
        {
            int[] scores = ReadScores();
            return scores.Length < maxScores || score > scores[scores.Length - 1];
        }
    }

    public static void AddNewScore(int score, string name)
    {
        List<HighScore> oldScores = GetScores();
        oldScores.Add(new HighScore(score, name));
        oldScores.Sort((a, b) => b.score - a.score);
        if (oldScores.Count > maxScores)
        {
            oldScores.RemoveAt(oldScores.Count - 1);
        }
        WriteScores(oldScores);
    }

    static void WriteScores(List<HighScore> highscores)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile;
        saveFile = File.Open(path, FileMode.OpenOrCreate);
        bf.Serialize(saveFile, highscores);
        saveFile.Close();
    }

    static int[] ReadScores()
    {
        List<HighScore> highscores = GetScores();
        int[] scores = new int[highscores.Count];

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = highscores[i].score;
        }
        return scores;
    }

    public static List<HighScore> GetScores()
    {
        if (!File.Exists(path))
        {
            return new List<HighScore>();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream saveFile = File.Open(path, FileMode.Open);
            List<HighScore> scores = (List<HighScore>) bf.Deserialize(saveFile);
            saveFile.Close();

            return scores;
        }
    }
}