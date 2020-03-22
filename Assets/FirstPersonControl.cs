using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonControl : MonoBehaviour
{

	public float horizontal;
	public float vertical;

    private void Start()
    {
        GameLevelSystem.Instance.PassLevel(2);
    }

    private void FixedUpdate()
	{
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}
