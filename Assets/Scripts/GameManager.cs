using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int difficulty = 0;
    public float increaseDifficultyTime = 15;
    public float timeUntilNextDifficultyIncrease = 15;
    private void Update()
    {
        if (timeUntilNextDifficultyIncrease > 0)
        {
            timeUntilNextDifficultyIncrease -= Time.deltaTime;
        }
        if (timeUntilNextDifficultyIncrease < 0)
        {
            timeUntilNextDifficultyIncrease = increaseDifficultyTime;
            difficulty++;
        }

        LifeTracker.difficulty = difficulty;


        if (LifeTracker.life <= 0)
        {
            //SceneManager.LoadScene("LoseScreen");
            Debug.Log("You suck lmao");
        }
    }
}
