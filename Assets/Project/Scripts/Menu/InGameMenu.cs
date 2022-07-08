using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Menu
{
    public class InGameMenu : Menu
    {
        [SerializeField] private Button _pauseButton;
        
        private IDisposable _disposable;

        public override void Show()
        {
            _disposable = _pauseButton.OnClickAsObservable().Take(1).Subscribe((unit => MenuSwitchService.OpenMenu<PauseMenu>()));

            DefaultShow();
        }

        public override void Hide()
        {
            _disposable.Dispose();
            DefaultHide();
        }
    }
}