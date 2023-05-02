using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceButton : MonoBehaviour
{
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        GameManager.Instance.DiceButton = this;
    }

    public void OnClick()
    {
        GameManager.Instance.Dice.Roll();
        gameObject.SetActive(false);
    }
}
