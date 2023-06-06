using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public enum UILayer
    {
        Bottom,
        Midle,
        Top,
        Guide,
    }
    public UILayer Layer = UILayer.Midle;

    public int Depth
    {
        get
        {
            return GetComponent<Canvas>().sortingOrder;
        }
        set
        {
            GetComponent<Canvas>().sortingOrder = value;

        }
    }

    public void Init()
    {
        GetComponent<Canvas>().overrideSorting = true;
    }

    private void Awake()
    {

    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnDisable()
    {

    }

    private void OnEnable()
    {

    }

    public virtual void OnOpen(object[] param)
    {

    }

    public virtual void OnClose(object[] param)
    {

    }

    public virtual void OnRefresh()
    {

    }
}
