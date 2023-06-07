using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get{
            if (_instance == null) _instance = new UIManager();
            return _instance;
        }
    }
    
    private string PATH = "Prefabs/UI/";
    private Transform mUIRoot;
    private Dictionary<string, UIBase> mUIDic;
    public Transform UIRoot
    {
        get
        {
            return mUIRoot;
        }
        set
        {
            GameObject.Destroy(mUIRoot.gameObject);
            mUIRoot = value;
        }
    }
    public UIManager()
    {
        mUIDic = new Dictionary<string, UIBase>();
        if (mUIRoot == null)
        {
            GameObject go = Resources.Load<GameObject>(PATH + "UIRoot");
            Debug.Log(go.name);
            go = GameObject.Instantiate<GameObject>(go);
            go.name = "UIRoot";
            mUIRoot = go.transform;
            go.AddComponent<DontDestory>();
        }
    }
    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="uiName">ui名</param>
    /// <param name="callback">开启后回调</param>
    /// <returns>ui路径</returns>
    public string Open(string uiName,params object[] param)
    {
        if (mUIDic.ContainsKey(uiName))
        {
            Debug.Log(string.Format("This ui is opened,ui name is {0}", uiName));
            return uiName;
        }
        GameObject go = Resources.Load<GameObject>(PATH + uiName );
        if (go == null)
        {
            Debug.Log(string.Format("Not find ui,ui path is {0}", uiName));
            return uiName; 
        }
        go = GameObject.Instantiate<GameObject>(go);
        go.name = uiName;
        UIBase uiBase = go.GetComponent<UIBase>();
        if (uiBase == null)
        {
            Debug.Log("this ui has no the component:uibase");
            return null;
        }
        uiBase.Init();
        sortByLayer(uiBase);
        go.transform.SetParent(mUIRoot);
        mUIDic.Add(uiName, go.GetComponent<UIBase>());
        Debug.Log(string.Format("UI open success,ui name is {0}", uiName));
        uiBase.OnOpen(param);
        return PATH + uiName;
    }
    /// <summary>
    /// 关闭ui
    /// </summary>
    /// <param name="uiName">ui名</param>
    /// <param name="callback">关闭后回调</param>
    /// <returns>ui路径</returns>
    public string Close(string uiName, System.Action callback = null)
    {
        if (!mUIDic.ContainsKey(uiName))
        {
            Debug.Log(string.Format("This ui is not open,ui name is {0}", uiName));
            return PATH + uiName;
        }
        GameObject go = mUIRoot.Find(uiName).gameObject;
        if(go != null)
        {
            GameObject.Destroy(go);
            mUIDic.Remove(uiName);
        }
        return PATH + uiName;
    }
    /// <summary>
    /// 关闭所有ui
    /// </summary>
    public void CloseAll()
    {
        List<string> list = new List<string>();
        foreach (var item in mUIDic)
        {
            list.Add(item.Key);
        }
        for (int i = 0; i < list.Count; ++i)
        {
            UIBase uiBase = mUIDic[list[i]];
            uiBase.OnClose(null);
            GameObject.Destroy(uiBase.gameObject);
            mUIDic.Remove(list[i]);
        }
    }
    /// <summary>
    /// 关闭除了uiName的其余所有ui
    /// </summary>
    /// <param name="uiName">ui名</param>
    public void CloseAllBut(string uiName)
    {
        List<string> list = new List<string>();
        foreach (var item in mUIDic)
        {
            if (item.Key != uiName)
            {
                list.Add(item.Key);
            }
        }
        for (int i = 0; i < list.Count; ++i)
        {
            UIBase uiBase = mUIDic[list[i]];
            uiBase.OnClose(null);
            GameObject.Destroy(uiBase.gameObject);
            mUIDic.Remove(list[i]);
        }
    }
    /// <summary>
    /// 隐藏所有ui
    /// </summary>
    /// <param name="hide">是否隐藏</param>
    public void HideAll(bool hide)
    {
        foreach (var item in mUIDic)
        {
            item.Value.gameObject.SetActive(!hide);
        }
    }
    /// <summary>
    /// 隐藏除了uiName外的所有ui
    /// </summary>
    /// <param name="uiName">ui名</param>
    /// <param name="hide">是否隐藏</param>
    public void HideAllBut(string uiName, bool hide)
    {
        foreach (var item in mUIDic)
        {
            if (item.Key != uiName)
            {
                item.Value.gameObject.SetActive(!hide);
            }
        }
    }
    /// <summary>
    /// 未新开启的ui排显示顺序
    /// </summary>
    /// <param name="uiBase">ui</param>
    private void sortByLayer(UIBase uiBase)
    {
        int maxDepth = getBaseDepth(uiBase.Layer);
        foreach (var item in mUIDic)
        {
            if (item.Value.Layer == uiBase.Layer && item.Value.Depth > maxDepth)
            {
                maxDepth = item.Value.Depth;
            }
        }
        uiBase.Depth = maxDepth + 10;
    }
    /// <summary>
    /// 获取ui的显示层级
    /// </summary>
    /// <param name="layer">ui的layer</param>
    /// <returns>ui显示层级</returns>
    private int getBaseDepth(UIBase.UILayer layer)
    {
        switch (layer)
        {
            case UIBase.UILayer.Bottom:
                return 0;
            case UIBase.UILayer.Midle:
                return 1000;
            case UIBase.UILayer.Top:
                return 2000;
            case UIBase.UILayer.Guide:
                return 3000;
        }
        return -1000;
    }
}
