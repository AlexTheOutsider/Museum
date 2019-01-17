using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Humanoid {

    public void Yell()
    {
        print("Orc Yell");
        SingletonTest.Instance.Test();
    }

    private void Start()
    {
        SingletonTest.Instance.Test();
        SingletonTest.Instance.Test();
    }
}
