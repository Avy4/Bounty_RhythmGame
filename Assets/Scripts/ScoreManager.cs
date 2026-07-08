using System;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int combo = 0;
    private bool missedLastBeat = false;

    public int GetScore()
    {
        return score;
    }

    public int GetCombo()
    {
        return combo;
    }

    public bool GetMissedLastBeat()
    {
        return missedLastBeat;
    }

    public void ResetMissedLastBeat()
    {
        missedLastBeat = false;
    }

    public void AddScore(int addedScore)
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
