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
    
        [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
        [SerializeField] private PlayerData _playerDataPrefab = null;

        [SerializeField] private TMP_InputField _nickName = null;

        // The Placeholder Text is not accessible through the TMP_InputField component so need a direct reference
        [SerializeField] private TextMeshProUGUI _nickNamePlaceholder = null;

        [SerializeField] private TMP_InputField _roomName = null;
        [SerializeField] private string _gameSceneName = null;

        private NetworkRunner _runnerInstance = null;

        public Button startGameButton;

        public Toggle toggleToCreateRoom, toggleToJoinRoom;

        private void Start()
        {
            toggleToCreateRoom.interactable = false;
            toggleToJoinRoom.interactable = true;
        }

        private void Update()
        {
            if (_nickName.text.Length >= 3 && _roomName.text.Length >= 3)
            {
                startGameButton.interactable = true;
            }
        }

        public void SetToHost()
        {
            toggleToCreateRoom.interactable = false;
            toggleToJoinRoom.interactable = true;
        }
        
        public void SetToClient()
        {
            toggleToCreateRoom.interactable = true;
            toggleToJoinRoom.interactable = false;
        }
        
        public void StartGame()
        {
            if (toggleToCreateRoom.isOn && !toggleToJoinRoom.isOn)
            {
                StartHost();
            }
            else if (!toggleToCreateRoom.isOn && toggleToJoinRoom.isOn)
            {
                StartClient();
            }
        }

        // Attempts to start a new game session 
        public void StartHost()
        {
            SetPlayerData();
            StartGame(GameMode.AutoHostOrClient, _roomName.text, _gameSceneName);
        }

        public void StartClient()
        {
            SetPlayerData();
            StartGame(GameMode.Client, _roomName.text, _gameSceneName);
        }

        private void SetPlayerData()
        {
            var playerData = FindObjectOfType<PlayerData>();
            if (playerData == null)
            {
                playerData = Instantiate(_playerDataPrefab);
            }
            
            if (string.IsNullOrWhiteSpace(_nickName.text))
            {
                playerData.SetNickName(_nickNamePlaceholder.text);
            }
            else
            {
                playerData.SetNickName(_nickName.text);
            }
        }

        private async void StartGame(GameMode mode, string roomName, string sceneName)
        {
            _runnerInstance = FindObjectOfType<NetworkRunner>();
            if (_runnerInstance == null)
            {
                _runnerInstance = Instantiate(_networkRunnerPrefab);
            }

            // Let the Fusion Runner know that we will be providing user input
            _runnerInstance.ProvideInput = true;

            var startGameArgs = new StartGameArgs()
            {
                GameMode = mode,
                SessionName = roomName,
                ObjectPool = _runnerInstance.GetComponent<NetworkObjectPoolDefault>(),
            };

            // GameMode.Host = Start a session with a specific name
            // GameMode.Client = Join a session with a specific name
            await _runnerInstance.StartGame(startGameArgs);

            _runnerInstance.SetActiveScene(sceneName);
        }
    
}
