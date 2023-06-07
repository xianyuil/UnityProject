using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : UIBase
{
    public Text text;
    private string msg;
    public override void OnOpen(object[] param)
    {
        base.OnOpen(param);
        msg = param[0] as string;
        if(msg != null && msg != "")
        {
            text.text = msg;
        }
        Timer.instance.AddTimer(2f,() => { UIManager.instance.Close("MessageUI"); });
    }
}
