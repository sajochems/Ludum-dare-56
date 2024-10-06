using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text catFoodAmount;
    public TMP_Text catAmount;

    public TMP_Text scoreAmount;

    private void Start()
    {
        catFoodAmount.SetText("0");
        catAmount.SetText("0");
        scoreAmount.SetText("0");
    }

    private void Update()
    {
        int catFood = GameState.catfood;
        catFoodAmount.SetText(catFood.ToString());

        int score = GameState.score;
        scoreAmount.SetText(score.ToString());
    }

}
