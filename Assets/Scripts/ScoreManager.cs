using System;

public static class ScoreManager
{
    private static int score = 0;
    private static int combo = 0;
    private static bool missedLastBeat = false;

    public static int GetScore()
    {
        return score;
    }

    public static int GetCombo()
    {
        return combo;
    }

    public static bool GetMissedLastBeat()
    {
        return missedLastBeat;
    }

    public static void ResetMissedLastBeat()
    {
        missedLastBeat = false;
    }

    public static void AddScore(int addedScore)
    {
        if (addedScore == 300 || addedScore == 100)
        {
            combo += 1;
        }
        else
        {
            combo = 0;
            missedLastBeat = true;
        }

        score += addedScore + (int)Math.Round(addedScore * 1.5 * combo);
    }
}
