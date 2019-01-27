using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    public string welcome;

    private void OnEnable()
    {
        EventManager.Instance.StartListening("test", () => { LambdaFunction(welcome, welcome); });
        //EventManager.StartListening("test", SomeFunction1);
        //EventManager.StartListening("test", SomeFunction2);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening("test", SomeFunction);
        //EventManager.StopListening("test", SomeFunction1);
        //EventManager.StopListening("test", SomeFunction2);
    }

    private void LambdaFunction(string welcome, string welcome2)
    {
        print(welcome + welcome2);
    }

    private void SomeFunction()
    {
        print("some function was called!");
    }

    private void SomeFunction1()
    {
        print("1111111111111111");
    }

    private void SomeFunction2()
    {
        print("2222222222222222");
    }
}