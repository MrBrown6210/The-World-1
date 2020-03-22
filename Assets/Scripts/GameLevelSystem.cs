using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RoboRyanTron.Unite2017.Variables;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.FirstPerson;

public class GameLevelSystem : MonoBehaviour
{

    public UnityEvent OnPassLevel;
    public UnityEvent OnFailed;

    private static GameLevelSystem _instance;
    public static GameLevelSystem Instance { get { return _instance; } }

    public FloatReference currentLevel;

    public FloatVariable start_run_firsttime;
    public FloatVariable level_timestamp_1;
    public FloatVariable level_timestamp_2;
    public FloatVariable level_timestamp_3;
    public FloatVariable level_timestamp_4;
    public FloatVariable level_timestamp_5;

    public Quest[] quests;

    [HideInInspector] public bool isCutscenePlaying;

    [Header("Camera & cutscene")]
    public PlayableDirector cutsceneBear;
    public GameObject helpedSceneCamera;
    
    [Header("UI")]
    public GameObject gamePlayUI;
    public ChatBubble dialogUI;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        //if (Math.Abs(start_run_firsttime.Value) < Mathf.Epsilon)
        //{
            start_run_firsttime.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        //}

    }

    public void PassLevel(int level)
    {
        if (level > currentLevel)
        {
            Debug.Log("<color=green>PASS LEVEL " + level + "</color>");
            //currentLevel.Variable.SetValue(level);
            OnPassLevel?.Invoke();

            switch (level)
            {
                case 1:
                    //if (Math.Abs(level_timestamp_1.Value) < Mathf.Epsilon) break;
                    StartCoroutine(PlayCutSceneBear());
                    break;
                case 2:
                    //if (Math.Abs(level_timestamp_2.Value) < Mathf.Epsilon) break;
                    StartCoroutine(PlayWalkScene());
                    break;
                case 3:
                    //if (Math.Abs(level_timestamp_3.Value) < Mathf.Epsilon) break;
                    StartCoroutine(PlayHelpedScene());
                    break;
                case 4:
                    //if (Math.Abs(level_timestamp_4.Value) < Mathf.Epsilon) break;
                    StartCoroutine(PlayCollectedAllItemScene());
                    break;
                case 5:
                    //if (Math.Abs(level_timestamp_5.Value) < Mathf.Epsilon) break;
                    StartCoroutine(PlayCrossedScene());
                    break;
                default:
                    break;
            }

        }
    }

    public Quest CurrentQuest()
    {
        if ((int)currentLevel >= quests.Length) return null;
        return quests[(int)currentLevel.Value];
    }

    public Quest PreviousQuest()
    {
        if ((int)currentLevel < 1) return null;
        return quests[(int)currentLevel.Value - 1];
    }

    public void Reset()
    {
        start_run_firsttime.SetValue(0);
        level_timestamp_1.SetValue(0);
        level_timestamp_2.SetValue(0);
        level_timestamp_3.SetValue(0);
        level_timestamp_4.SetValue(0);
        level_timestamp_5.SetValue(0);
    }

    public void Failed(string message)
    {
        OnFailed?.Invoke();
        Pause();
        dialogUI.SetDialog("GM", "<size=120%>Oh no! </size><color=red>bear hurt someone</color> how we can fix this!!!");
        dialogUI.SetButtonEvent(Quit);
        Debug.Log("<color=red>FAILED: " + message + "</color>");
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    IEnumerator PlayCutSceneBear() //Level 1
    {
        Pause();
        level_timestamp_1.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        cutsceneBear.transform.GetChild(0).gameObject.SetActive(true);
        cutsceneBear.Play();
        Debug.Log((float)cutsceneBear.duration);
        yield return new WaitForSeconds((float)cutsceneBear.duration);
        currentLevel.Variable.SetValue(1);
        cutsceneBear.Stop();
        cutsceneBear.transform.GetChild(0).gameObject.SetActive(false);

        dialogUI.SetDialog("GM", "<size=150%>Oh no! </size> bear is awake <color=blue> You need to run!!!</color>");
        dialogUI.SetButtonEvent(Resume);
    }

    IEnumerator PlayWalkScene() //Level 2
    {
        Pause();
        level_timestamp_2.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        currentLevel.Variable.SetValue(2);
        dialogUI.SetDialog("GM", "Now you can move. <color=blue>press WASD to move</color>");
        dialogUI.SetButtonEvent(Resume);
        yield break;
    }

    IEnumerator PlayHelpedScene() //Level 3
    {
        Pause();
        level_timestamp_3.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        currentLevel.Variable.SetValue(3);
        helpedSceneCamera.SetActive(true);
        dialogUI.SetDialog("NPC", "Thank for helping me. if you want to get out of here <size=140%><color=blue>collect 4 wood</color></size> to create bridge. Good luck");
        dialogUI.SetButtonEvent(HelpedSceneResume);
        yield break;
    }

    IEnumerator PlayCollectedAllItemScene()
    {
        Pause();
        level_timestamp_4.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        currentLevel.Variable.SetValue(4);
        dialogUI.SetDialog("GM", "Now go to the river and <color=blue> place wood by pressing 'F'</color>");
        dialogUI.SetButtonEvent(Resume);
        yield break;
    }

    IEnumerator PlayCrossedScene()
    {
        Pause();
        level_timestamp_5.SetValue(new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds());
        currentLevel.Variable.SetValue(5);
        dialogUI.SetDialog("GM", "<color=green>Congrats</color> You passed all level in this chapter!!!");
        dialogUI.SetButtonEvent(Quit);
        yield break;
    }

    private void HelpedSceneResume()
    {
        helpedSceneCamera.SetActive(false);
        Resume();
    }

    private void Pause()
    {
        isCutscenePlaying = true;
        gamePlayUI.SetActive(false);
        FindObjectOfType<CameraFirstPerson>().DisableFirstPersionCamera();
        FindObjectOfType<FirstPersonController>().SetCursorLock(false);
    }

    private void Resume()
    {
        isCutscenePlaying = false;
        gamePlayUI.SetActive(true);
        FindObjectOfType<CameraFirstPerson>().SetCameraToFirstPerson();
        dialogUI.ClearDialog();
        FindObjectOfType<FirstPersonController>().SetCursorLock(true);
    }


}
