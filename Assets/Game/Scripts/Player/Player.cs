using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    public PlayerMover Mover { get; private set; }

    private void Awake()
    {
        GameManager.Instance.Player = this;
        Mover = GetComponent<PlayerMover>();
    }
}
