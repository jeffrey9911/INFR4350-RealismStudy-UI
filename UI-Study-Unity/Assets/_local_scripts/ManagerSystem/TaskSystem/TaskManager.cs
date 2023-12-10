using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Transform CenterEye;
    public Transform TaskUIFollow;

    public Vector3 OrigPos = new Vector3(-4f, 2.7f, 2f);

    public GameObject TaskPanel;
    public TMP_Text TaskText;
    public TMP_Text TipText;

    public GameObject GreenBook;
    public GameObject RedBook;
    public GameObject Marker;
    public GameObject Mouse;

    private int TaskStep = -1;

    public GameObject GoButton;

    public GameObject FlatTask1;
    public GameObject FlatTask2;
    public GameObject FlatTask3;
    public GameObject FlatTask4;

    public GameObject SkeuTask1;
    public GameObject SkeuTask2;
    public GameObject SkeuTask3;
    public GameObject SkeuTask4;

    public OVRGrabber LGrabber;
    public OVRGrabber RGrabber;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIFollow();
    }

    void UIFollow()
    {
        Quaternion lookatRot = Quaternion.LookRotation(CenterEye.transform.position - this.transform.position, Vector3.up);

        this.transform.localRotation = Quaternion.Lerp(this.transform.rotation, lookatRot, Time.deltaTime * 5.0f);

        if(TaskStep > 0)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, TaskUIFollow.position, Time.deltaTime * 5.0f);
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                TaskPanel.SetActive(!TaskPanel.activeSelf);
            }
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, OrigPos, Time.deltaTime * 5.0f);
        }
    }

    public void StartTaskLayer()
    {
        SwitchOff();
        SetGrabbables();

        TaskStep = 0;

        TaskPanel.SetActive(true);
        TaskText.text = "The room is a mess\nYour task is to organize the objects\nPlease follow the guide\nand put the things back :)";
        GoButton.SetActive(true);



    }

    public void TaskStep1()
    {
        RuntimeManager.Instance.SURVEYTIME_MANAGER.StartTaskTimer();
        SwitchOff();

        TipText.text = "Tip: You can always use Left Oculus (≡) to Hide/Display the guide.";

        Marker.GetComponent<OVRGrabbable>().enabled = true;

        if(RuntimeManager.Instance.UI_MANAGER.CurrentMode == 0)
        {
            SkeuTask1.SetActive(true);
        }
        else if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 1)
        {
            FlatTask1.SetActive(true);
        }

        TaskStep = 1;
    }

    public void TaskStep2()
    {
        SwitchOff();

        TipText.text = "Tip: You can always use Left Oculus (≡) to Hide/Display the guide.";

        GreenBook.GetComponent<OVRGrabbable>().enabled = true;

        if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 0)
        {
            SkeuTask2.SetActive(true);
        }
        else if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 1)
        {
            FlatTask2.SetActive(true);
        }

        TaskStep = 2;
    }

    public void TaskStep3()
    {
        SwitchOff();

        TipText.text = "Tip: You can always use Left Oculus (≡) to Hide/Display the guide.";

        Mouse.GetComponent<OVRGrabbable>().enabled = true;

        if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 0)
        {
            SkeuTask3.SetActive(true);
        }
        else if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 1)
        {
            FlatTask3.SetActive(true);
        }

        TaskStep = 3;
    }

    public void TaskStep4()
    {
        SwitchOff();

        TipText.text = "Tip: You can always use Left Oculus (≡) to Hide/Display the guide.";

        RedBook.GetComponent<OVRGrabbable>().enabled = true;

        if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 0)
        {
            SkeuTask4.SetActive(true);
        }
        else if (RuntimeManager.Instance.UI_MANAGER.CurrentMode == 1)
        {
            FlatTask4.SetActive(true);
        }

        TaskStep = 4;
    }

    public void EndTask()
    {
        SwitchOff();

        RuntimeManager.Instance.SURVEYTIME_MANAGER.StopTaskTimer();

        RuntimeManager.Instance.UI_MANAGER.StartUserCanvas();
    }



    void SwitchOff()
    {
        TaskText.text = "";
        TipText.text = "";
        GoButton.SetActive(false);

        //GreenBook.GetComponent<OVRGrabbable>().enabled = false;
        //RedBook.GetComponent<OVRGrabbable>().enabled = false;
        //Marker.GetComponent<OVRGrabbable>().enabled = false;
        //Mouse.GetComponent<OVRGrabbable>().enabled = false;

        FlatTask1.SetActive(false);
        FlatTask2.SetActive(false);
        FlatTask3.SetActive(false);
        FlatTask4.SetActive(false);

        SkeuTask1.SetActive(false);
        SkeuTask2.SetActive(false);
        SkeuTask3.SetActive(false);
        SkeuTask4.SetActive(false);
    }

    void SetGrabbables()
    {
        GreenBook.transform.position = new Vector3(2.1f, 2.4f, -1.7f);
        GreenBook.transform.rotation = Quaternion.Euler(90f, 0f, -232f);
        SetRigidbody(GreenBook);
        

        RedBook.transform.position = new Vector3(-3.1f, 3f, -2.4f);
        RedBook.transform.rotation = Quaternion.Euler(50f, -90f, -90f);
        SetRigidbody(RedBook);

        Marker.transform.position = new Vector3(-2.8f, 2.1f, 1f);
        Marker.transform.rotation = Quaternion.Euler(-43f, 90f, -90f);
        SetRigidbody(Marker);

        Mouse.transform.position = new Vector3(1.5f, 2.4f, 2.8f);
        Mouse.transform.rotation = Quaternion.Euler(50f, -53f, -90f);
        SetRigidbody(Mouse);
    }

    void SetRigidbody(GameObject gobj)
    {
        Rigidbody rb = gobj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.isKinematic = false;
        gobj.GetComponent<Collider>().enabled = true;
    }
}
