using System;
using Project.Platform;
using UniRx;
using Zenject;

namespace Project
{
    public class PlatformsDeathWatcher : IInitializable, IDisposable
    {
        private readonly TriggerBox _triggerBox;
        private IDisposable _disposable;

        public PlatformsDeathWatcher(TriggerBox triggerBox)
        {
            _triggerBox = triggerBox;   
        }

        public void Initialize()
        {
            _disposable = _triggerBox.Triggered.Subscribe((OnTriggered));
        }

        private void OnTriggered(TriggerEventContext context)
        {
            if (context.Other.TryGetComponent<Platform.Platform>(out var platform))
            {
                platform.ToRecycle.OnNext(platform);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}