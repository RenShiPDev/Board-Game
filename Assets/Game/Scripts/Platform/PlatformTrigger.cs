using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTrigger : MonoBehaviour
{
    public UnityEvent OnStep;

    public void ActivateTrigger() 
    {
        OnStep?.Invoke();
    }
}