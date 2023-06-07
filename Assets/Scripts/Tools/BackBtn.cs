using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    UIBase panelUI;
    // Start is called before the first frame update
    void Start()
    {
        panelUI = transform.parent.GetComponent<UIBase>();
        if (panelUI == null)
        {
            Debug.Log("Not panelUI,Plealse Check Parent");
            return;
        }
        ClickListener.AddCallBack(transform, (Transform trans, object param) => { UIManager.instance.Close(panelUI.name);}, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
