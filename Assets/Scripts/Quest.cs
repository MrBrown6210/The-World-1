﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{

    public string objective;
    [Multiline]
    public string description;
    public string hint;

}
