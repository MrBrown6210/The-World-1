using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDetection : MonoBehaviour
{

    public float radius = 2f;

    private bool isPassedLevelOne = false;

    // Update is called once per frame
    void Update()
    {
        if (isPassedLevelOne) return;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                //TODO: Make Success level 1
                isPassedLevelOne = true;
                GameLevelSystem.Instance.PassLevel(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
