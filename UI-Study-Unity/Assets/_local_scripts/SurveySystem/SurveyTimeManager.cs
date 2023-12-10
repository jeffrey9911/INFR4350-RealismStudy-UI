using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyTimeManager : MonoBehaviour
{
    public float TutorialTime = 0f;
    public float TaskTime = 0f;

    private bool isCountingTutorialTime = false;
    private bool isCountingTaskTime = false;

    public float SavedTutorialTime = 0f;
    public float SavedTaskTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (isCountingTutorialTime)
        {
            TutorialTime += Time.deltaTime;
        }

        if (isCountingTaskTime)
        {
            TaskTime += Time.deltaTime;
        }
    }

    public void StartTutorialTimer()
    {
        isCountingTutorialTime = true;
    }

    public void StopTutorialTimer()
    {
        isCountingTutorialTime = false;
    }

    public void StartTaskTimer()
    {
        isCountingTaskTime = true;
    }

    public void StopTaskTimer()
    {
        isCountingTaskTime = false;
    }

    public void SaveTimer()
    {
        SavedTutorialTime = TutorialTime;
        SavedTaskTime = TaskTime;
    }

    public void ResetTimer()
    {
        TutorialTime = 0f;
        TaskTime = 0f;
    }
}
