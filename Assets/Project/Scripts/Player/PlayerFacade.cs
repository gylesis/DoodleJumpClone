using UniRx;
using UnityEngine;

namespace Project.Player
{
    public class PlayerFacade
    {
        private readonly PlayerFactory _playerFactory;

        private Player _player;
        public Transform Transform => _player.transform;
        public Subject<Player> PlayerSpawned { get; } = new Subject<Player>();

        public PlayerFacade(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void SpawnPlayer()
        {
            _player = _playerFactory.Create();

           _player.transform.position += Vector3.down * 2;

            PlayerSpawned.OnNext(_player);
        }
    }
}