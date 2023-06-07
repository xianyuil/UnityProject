using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : UIBase
{
    public Transform startBtn;
    public Transform settingBtn;
    public Transform quitBtn;
    public override void OnOpen(object[] param)
    {
        base.OnOpen(param);
        ClickListener.AddCallBack(startBtn, OnStartGame, null);
        ClickListener.AddCallBack(settingBtn, OnOpenSetGame, null);
        ClickListener.AddCallBack(quitBtn, OnQuitGame, null);
        MessageManger.instance.MessagePut("游戏加载成功");
    }
    private void OnStartGame(Transform trans, object param)
    {

    }
    private void OnOpenSetGame(Transform trans, object param)
    {
        UIManager.instance.Open("SetUI");
    }
    private void OnQuitGame(Transform trans, object param)
    {

    }
}
