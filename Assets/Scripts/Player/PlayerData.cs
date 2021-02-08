using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    [SerializeField] private float planesCount = 10;

    public float Speed { get => speed; }
    public static PlayerData Player { get; private set; } = null;
    public float PlanesCount { get => planesCount; set => planesCount = value; }

    private void Start()
    {
        PlayerDataInitialization();
    }

    private void PlayerDataInitialization()
    {
        if (Player == null)
            Player = this;
        else
            Destroy(gameObject);
    }
}
