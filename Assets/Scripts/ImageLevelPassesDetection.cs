using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Variables;
using UnityEngine;
using UnityEngine.UI;

public class ImageLevelPassesDetection : MonoBehaviour
{

    public Sprite emtpy;
    public Sprite fill;

    public float levelRequirement;
    public FloatVariable currentLevel;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel.Value >= levelRequirement)
        {
            image.sprite = fill;
        } else
        {
            image.sprite = emtpy;
        }
    }
}
