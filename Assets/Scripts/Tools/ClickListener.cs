using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler
{
    public delegate void OnClickCallBack(Transform obj, object param);
    public OnClickCallBack mOnClick;
    public object mParam;
    public bool Enable;
    private Dictionary<Transform, Material> mMats;
    private bool mWithScaleEffect;
    private bool mCanKeep;
    private bool mPressing;
    private float mPressTime;
    private bool mKeeping;
    private Dictionary<Transform, Color> mColor;
    private void Awake()
    {
        mOldScale = transform.localScale;
    }
    // Use this for initialization
    void Start()
    {
        mOldScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPressing == true && mCanKeep == true && mKeeping == false)
        {
            if (Time.realtimeSinceStartup - mPressTime > 0.4f)
            {
                mKeeping = true;
            }
        }

        if (mKeeping == true)
        {
            if (mOnClick != null)
            {
                mOnClick(transform, mParam);
            }
        }
    }

    public static void AddCallBack(Transform trans, ClickListener.OnClickCallBack callBack, object param, bool withScaleEffect = true, bool canKeep = false)
    {
        ClickListener cl = trans.GetComponent<ClickListener>();
        if (cl == null)
        {
            cl = trans.gameObject.AddComponent<ClickListener>();
        }
        cl.SetClickEvent(callBack, param);
        cl.Enable = true;
        cl.mWithScaleEffect = withScaleEffect;
        cl.mOldScale = trans.localScale;
        cl.mCanKeep = canKeep;
    }

    public void SetEnable(bool isEnable)
    {
        Enable = isEnable;
        if (mMats == null)
        {
            mMats = new Dictionary<Transform, Material>();
        }
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.enabled = isEnable;
        }
        if (Enable == false)
        {
            mMats.Clear();
            Material mat = new Material(Shader.Find("UI/ImageGreyShader"));
            Graphic[] gras = transform.GetComponentsInChildren<Graphic>();
            for (int i = 0; i < gras.Length; ++i)
            {
                mMats.Add(gras[i].transform, gras[i].material);
                gras[i].material = mat;
            }
        }
        else
        {
            Graphic[] gras = transform.GetComponentsInChildren<Graphic>();
            for (int i = 0; i < gras.Length; ++i)
            {
                gras[i].material = mMats[gras[i].transform];
            }
        }
    }
    public void SetEnable_Txt(bool isEnable)
    {
        Enable = isEnable;
        if (mMats == null)
        {
            mMats = new Dictionary<Transform, Material>();
        }
        if (mColor == null)
            mColor = new Dictionary<Transform, Color>();
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.enabled = isEnable;
        }
        if (Enable == false)
        {
            mMats.Clear();
            mColor.Clear();
            Material mat = new Material(Shader.Find("UI/ImageGreyShader"));
            Image[] gras = transform.GetComponentsInChildren<Image>();
            for (int i = 0; i < gras.Length; ++i)
            {
                mMats.Add(gras[i].transform, gras[i].transform.GetComponent<Graphic>().material);
                gras[i].material = mat;
            }
            Text[] texts = transform.GetComponentsInChildren<Text>();
            for (int i = 0; i < texts.Length; i++)
            {
                mColor.Add(texts[i].transform, texts[i].color);
                texts[i].color = Color.gray;
            }
        }
        else
        {
            Image[] gras = transform.GetComponentsInChildren<Image>();
            if (mMats?.Count > 0)
            {
                for (int i = 0; i < gras.Length; ++i)
                {
                    gras[i].transform.GetComponent<Graphic>().material = mMats[gras[i].transform];
                }
            }

            Text[] texts = transform.GetComponentsInChildren<Text>();
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = mColor[texts[i].transform];
            }
        }
    }
    public void OnPointerClick(PointerEventData data)
    {
        if (Enable == false)
        {
            return;
        }
        transform.localScale = mOldScale;
        if (mOnClick != null)
        {
            mOnClick(transform, mParam);
        }
        //ButtonSound bs = GetComponent<ButtonSound>();
        //if (bs != null && data.clickCount == 1)
        //{
        //    MusicManager.Instance.PlayButtonSound(bs.Sound);
        //}
    }

    public void SetClickEvent(ClickListener.OnClickCallBack callBack, object param)
    {
        mOnClick = callBack;
        mParam = param;
        //string.Format("jin{0}", 100);
    }

    private Vector3 mOldScale;
    public void OnPointerDown(PointerEventData data)
    {
        if (Enable == false)
        {
            return;
        }
        mOldScale = transform.localScale;
        if (mOnClick != null && mWithScaleEffect == true)
        {
            transform.localScale = mOldScale * 0.9f;
        }
        mPressTime = Time.realtimeSinceStartup;
        mPressing = true;
        //MusicManager.Instance.PlaySound("sound_common_button.mp3");
        //ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
        //if (ps != null && ps.Length > 0)
        //{
        //    for (int i = 0; i < ps.Length; ++i)
        //    {
        //        ps[i].Stop();
        //        ps[i].Play();
        //    }
        //}
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (Enable == false)
        {
            return;
        }
        transform.localScale = mOldScale;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (Enable == false)
        {
            return;
        }
        //mOldScale = transform.localScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Enable == false)
        {
            return;
        }
        transform.localScale = mOldScale;
        mKeeping = false;
        mPressing = false;
    }
}
