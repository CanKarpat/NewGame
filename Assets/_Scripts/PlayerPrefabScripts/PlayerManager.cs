using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public string nameText;
    public string playerId;

    public int playerNumber = 0;
    
    public TextMeshPro nickname;
    public MeshRenderer playerMeshColor;

    public PlayerManager(int playerNumber, string name)
    {
        this.playerNumber = playerNumber;
        this.nameText = name;
    }

    private void Start()
    {
        var playerData = FindObjectOfType<PlayerData>();


        nickname.text = playerData._nickName;
        nameText = playerData._nickName;
    }
    

    
}
