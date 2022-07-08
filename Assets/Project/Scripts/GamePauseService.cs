using UnityEngine;
using Zenject;

namespace Project
{
    public class GamePauseService : ITickable, IInitializable
    {
        private IMyTickable[] _tickables;

        private bool _isPaused;

        public GamePauseService(IMyTickable[] tickables)
        {
            _tickables = tickables;
        }

        public void Pause()
        {
            Debug.Log("Game paused");
            _isPaused = true;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            Debug.Log("Game unpaused");
            _isPaused = false;
            Time.timeScale = 1;
        }

        public void Tick()
        {
            if (_isPaused) return;

            foreach (IMyTickable tickable in _tickables)
            {
                tickable.Tick();
            }
        }

        public void Initialize()
        {
            Pause();
        }
    }

    public interface IMyTickable
    {
        void Tick();
    }
}