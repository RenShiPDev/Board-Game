using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGift : MonoBehaviour
{
    public void OnActivateTrigger()
    {
        if(gameObject.transform.parent.GetComponentInChildren<Gift>() != null)
            gameObject.transform.parent.GetComponentInChildren<Gift>().GetGift();
    }
}
