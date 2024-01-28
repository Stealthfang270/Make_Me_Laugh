using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    void Update()
    {
        tmp.text = "Lives: " + LifeTracker.life;
    }
}
