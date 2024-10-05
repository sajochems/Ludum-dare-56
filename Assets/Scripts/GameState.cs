using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public  static float score;

    static bool fightState = false;
    static bool buildState = true;

    public static void SwitchState()
    {
        fightState = fightState ? false : true;
        buildState = buildState ? false : true;
        Debug.Log("fightState=" +  fightState + ", buildState=" + buildState);
    }

    public static bool FightState()
    {
        return fightState;
    }

    public static bool BuildState()
    {
        return buildState;
    }

    public static void IncreaseScore(float change)
    {
        score += change;
        if (score < 0)
        {
            score = 0;
        }
    }

    public static void DecreaseScore(float change)
    {
        score -= change;
        if (score < 0)
        {
            score = 0;
        }
    }
}
