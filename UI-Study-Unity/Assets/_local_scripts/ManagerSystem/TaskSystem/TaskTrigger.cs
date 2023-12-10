using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public enum TriggerGoal
    {
        GreenBook,
        RedBook,
        Marker,
        Mouse
    }

    public TriggerGoal Goal;

    public Vector3 CollectPosition;
    public Vector3 CollectRotation;

    private GameObject MoveObject;

    private bool IsMoving = false;

    private void Update()
    {
        if(IsMoving)
        {
            MoveObject.transform.position = Vector3.Lerp(MoveObject.transform.position, CollectPosition, Time.deltaTime * 2.0f);
            MoveObject.transform.rotation = Quaternion.Lerp(MoveObject.transform.rotation, Quaternion.Euler(CollectRotation), Time.deltaTime * 2.0f);

            if(Vector3.Distance(MoveObject.transform.position, CollectPosition) < 0.1f)
            {
                MoveObject.transform.position = CollectPosition;
                
                MoveObject.transform.rotation = Quaternion.Euler(CollectRotation);

                GoalMet();

                IsMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (Goal)
        {
            case TriggerGoal.GreenBook:
                if(other.tag == "GreenBook")
                {
                    MoveObject = other.gameObject;
                    GoalRelease();
                    IsMoving = true;
                }
                break;
            case TriggerGoal.RedBook:
                if (other.tag == "RedBook")
                {
                    MoveObject = other.gameObject;
                    GoalRelease();
                    IsMoving = true;
                }
                break;
            case TriggerGoal.Marker:
                if (other.tag == "Marker")
                {
                    MoveObject = other.gameObject;
                    GoalRelease();
                    IsMoving = true;
                }
                break;
            case TriggerGoal.Mouse:
                if (other.tag == "Mouse")
                {
                    MoveObject = other.gameObject;
                    GoalRelease();
                    IsMoving = true;
                }
                break;
            default:
                break;
        }
    }

    void GoalMet()
    {
        switch (Goal)
        {
            case TriggerGoal.GreenBook:
                RuntimeManager.Instance.UI_MANAGER.TaskLayer.TaskStep3();
                break;
            case TriggerGoal.RedBook:
                RuntimeManager.Instance.UI_MANAGER.TaskLayer.EndTask();
                break;
            case TriggerGoal.Marker:
                RuntimeManager.Instance.UI_MANAGER.TaskLayer.TaskStep2();
                break;
            case TriggerGoal.Mouse:
                RuntimeManager.Instance.UI_MANAGER.TaskLayer.TaskStep4();
                break;
            default:
                break;
        }
    }

    void GoalRelease()
    {
        RuntimeManager.Instance.UI_MANAGER.TaskLayer.LGrabber.ForceRelease(MoveObject.GetComponent<OVRGrabbable>());
        RuntimeManager.Instance.UI_MANAGER.TaskLayer.RGrabber.ForceRelease(MoveObject.GetComponent<OVRGrabbable>());
        //MoveObject.GetComponent<OVRGrabbable>().enabled = false;
        Rigidbody rb = MoveObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        MoveObject.GetComponent<Collider>().enabled = false;
    }

}
