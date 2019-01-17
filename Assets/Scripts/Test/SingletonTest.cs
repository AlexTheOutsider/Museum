using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTest : Singleton<SingletonTest>
{
        public void Test()
        {
                print("Test.");
        }
}
