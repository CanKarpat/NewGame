using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { print("OnConnectedToServer"); }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { print("OnSceneLoadDone"); }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    
    public static GameManager Instance;
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private NetworkRunner _runner;

    // An instance of the ScriptableObject defined above.
    public ScriptableObjectCreator spawnManagerValues;
    
    //Nickname
    public string playerNickname;
    
    public async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        
        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        playerNickname = name;
        SceneManager.LoadSceneAsync("_Scenes/GameScene");
    }
    
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            // if (_spawnedCharacters.Count == 0)
            // {
            //     Vector3 spawnPosition = spawnManagerValues.playerTowerPositionsForRoundOne[0];
            //     NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            //     DontDestroyOnLoad(networkPlayerObject);
            //     networkPlayerObject.transform.eulerAngles = spawnManagerValues.playerTowerRotationsForRoundOne[0];
            //     // Keep track of the player avatars so we can remove it when they disconnect
            //     
            //
            //     SpawnEntities(networkPlayerObject.gameObject,_spawnedCharacters.Count);
            // }
            // else if (_spawnedCharacters.Count == 1)
            // {
            //     Vector3 spawnPosition = spawnManagerValues.playerTowerPositionsForRoundOne[1];
            //     NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            //     DontDestroyOnLoad(networkPlayerObject);
            //     networkPlayerObject.transform.eulerAngles = spawnManagerValues.playerTowerRotationsForRoundOne[1];
            //     // Keep track of the player avatars so we can remove it when they disconnect
            //     _spawnedCharacters.Add(player, networkPlayerObject);
            //
            //     SpawnEntities(networkPlayerObject.gameObject,_spawnedCharacters.Count);
            // }
            // else if (_spawnedCharacters.Count == 2)
            // {
            //     Vector3 spawnPosition = spawnManagerValues.playerTowerPositionsForRoundOne[2];
            //     NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            //     DontDestroyOnLoad(networkPlayerObject);
            //     networkPlayerObject.transform.eulerAngles = spawnManagerValues.playerTowerRotationsForRoundOne[2];
            //     // Keep track of the player avatars so we can remove it when they disconnect
            //     _spawnedCharacters.Add(player, networkPlayerObject);
            //
            //     SpawnEntities(networkPlayerObject.gameObject,_spawnedCharacters.Count);
            // }
            // else if (_spawnedCharacters.Count == 3)
            // {
            //     Vector3 spawnPosition = spawnManagerValues.playerTowerPositionsForRoundOne[3];
            //     NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            //     DontDestroyOnLoad(networkPlayerObject);
            //     networkPlayerObject.transform.eulerAngles = spawnManagerValues.playerTowerRotationsForRoundOne[3];
            //     // Keep track of the player avatars so we can remove it when they disconnect
            //     _spawnedCharacters.Add(player, networkPlayerObject);
            //
            //     SpawnEntities(networkPlayerObject.gameObject,_spawnedCharacters.Count);
            // }
            
            // Create a unique position for the player
            
            Vector3 spawnPosition = new Vector3((player.RawEncoded%runner.Config.Simulation.DefaultPlayers)*3,1,0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            DontDestroyOnLoad(networkPlayerObject);
            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
            SpawnEntities(networkPlayerObject.gameObject.gameObject);
        }
    }
    
    
    private void SpawnEntities(GameObject joinedPlayer)
    {
        //Set player name/id at UI - Set player color
        var controller = joinedPlayer.GetComponent<PlayerManager>();
        
        // playerNickname = nicknameController.text;
        
        controller.nameText = playerNickname;
        controller.nickname.text = playerNickname;
        controller.playerId = _runner.UserId;
        controller.playerMeshColor.material = spawnManagerValues.playerColors[0];
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
}
