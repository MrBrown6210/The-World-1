using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveLog : MonoBehaviour
{

    public Text objectiveDetailText;
    public Text descriptionText;

    public void Enable()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Quest currentQuest = GameLevelSystem.Instance.CurrentQuest();
        objectiveDetailText.text = currentQuest.description;
        //timeText.text = currentQuest;
    }

    public void Disable()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
