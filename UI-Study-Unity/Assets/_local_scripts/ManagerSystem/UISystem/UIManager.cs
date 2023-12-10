using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public ConfigCanvas ConfigLayer;

    [SerializeField] public TutorialManager TutorialLayer;

    [SerializeField] public UserCanvas UserLayer;

    [SerializeField] public TaskManager TaskLayer;

    [SerializeField] public GameObject UserLevel;
    [SerializeField] public GameObject TutorialLevel;

    public int CurrentMode = -1; // 0: Skeuomorphic, 1: Flat
    public int CurrentHeadsetType = -1; // 0: Oculus Quest 2, 1: Meta Quest Pro

    public int CurrentTry = -1;

    float ResetTimer = 0f;

    // Start with Config Canvas
    private void Start()
    {
        if (OVRManager.isHmdPresent)
        {
            RuntimeManager.Instance.UI_MANAGER.ConfigLayer.UISystemMessage($"Current Device: {OVRPlugin.GetSystemHeadsetType().ToString()}");

            if(OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Quest_2)
            {
                CurrentHeadsetType = 0;
            }
            else if(OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Meta_Quest_Pro)
            {
                CurrentHeadsetType = 1;
            }
        }

        RestartLayers();
    }

    private void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            RestartLayers();
        }
    }

    public void RestartLayers()
    {
        ConfigLayer.gameObject.SetActive(true);
        TutorialLayer.gameObject.SetActive(false);
        UserLayer.gameObject.SetActive(false);
        TaskLayer.gameObject.SetActive(false);

        CurrentMode = -1;
        CurrentTry = -1;


        ActiveTutorialLevel();
    }

    // Tutorial Canvas
    [ContextMenu("StartTutorialCanvas")]
    public void StartTutorialCanvas()
    {
        ConfigLayer.gameObject.SetActive(false);
        UserLayer.gameObject.SetActive(false);
        TutorialLayer.gameObject.SetActive(true);
        TaskLayer.gameObject.SetActive(false);

        TutorialLayer.StartTutorial();
    }

    public void StartTaskCanvas()
    {
        ConfigLayer.gameObject.SetActive(false);
        UserLayer.gameObject.SetActive(false);
        TutorialLayer.gameObject.SetActive(false);
        TaskLayer.gameObject.SetActive(true);

        TaskLayer.StartTaskLayer();
    }

    public void StartUserCanvas()
    {
        if(CurrentTry == 1)
        {
            RuntimeManager.Instance.SURVEYTIME_MANAGER.SaveTimer();

            ActiveTutorialLevel();

            if (CurrentMode == 0)
            {
                ConfigLayer.StartFlat();
            }
            else if (CurrentMode == 1)
            {
                ConfigLayer.StartSkeuomorphic();
            }

            CurrentTry = 2;

            return;
        }
        else if(CurrentTry > 1)
        {
            ConfigLayer.gameObject.SetActive(false);
            UserLayer.gameObject.SetActive(true);
            TutorialLayer.gameObject.SetActive(false);
            TaskLayer.gameObject.SetActive(false);

            UserLayer.StartUserLayer();
            ActiveTutorialLevel();
        }
    }

    public void ActiveTutorialLevel()
    {
        UserLevel.SetActive(false);
        TutorialLevel.SetActive(true);

        RuntimeManager.Instance.SURVEYTIME_MANAGER.ResetTimer();
    }

    public void ActiveUserLevel()
    {
        UserLevel.SetActive(true);
        TutorialLevel.SetActive(false);
    }

    [ContextMenu("StartTask")]
    public void StartTask()
    {
        ActiveUserLevel();

        StartTaskCanvas();
    }

}
