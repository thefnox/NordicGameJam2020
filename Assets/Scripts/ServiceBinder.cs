using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct ObjectsWithPrefab
{
    public string name;
    public GameObject prefab;
}

public class ServiceBinder : MonoBehaviour
{
    public ObjectsWithPrefab[] objects;
    public int startMoney = 2000;

    // Use this for service initialization
    void Awake()
    {
        ServiceLocator.Register<IGameService>(new GameHandler(objects, startMoney));
    }
}
