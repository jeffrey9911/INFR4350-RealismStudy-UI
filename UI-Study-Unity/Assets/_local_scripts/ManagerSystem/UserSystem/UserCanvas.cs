using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCanvas : MonoBehaviour
{
    [SerializeField] private GameObject user_canvas;

    [SerializeField] private Transform FollowTransform;

    private bool IsUiFollowing = true;
    private float FollowSpeed = 3f;

    private Vector3 LControllerPosRecord = new Vector3(0f, 0f, 0f);
    private Vector3 RControllerPosRecord = new Vector3(0f, 0f, 0f);
    //private Quaternion ControllerRotRecord = new Quaternion(0f, 0f, 0f, 0f);
    private bool IsControllerPosRecorded = false;
    private Vector3 FollowUIScale = new Vector3(0.001075f, 0.001075f, 0.001075f);
    private bool isDisplayGUI = true;

    [SerializeField] private Transform LeftHandAnchor;
    [SerializeField] private Transform RightHandAnchor;
    [SerializeField] private Transform CentreEye;
    [SerializeField] private GameObject MovePanel;
    [SerializeField] private GameObject ScalePanel;

    public TMP_Text TipText;

    private void Start()
    {
        MovePanel.SetActive(false);
        ScalePanel.SetActive(false);
    }

    private void Update()
    {
        if (IsUiFollowing)
        {
            
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                isDisplayGUI = !isDisplayGUI;
                user_canvas.SetActive(isDisplayGUI);
            }

            bool isLeftHandTrigger = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
            bool isRightHandTrigger = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

            if (isLeftHandTrigger || isRightHandTrigger)
            {
                MovePanel.SetActive(true);
                ScalePanel.SetActive(false);

                if (!IsControllerPosRecorded)
                {
                    LControllerPosRecord = LeftHandAnchor.position;
                    RControllerPosRecord = RightHandAnchor.position;
                    IsControllerPosRecorded = true;
                }

                if (isLeftHandTrigger && isRightHandTrigger)
                {
                    MovePanel.SetActive(false);
                    ScalePanel.SetActive(true);

                    float dDistance = Vector3.Distance(LeftHandAnchor.position, RightHandAnchor.position) - Vector3.Distance(LControllerPosRecord, RControllerPosRecord);
                    dDistance *= 0.001f;

                    FollowUIScale += new Vector3(dDistance, dDistance, dDistance);
                }
                else if (isLeftHandTrigger)
                {
                    FollowTransform.position += LeftHandAnchor.position - LControllerPosRecord;
                }
                else if (isRightHandTrigger)
                {
                    FollowTransform.position += RightHandAnchor.position - RControllerPosRecord;
                }

                LControllerPosRecord = LeftHandAnchor.position;
                RControllerPosRecord = RightHandAnchor.position;
            }
            else
            {
                MovePanel.SetActive(false);
                ScalePanel.SetActive(false);
                IsControllerPosRecorded = false;
            }

            Quaternion lookatRot = Quaternion.LookRotation(CentreEye.position - FollowTransform.position, Vector3.up);
            lookatRot *= Quaternion.Euler(0f, 180f, 0f);
            FollowTransform.rotation = lookatRot;

            user_canvas.transform.position = Vector3.Lerp(user_canvas.transform.position, FollowTransform.position, Time.deltaTime * FollowSpeed);
            user_canvas.transform.rotation = Quaternion.Lerp(user_canvas.transform.rotation, FollowTransform.rotation, Time.deltaTime * FollowSpeed);
            user_canvas.transform.localScale = Vector3.Lerp(user_canvas.transform.localScale, FollowUIScale, Time.deltaTime * FollowSpeed * 2.0f);
        }


    }

    public void StartUserLayer()
    {
        RuntimeManager.Instance.WEBVIEW_MANAGER.LoadStudy();
        string firstmode = RuntimeManager.Instance.UI_MANAGER.CurrentMode == 0 ? "Flat" : "Skeuomorphic";
        TipText.text = $"Your first try was: {firstmode}, Tutorial time was: {Mathf.RoundToInt(RuntimeManager.Instance.SURVEYTIME_MANAGER.SavedTutorialTime)} sec," +
            $"Task time was: {Mathf.RoundToInt(RuntimeManager.Instance.SURVEYTIME_MANAGER.SavedTaskTime)} sec";
        FollowTransform.localPosition = new Vector3(0f, 1f, 1.171f);
    }
}
