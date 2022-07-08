using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class PlayerCollisionHandler : IInitializable, IDisposable
    {
        private Player _player;

        private readonly PlayerFacade _playerFacade;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly PlayerController _playerController;

        public PlayerCollisionHandler(PlayerFacade playerFacade, PlayerController playerController)
        {
            _playerController = playerController;
            _playerFacade = playerFacade;
        }

        public void Initialize()
        {
            _playerFacade.PlayerSpawned.Take(1).Subscribe((player =>
            {
                _player = player;
                player.Collision.Subscribe((OnPlayerCollision)).AddTo(_disposable);
            })).AddTo(_disposable);
        }

        private void OnPlayerCollision(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                Vector2 hitPoint = other.contacts[0].point;

                var direction = hitPoint - (Vector2) _player.transform.position;

                var atan2 = Mathf.Atan2(direction.y, direction.x);

                Debug.Log($"angle {atan2 * Mathf.Rad2Deg}");
                
                if (atan2 < 0)
                {
                    _playerController.Jump();
                }
            }
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}