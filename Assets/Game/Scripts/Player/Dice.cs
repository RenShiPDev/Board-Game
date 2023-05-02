using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Transform _hiddenScreenPos;
    [SerializeField] private Transform _endScreenPos;

    [SerializeField] private float _rollingTime = 1;
    [SerializeField] private float _showTime = 1;


    [SerializeField] private float _speed = 30;
    [SerializeField] private float _moveSpeed = 10;

    private Dictionary<int, Vector3> _diceSides = new Dictionary<int, Vector3>()
    {
        { 1, new Vector3(180,0,0)},
        { 2, new Vector3(-90,0,0)},
        { 3, new Vector3(0,90,90)},
        { 4, new Vector3(0,90,-90)},
        { 5, new Vector3(90,0,0)},
        { 6, new Vector3(0,0,0)},
    };

    private Vector3 _targetVector;

    private float _timer;
    private int _result;

    private bool _isFloating;
    private bool _isRolling;
    private bool _playerMoved = true;

    private void Start()
    {
        transform.position = _hiddenScreenPos.position;
        GameManager.Instance.Dice = this;
    }

    public int Roll()
    {
        _isRolling = true;
        _playerMoved = false;
        _isFloating = true;

        _result = Random.Range(1, 7);
        _timer = 0;
        return _result;
    }

    private void Update()
    {
        if (!_isFloating)
        {
            RollAnimation();
            if (!_isRolling)
            {
                if (_timer >= _showTime)
                {
                    MoveToPos(_hiddenScreenPos.position);
                    if (!_playerMoved)
                    {
                        _playerMoved = true;
                        GameManager.Instance.Player.Mover.Move(_result);
                    }
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }
        else
        {
            if (_isRolling)
            {
                MoveToPos(_endScreenPos.position);
            }
        }
    }

    private void MoveToPos(Vector3 target)
    {
        if((transform.position - target).magnitude <= 0.1f)
        {
            _isFloating = false;
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, target, _moveSpeed * Time.deltaTime);
        }
    }

    private void RollAnimation()
    {
        if (_isRolling)
        {
            _timer += Time.deltaTime;

            if ((transform.rotation.normalized.eulerAngles - Quaternion.Euler(_targetVector).normalized.eulerAngles).magnitude <= 0.1f)
            {
                _targetVector = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_targetVector), Time.deltaTime*_speed);
            }

            if(_timer >= _rollingTime)
            {
                _isRolling = false;
                _timer = 0;
                if (_diceSides.TryGetValue(_result, out Vector3 targetPos))
                {
                    _targetVector = targetPos;
                }
                else
                {
                    _result = 1;
                    _targetVector = new Vector3(180, 0, 0);
                }
            }
        }
        else
        {
            if ((transform.rotation.normalized.eulerAngles - Quaternion.Euler(_targetVector).normalized.eulerAngles).magnitude >= 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_targetVector), Time.deltaTime * _speed);
            }
        }
    }
}
