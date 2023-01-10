using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneController : MonoBehaviour
{
    [Header("Selecting Join Type Variable / Set Nickname field")] public TMP_InputField nicknameController;
    [Space(10)] public Button createRoom, joinRoom;
    
    
    [Header("Create Room Variables")] 
    public TMP_InputField roomNameController;
    [Space(10)]public TextMeshProUGUI playButtonText;
    [Space(10)]public Button startServerButton;

    private string buttonText = "!Ready";
    private string buttonPlayText = "Ready";

    private bool isCreatingRoom,isJoiningRoom;
    
    private int checkFieldLength;
    private void Start()
    {
        startServerButton.interactable = false;
        createRoom.interactable = false;
        joinRoom.interactable = false;
    }

    private void Update()
    {
        if (startServerButton.interactable)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // StartGame();
            }
        }
    }

    private void OnEnable()
    {
        nicknameController.onValueChanged.AddListener(delegate { InputFieldValueCheck(); });
        // startServerButton.onClick.AddListener(delegate { StartGame(); });
    }

    private void StartGame(bool client)
    {
        if (client)
            GameManager.Instance.StartGame(GameMode.Host);
        else
        {
            GameManager.Instance.StartGame(GameMode.Client);
        }
    }
    
    private void InputFieldValueCheck()
    {
        if (checkFieldLength >= 3)
        {
            playButtonText.text = buttonPlayText;
            startServerButton.interactable = true;
        }
        else
        {
            playButtonText.text = buttonText;
            startServerButton.interactable = false;
        }
    }
}
