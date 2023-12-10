using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCubeEvent : MonoBehaviour
{
    public TutorialManager tutorialManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "CylinderGreen")
        {
            tutorialManager.OnCubeMoved();
        }
    }
}
