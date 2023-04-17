using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> feedbackToOPlay = null;

    private void Awake()
    {
        feedbackToOPlay = new List<Feedback>();
        GetComponents<Feedback>(feedbackToOPlay);
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach(Feedback f in feedbackToOPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in feedbackToOPlay)
        {
            f.FinishFeedback();
        }
    }
}
