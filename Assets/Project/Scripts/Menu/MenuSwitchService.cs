using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Menu
{
    public class MenuSwitchService : IInitializable , IDisposable
    {
        private readonly GamePauseService _gamePauseService;
        private Dictionary<Type, Menu> _menus;
        private Menu _currentMenu;

        public MenuSwitchService(UIContainer uiContainer)
        {
            _menus = uiContainer.Menus.ToDictionary(x => x.GetType());
        }

        public void Initialize()
        {
            Menu menu = _menus[typeof(MainMenu)];

            _currentMenu = menu;
        }

        public void OpenMenu<TMenu>() where TMenu : Menu
        {
            Type menuType = typeof(TMenu);
            
            if(_menus.ContainsKey(menuType) == false)
            {
                Debug.LogError($"Menu {menuType} doesn't exist");
                return;
            }
            
            Debug.Log("open menu");
            
            Menu menu = _menus[menuType];

            menu.Show();

            if (_currentMenu != null) 
                _currentMenu.Hide();

            _currentMenu = menu;
        }

        public void Dispose()
        {
            _currentMenu?.Hide();
        }
    }
}