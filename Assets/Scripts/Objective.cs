using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{

    public Text objectiveText;
    public Text descriptionText;

    // Update is called once per frame
    void Update()
    {
        Quest currentQuest = GameLevelSystem.Instance.CurrentQuest();
        if (currentQuest != null)
        {
            objectiveText.text = currentQuest.objective;
            descriptionText.text = currentQuest.description;
        }
        else
        {
            objectiveText.text = "You done all the quests";
            descriptionText.text = "Next level will be release soon.";
        }
    }
}
