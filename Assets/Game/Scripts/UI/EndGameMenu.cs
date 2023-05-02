using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _stepsText;

    private void Start()
    {
        GameManager.Instance.EndGameMenu = this;
        gameObject.SetActive(false);
    }

    public void SetStepsText()
    {
        _stepsText.text = "Шагов сделано: " + GameManager.Instance.StepsCount;
    }
}
