using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Swapnil.Gameplay
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainSceneElements;
        [SerializeField] private MenuUIScreen[] menuUIScreens;

        private MenuUIScreen currentActiveScreen;

        private void Awake()
        {
            for (int i = 0; i < menuUIScreens.Length; i++)
            {
                menuUIScreens[i].SetMenuController(this);
            }
        }

        private void Start()
        {
            SwitchMenu(MenuType.StartMenu);
        }

        public void SwitchMenu(MenuType menuType)
        {
            if (currentActiveScreen)
            {
                currentActiveScreen.TogglePanelActivation(false);
            }

            for (int i = 0; i < menuUIScreens.Length; i++)
            {
                if (menuUIScreens[i].MenuType == menuType)
                {
                    menuUIScreens[i].TogglePanelActivation(true);
                    currentActiveScreen = menuUIScreens[i];
                }
            }
        }

        public void DeactivateMainSceneElements()
        {
            mainSceneElements.SetActive(false);
        }

    }
}
