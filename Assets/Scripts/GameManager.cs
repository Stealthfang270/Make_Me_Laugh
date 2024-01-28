using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int difficulty = 0;
    public float increaseDifficultyTime = 30;
    public float timeUntilNextDifficultyIncrease = 30;
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

        Debug.Log("Difficulty: " + LifeTracker.difficulty);
        Debug.Log("Lives: " + LifeTracker.life);


        if (LifeTracker.life <= 0)
        {
            LifeTracker.life = 5;
            LifeTracker.difficulty = 0;
            SceneManager.LoadScene("GameOver");
        }
    }
}
