using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        GameManager.Instance.CoinsText = this;
    }

    public void SetCoinsText(string text)
    {
        _text.text = text;
    }
}
