using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPerson : MonoBehaviour
{
    public Transform firstPersonCameraPositon;

    [HideInInspector] public bool isEnable;

    private void Awake()
    {
        isEnable = true;
    }

    private void LateUpdate()
    {
        if (isEnable)
        {
            //resetCamera();
        }
    }

    public void DisableFirstPersionCamera()
    {
        isEnable = false;
    }

    public void SetCameraToFirstPerson()
    {
        isEnable = true;
    }

    private void resetCamera()
    {
        transform.position = firstPersonCameraPositon.position;
        transform.localRotation = firstPersonCameraPositon.rotation;
        transform.localScale = firstPersonCameraPositon.localScale;
    }

}
