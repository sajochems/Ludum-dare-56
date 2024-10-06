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
        SwitchState("build");
    }

    public static void SwitchState(string state)
    {

        if (state.Equals("build")){
            fightState = false;
            buildState = true;
            weapon.SetActive(false);
        }

        if (state.Equals("fight"))
        {
            fightState = true;
            buildState = false;
            weapon.SetActive(true);
        }

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
