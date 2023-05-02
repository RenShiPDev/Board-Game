using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RestartButton = this;
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        GameManager.Instance.StartGame();
    }
}
