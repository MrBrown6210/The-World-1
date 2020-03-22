using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI talkerUI;
    public TextMeshProUGUI chatUI;
    public Button continueButton;

    public delegate void PassingFunction();

    public void SetDialog(string talker, string chat)
    {
        talkerUI.text = talker;
        chatUI.text = chat;
        gameObject.SetActive(true);
    }

    public void SetButtonEvent(PassingFunction func)
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(delegate { func(); });
    }

    public void ClearDialog()
    {
        talkerUI.text = "";
        chatUI.text = "";
        gameObject.SetActive(false);
    }

}
