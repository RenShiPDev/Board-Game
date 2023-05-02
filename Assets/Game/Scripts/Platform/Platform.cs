using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool IsActive = true;
    public PlatformTrigger Trigger;

    private void OnEnable()
    {
        GameManager.Instance.Platforms.Add(this);
        Trigger = gameObject.GetComponentInChildren<PlatformTrigger>();
    }
}