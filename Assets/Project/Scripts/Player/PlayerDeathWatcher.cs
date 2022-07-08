using System;
using Project.Menu;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class PlayerDeathWatcher : IInitializable, IDisposable
    {
        private readonly TriggerBox _triggerBox;
        private readonly MenuSwitchService _menuSwitchService;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public PlayerDeathWatcher(TriggerBox triggerBox, MenuSwitchService menuSwitchService)
        {
            _menuSwitchService = menuSwitchService;
            _triggerBox = triggerBox;
        }

        public void Initialize()
        {
            _triggerBox.Triggered.Subscribe((OnDeathColliderTriggered)).AddTo(_compositeDisposable);
        }

        private void OnDeathColliderTriggered(TriggerEventContext context)
        {
            if (context.Other.TryGetComponent<Player>(out var player))
            {
                _menuSwitchService.OpenMenu<DeathMenu>();
            }
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}