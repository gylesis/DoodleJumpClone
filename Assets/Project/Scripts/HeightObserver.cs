using System;
using Project.Player;
using UniRx;

namespace Project
{
    public class HeightObserver : IDisposable, IMyTickable
    {
        private float _maxHeight = -2f;
        private float _currentHeight = 0;
        private int _platformsJumped = 0;

        private readonly UIContainer _uiContainer;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly PlayerFacade _playerFacade;

        public float MaxReachedHeight => _maxHeight;

        public HeightObserver(UIContainer uiContainer, PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
            _uiContainer = uiContainer;
        }

        public void Observe(Platform.Platform platform)
        {
            platform.PlatformJumped.Subscribe(OnPlatformJumped).AddTo(_compositeDisposable);
        }
        
        private void OnPlatformJumped(Platform.Platform platform)
        {
            _platformsJumped++;
       
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        public void Tick()
        {
            var positionY = _playerFacade.Transform.position.y;

            _currentHeight = positionY;

            if (_currentHeight > _maxHeight)
            {
                _maxHeight = _currentHeight;
            }
            
            _uiContainer.ScoreView.UpdateScore((int)_maxHeight);
        }
    }
    
}