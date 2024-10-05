using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameState : MonoBehaviour
{
    public  static float score;

    static bool fightState = true;
    static bool buildState = false;

    private static GameObject weapon;

    public static void Init()
    {
        //There has to be a better way to do this
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        SwitchState();
    }

    public static void SwitchState()
    {
        
        fightState = fightState ? false : true;
        buildState = buildState ? false : true;
        Debug.Log("fightState=" +  fightState + ", buildState=" + buildState);
        if (buildState ) { weapon.SetActive(false); }
        if (fightState ) { weapon.SetActive(true); }
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
