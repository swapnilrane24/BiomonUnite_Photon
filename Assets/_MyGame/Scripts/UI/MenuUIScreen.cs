using System.Collections;
using System.Collections.Generic;
using Swapnil.Gameplay;
using UnityEngine;

namespace Swapnil.Gameplay
{
    public enum MenuType
    {
        StartMenu,
        LoadingMenu,
        GameplayMenu,
        CoopMenu
    }

    public class MenuUIScreen : MonoBehaviour
    {
        [SerializeField] protected MenuType menuType;
        [SerializeField] protected TogglePanel togglePanel;

        protected MenuController _menuController;

        public MenuType MenuType => menuType;

        public virtual void TogglePanelActivation(bool canActivate)
        {
            togglePanel.ToggleVisibility(canActivate);
        }

        public virtual void SetMenuController(MenuController menuController)
        {
            _menuController = menuController;
        }

    }
}