using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesMenu : MonoBehaviour
{
    public Animator MovesMenuAnimator;
    public bool AnimCounter = false;
    private void Start()
    {
        MovesMenuAnimator = GetComponent<Animator>();

    }

    public void MenuAnimation()
    {
        if(AnimCounter)
        {
            MovesMenuAnimator.ResetTrigger("Showing");
            MovesMenuAnimator.SetTrigger("Hidden");
            AnimCounter = false;
            Debug.Log("hidden");
        }
        else
        {
            MovesMenuAnimator.ResetTrigger("Hidden");
            MovesMenuAnimator.SetTrigger("Showing");
            AnimCounter = true;
            Debug.Log("show");
        }
    }

    public void SetBool()
    {
        if (AnimCounter)
            AnimCounter = false;
        else
            AnimCounter = true;
    }
}
