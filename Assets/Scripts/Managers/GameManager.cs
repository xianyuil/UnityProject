using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        ManagersInit();
        UIManager.instance.Open("StartUI");
    }

    void Update()
    {
        
    }
    private void ManagersInit()
    {
        Timer timer = transform.GetComponent<Timer>();
        if(timer == null)transform.gameObject.AddComponent<Timer>();
        MessageManger messageManger = transform.GetComponent<MessageManger>();
        if (messageManger == null) transform.gameObject.AddComponent<MessageManger>();
    }
}
