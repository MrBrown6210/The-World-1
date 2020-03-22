using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailDetection : MonoBehaviour
{

    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = target.position;
        }
    }

    public void WarpToTarget(GameObject player)
    {
        player.transform.position = target.position;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.tag);
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.transform.position = target.position;
        }
    }

}
