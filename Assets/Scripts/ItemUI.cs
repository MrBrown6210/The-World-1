using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    public Text numberText;

    private ItemDetection itemDetection;

    // Start is called before the first frame update
    void Start()
    {
        itemDetection = FindObjectOfType<ItemDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = itemDetection.GetCurrentWood() + "/4";
    }
}
