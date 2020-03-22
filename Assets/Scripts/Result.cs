using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    public Text objectiveText;
    public Text timeText;

    public void Enable()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Quest currentQuest = GameLevelSystem.Instance.PreviousQuest();
        objectiveText.text = currentQuest.objective;
        //timeText.text = currentQuest;
    }

    public void Disable()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
