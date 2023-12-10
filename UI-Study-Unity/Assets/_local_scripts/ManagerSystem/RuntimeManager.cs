using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeManager : MonoBehaviour
{
    public static RuntimeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public UIManager UI_MANAGER;

    public WebViewManager WEBVIEW_MANAGER;

    public SurveyTimeManager SURVEYTIME_MANAGER;

}
