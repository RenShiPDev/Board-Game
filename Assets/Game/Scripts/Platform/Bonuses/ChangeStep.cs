using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeStep : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Material _positiveMat;
    [SerializeField] private Material _negativeMat;
    [SerializeField] private MeshRenderer _triggerRenderer;

    [SerializeField] private int _stepCount = 1;
    [SerializeField] private bool _isForward = true;

    private bool _isAnimation;
    private Material _currentMat;

    private void Start()
    {
        if (_stepCount < 0 || !_isForward)
        {
            _text.text = "-" + Mathf.Abs(_stepCount);
            _triggerRenderer.material = new Material(_negativeMat);
        }
        else
        {
            _text.text = "+" + _stepCount;
            _triggerRenderer.material = new Material(_positiveMat);
        }

        _currentMat = new Material(_triggerRenderer.material);
    }

    public void MakeStep()
    {
        if(_stepCount < 0)
        {
            _stepCount = _stepCount * -1;
            _isForward = false;
        }

        _isAnimation = true;
        _triggerRenderer.material.color = Color.white;
        GameManager.Instance.Player.Mover.Move(_stepCount, _isForward);
    }

    private void Update()
    {
        if (_isAnimation)
        {
            Vector3 currentCoolor = new Vector3(_triggerRenderer.material.color.r, _triggerRenderer.material.color.g, _triggerRenderer.material.color.b);
            Vector3 targetColor = new Vector3(_currentMat.color.r, _currentMat.color.g, _currentMat.color.b);
            Vector3 newColor = Vector3.Slerp(currentCoolor, targetColor, Time.deltaTime * 5);

            _triggerRenderer.material.color = new Color(newColor.x, newColor.y, newColor.z);

            if (_triggerRenderer.material.color == new Color(_currentMat.color.r, _currentMat.color.g, _currentMat.color.b))
            {
                _isAnimation = false;
            }
;        }
    }
}
