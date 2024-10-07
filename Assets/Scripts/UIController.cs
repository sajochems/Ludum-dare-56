using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text catFoodAmount;
    public TMP_Text catAmount;

    public TMP_Text scoreAmount;

    public TMP_Text houseHealth;

    private Home home;

    private void Start()
    {
        catFoodAmount.SetText("0");
        catAmount.SetText("0");
        scoreAmount.SetText("0");
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();
    }

    private void Update()
    {
        int catFood = GameState.catfood;
        catFoodAmount.SetText(catFood.ToString());

        int score = GameState.score;
        scoreAmount.SetText(score.ToString());

        int cats = GameState.numberOfCats;
        catAmount.SetText(cats.ToString());

        int homeHealth = home.health;
        houseHealth.SetText(homeHealth.ToString() + "/100");
    }

}
