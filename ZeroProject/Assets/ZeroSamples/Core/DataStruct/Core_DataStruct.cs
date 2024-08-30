using System.Collections.Generic;
using UnityEngine;
using ZeroEngine;

public class Core_DataStruct : MonoBehaviour
{
    [SerializeField] public Dictionary<string, string> N_Dict = new Dictionary<string, string>();

    [SerializeField]
    public SerializableDictionary<string, string> S_Dict = new SerializableDictionary<string, string>()
    {
        { "unity", "C#" },
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // LinkedListTest();
        SerializableDictionaryTest();
    }

    void LinkedListTest()
    {
        GameFrameworkLinkedList<string> linkedList = new GameFrameworkLinkedList<string>();
        var firstNode = linkedList.AddFirst("Hello");
        linkedList.AddAfter(firstNode, "World");

        foreach (var item in linkedList)
        {
            Debug.Log(item);
        }
    }

    void SerializableDictionaryTest()
    {
        S_Dict.Add("unreal", "C++");

        foreach (var item in S_Dict)
        {
            Debug.Log(item.Key + ":" + item.Value);
        }
    }
}
