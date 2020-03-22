using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDetection : MonoBehaviour
{
    private bool isFinished = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFinished) {
            isFinished = true;
            GameLevelSystem.Instance.PassLevel(5);
        }
    }
}
