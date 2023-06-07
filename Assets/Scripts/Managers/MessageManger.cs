using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManger : MonoBehaviour
{
    public static MessageManger instance;
    void Awake()
    {
        if (instance == null) instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MessagePut(string msg)
    {
        UIManager.instance.Open("MessageUI", msg);
    }
}
