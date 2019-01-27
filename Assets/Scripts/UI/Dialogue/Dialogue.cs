using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string name;
    public SentenceInfo[] sentences;
}

[Serializable]
public class SentenceInfo
{
    [TextArea(3,10)]
    public string text;
    public int stayTime = 3;
}
