using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using Fusion.Addons.Physics;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Swapnil.Gameplay
{
    /*
     * Implementing INetworkRunnerCallbacks will allow Fusions NetworkRunner to interact with the class
     * The NetworkRunner is the heart and soul of Fusion and runs the actual network simulation.
     */
    public class FusionConnect : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef actorPrefab;
        [SerializeField] private MenuController menuController;
        //PlayerRef is Fusion struct for player and NetwrokObject is component
        //attached to gameobject which will be share in server
        private Dictionary<PlayerRef, NetworkObject> _spawnedActors = new Dictionary<PlayerRef, NetworkObject>();

        private NetworkRunner _networkRunner;
        private string _playerName;
        private string _roomName;
        private int _sceneIndex;
        private bool _mouseButton0;

        public void StartHost(string playerName, string roomName, int sceneIndex)
        {
            _playerName = playerName;
            _roomName = roomName;
            _sceneIndex = sceneIndex;

            if (_playerName.Length <= 0)
            {
                _playerName = "Player" + UnityEngine.Random.Range(0, 1000);
            }

            if (_roomName.Length <= 0)
            {
                _roomName = "TestRoom";
            }

            ConnectToRunner(GameMode.Host);
        }

        public void JoinRoom(string playerName, string roomName, int sceneIndex)
        {
            _playerName = playerName;
            _roomName = roomName;
            _sceneIndex = sceneIndex;

            if (_playerName.Length <= 0)
            {
                _playerName = "Player" + UnityEngine.Random.Range(0, 1000);
            }

            //if (_roomName.Length <= 0)
            //{
            //    _roomName = "TestRoom";
            //}

            ConnectToRunner(GameMode.Client);
        }

        private async void ConnectToRunner(GameMode gameMode)
        {
            //we Add NetworkRunner component to gameobject
            _networkRunner = gameObject.AddComponent<NetworkRunner>();
            _networkRunner.ProvideInput = true; //we tell the Fusion we will provide input

            gameObject.AddComponent<RunnerSimulatePhysics3D>();

            //we convert our sceneIndex which will be scene where action happens
            //to SceneRef which is Fusion struct
            var scene = SceneRef.FromIndex(_sceneIndex);

            //we define basic arguments for our game
            var startGameArgs = new StartGameArgs()
            {
                GameMode = gameMode,
                SessionName = _roomName,
                //Scene = scene,
                //SceneManager handles instantiation of NetworkObjects that are placed directly in the scene
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            };

            //here we ask NetworkRunner to start the game by passing game arguments
            await _networkRunner.StartGame(startGameArgs);

            if (_networkRunner.IsServer)
            {
                await _networkRunner.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        //we dont want to miss click so we are using Update
        private void Update()
        {
            _mouseButton0 = _mouseButton0 | Input.GetMouseButton(0);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {

            var data = new NetworkInputData();

            if (Input.GetKey(KeyCode.W))
                data.direction += Vector3.forward;

            if (Input.GetKey(KeyCode.S))
                data.direction += Vector3.back;

            if (Input.GetKey(KeyCode.A))
                data.direction += Vector3.left;

            if (Input.GetKey(KeyCode.D))
                data.direction += Vector3.right;
            //set the left mouse click
            data.buttons.Set(NetworkInputData.MOUSEBUTTON0, _mouseButton0);
            //and reset to false as soon as we send the data
            _mouseButton0 = false;

            input.Set(data);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        //method called when player joins
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            //if this is the host only then we perform the logic
            if (runner.IsServer)
            {
                SpawnPlayer(runner, player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            //we check if the player who left is available in dictionary
            if (_spawnedActors.TryGetValue(player, out NetworkObject networkObject))
            {
                //despawn it from the network
                runner.Despawn(networkObject);
                //and remove the player from dictionary
                _spawnedActors.Remove(player);
                runner.SetPlayerObject(player, null);
            }
        }

        async void SpawnPlayer(NetworkRunner runner, PlayerRef player)
        {
            await Task.Delay(1000);//0.5 sec

            //we get some random spawn position
            //Vector3 spawnPosition = new Vector3(3 * UnityEngine.Random.Range(-1, 1), 0.2f,
            //    3 * UnityEngine.Random.Range(-1, 1));
            //Spawn the networkobject aka action
            NetworkObject networkActorObject = runner.Spawn(actorPrefab, Vector3.up,
                Quaternion.identity, player);
            //add the actor to our dictionary
            _spawnedActors.Add(player, networkActorObject);
            runner.SetPlayerObject(player, networkActorObject);
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
            
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
            
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            Debug.Log("----- Finished Loading Scene -----");
            menuController.SwitchMenu(MenuType.GameplayMenu);
            menuController.DeactivateMainSceneElements();
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            
        }
    }
}