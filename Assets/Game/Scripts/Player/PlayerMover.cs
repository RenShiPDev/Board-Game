using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Platform _currentPlatform;
    private PlatformTrigger _currentTrigger;
    private Rigidbody _rb;

    private Vector3 _startPos;
    private Vector3 _targetPos;

    private int _currentStep;

    private bool _steping;
    private bool _moving;
    private bool _isForward;

    private void Start()
    {
        _startPos = transform.position;
        _rb = GetComponent<Rigidbody>();
    }

    [ContextMenu("RollDice")]
    public void RollDice()
    {
        int steps = GameManager.Instance.Dice.Roll();
        Move(steps);
        Debug.Log(steps);
    }
    [ContextMenu("Move1")]
    public void Move1()
    {
        Move(1);
    }

    public void Move(int stepCount, bool isForward = true)
    {
        _isForward = isForward;
        _currentStep = stepCount;
        MakeStep();
    }

    public void MakeStep()
    {
        if(_currentPlatform != null) _currentPlatform.IsActive = !_isForward;
        FindClosestPlatform(_isForward);
        gameObject.transform.LookAt(_currentPlatform.transform);
        gameObject.transform.rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y, 0);

        _targetPos = transform.position;
        _targetPos += transform.forward;
        _steping = true;
    }

    private void FindClosestPlatform(bool isForward)
    {
        var prevPlatform = _currentPlatform;
        float minDistance = Mathf.Infinity;
        foreach(Platform platform in GameManager.Instance.Platforms)
        {
            float distance = (gameObject.transform.position - platform.gameObject.transform.position).magnitude;
            if (distance <= minDistance && platform.IsActive == _isForward && _currentPlatform != platform)
            {
                minDistance = distance;
                _currentPlatform = platform;
            }
        }
        _currentPlatform.IsActive = !_isForward;

        if(_currentPlatform == prevPlatform)
        {
            GameManager.Instance.EndGame();
            _steping = false;
            _moving = false;
            this.enabled = false;
        }
    }

    public void FixedUpdate()
    {
        if (_steping)
        {
            if ((transform.position - _targetPos).magnitude > 0.01f)
            {
                _rb.MovePosition(Vector3.Slerp(transform.position, _targetPos, _speed*Time.deltaTime));
            }
            else
            {
                _steping = false;
                _currentStep--;
                GameManager.Instance.StepsCount++;
                GameManager.Instance.DiceButton.gameObject.SetActive(false);

                if (!_isForward)
                {
                    _currentPlatform.IsActive = true;
                }

                transform.position = _targetPos;
                if (_currentStep <= 0)
                {
                    var trigger = _currentPlatform.GetComponentInChildren<PlatformTrigger>();
                    if (trigger != null)
                    {
                        _currentTrigger = trigger;
                    }
                    
                    _currentTrigger?.ActivateTrigger();

                    if(_currentTrigger == null)
                        GameManager.Instance.DiceButton.gameObject.SetActive(true);

                    _currentTrigger = null;
                }
                else
                    MakeStep();
            }
        }
    }
}
