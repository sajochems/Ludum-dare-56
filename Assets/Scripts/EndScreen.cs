using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TMP_Text score;

    private void Start()
    {
        score.SetText(GameState.score.ToString());
    }

    public void OnReturnButton()
    {
        SceneManager.LoadScene(0);
    }

}
