using Project.Menu;
using Project.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Project
{
    public class SessionObserver : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _clickImage;
        
        private PlayerFacade _playerFacade;
        private MenuSwitchService _menuSwitchService;

        [Inject]
        private void Init(PlayerFacade playerFacade, MenuSwitchService menuSwitchService)
        {
            _menuSwitchService = menuSwitchService;
            _playerFacade = playerFacade;
        }

        private void Start()
        {
            _menuSwitchService.OpenMenu<MainMenu>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartSession();
            _clickImage.enabled = false;
        }

        private void StartSession()
        {
            _playerFacade.SpawnPlayer();
            _menuSwitchService.OpenMenu<InGameMenu>();
        }

        public void RestartSession() 
        {
            // need to restart all systems
            // reloading scene is just for now
            
            SceneManager.LoadScene(0);
        }
        
        
    }
}