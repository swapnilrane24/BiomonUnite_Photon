using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Swapnil.Gameplay
{
    public class StartMenu : MenuUIScreen
    {
        [SerializeField] private FusionConnect fusionConnect;
        [SerializeField] private TMP_InputField playerName;
        [SerializeField] private TMP_InputField roomName;
        [SerializeField] private int SceneIndex;
        [SerializeField] private Button hostButton;
        [SerializeField] private Button joinButton;

        private void Start()
        {
            hostButton.onClick.AddListener(HostButtonListner);
            joinButton.onClick.AddListener(JoinButtonListner);
        }

        private void HostButtonListner()
        {
            _menuController.SwitchMenu(MenuType.LoadingMenu);
            fusionConnect.StartHost(playerName.text, roomName.text, SceneIndex);
        }

        private void JoinButtonListner()
        {
            _menuController.SwitchMenu(MenuType.LoadingMenu);
            fusionConnect.JoinRoom(playerName.text, roomName.text, SceneIndex);
        }

    }
}