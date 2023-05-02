using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Gift
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private MeshRenderer _meshRenderer;

    private bool _isParticleEmitted;

    private void Start()
    {
        if(GameManager.Instance.CoinsText != null)
            GameManager.Instance.CoinsText.SetCoinsText(PlayerPrefs.GetInt(_name).ToString());
    }

    private void Update()
    {
        transform.Rotate(transform.forward, _rotationSpeed * Time.deltaTime, Space.World);
        if (_particleSystem.isStopped && _isParticleEmitted) gameObject.SetActive(false);
    }

    public override void ChangeProp()
    {
        _particleSystem.Emit(5);
        _meshRenderer.enabled = false;
        _isParticleEmitted = true;

        GameManager.Instance.DiceButton.gameObject.SetActive(true);
        GameManager.Instance.CoinsText.SetCoinsText(PlayerPrefs.GetInt(_name).ToString());
    }
}
