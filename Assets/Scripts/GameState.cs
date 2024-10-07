using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameState : MonoBehaviour
{

    public static int score;
    public static int catfood;
    public static int numberOfCats;

    public static int enemiesLeft;

    static bool fightState = true;
    static bool buildState = false;

    private static GameObject weapon;
    private static GameObject buildMenu;
    

    public static void Init()
    {
        //There has to be a better way to do this
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        buildMenu = GameObject.FindGameObjectWithTag("BuildingPanel");
        enemiesLeft = 0;
        SwitchState("build");

        score = 0;
        catfood = 0;
        numberOfCats = 0;
    }

    public static void SwitchState(string state)
    {

        if (state.Equals("build")){
            fightState = false;
            buildState = true;
            weapon.SetActive(false);
            buildMenu.SetActive(true);
        }

        if (state.Equals("fight"))
        {
            fightState = true;
            buildState = false;
            weapon.SetActive(true);
            buildMenu.SetActive(false);
        }
    }

    public static bool FightState()
    {
        return fightState;
    }

    public static bool BuildState()
    {
        return buildState;
    }

    public static void IncreaseScore(int change)
    {
        score += change;
        catfood += change;
    }


    public static void DecreaseCatfood(int change)
    {
        catfood -= change;
        if (catfood < 0)
        {
            catfood = 0;
        }
    }

    public static void IncreaseCats(int change)
    {
        numberOfCats += change;
        if (numberOfCats < 0)
        {
            numberOfCats = 0;
        }
    }

    public static void DecreaseCats(int change)
    {
        numberOfCats -= change;
        if (numberOfCats < 0)
        {
            numberOfCats = 0;
        }
    }
}
