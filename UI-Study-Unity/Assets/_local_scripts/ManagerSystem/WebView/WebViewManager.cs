using System.Collections;
using System.Collections.Generic;
using TLab.Android.WebView;
using UnityEngine;

public class WebViewManager : MonoBehaviour
{
    [SerializeField] private TLabWebView m_webView;

    private bool WebViewEnable = false;

    public void LoadStudy()
    {
        m_webView.SetUrl("https://docs.google.com/forms/d/e/1FAIpQLSdqXEy5L0AD17Dp-P7ArzmhRTuHqd8PLquC1rQZOZ5m0fFRNg/viewform?usp=sf_link");
        m_webView.StartWebView();
        WebViewEnable = true;
    }


    void Update()
    {
#if UNITY_ANDROID
        if(WebViewEnable)
        {
            m_webView.UpdateFrame();
        }
#endif
    }
}
