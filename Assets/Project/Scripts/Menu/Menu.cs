using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Menu : MonoBehaviour  // smth like state machine
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        
        protected CompositeDisposable CompositeDisposable = new CompositeDisposable();
        protected MenuSwitchService MenuSwitchService;

        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        [Inject]
        private void Init(MenuSwitchService menuSwitchService)
        {
            MenuSwitchService = menuSwitchService;
        }
        
        public abstract void Show();

        public abstract void Hide();

        protected void DefaultShow()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        protected void DefaultHide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnDestroy()
        {
            CompositeDisposable.Dispose();
        }
    }
}