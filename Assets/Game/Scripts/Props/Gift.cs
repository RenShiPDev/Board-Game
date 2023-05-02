using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] public readonly string _name;

    public void GetGift()
    {
        PlayerPrefs.SetInt(_name, PlayerPrefs.GetInt(_name) + 1);
        ChangeProp();
    }

    public virtual void ChangeProp()
    {
        Debug.Log(PlayerPrefs.GetInt(_name));
    }
}
