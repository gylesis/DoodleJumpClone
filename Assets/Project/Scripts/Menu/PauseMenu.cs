using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Menu
{
    public class PauseMenu : Menu
    {
        [SerializeField] private Button _continueButton;    
        
        private GamePauseService _gamePauseService;
        private IDisposable _disposable;

        [Inject]
        private void Init(GamePauseService gamePauseService)
        {
            _gamePauseService = gamePauseService;
        }
        
        public override void Show()
        {
            _disposable = _continueButton.OnClickAsObservable().Take(1).Subscribe((unit => MenuSwitchService.OpenMenu<InGameMenu>()));

            _gamePauseService.Pause();
            
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public override void Hide()
        {
            _disposable.Dispose();
            
            _gamePauseService.UnPause();
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}