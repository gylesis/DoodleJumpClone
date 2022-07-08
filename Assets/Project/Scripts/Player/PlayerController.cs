using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class PlayerController : IInitializable, IDisposable, IMyTickable
    {
        private Player _player;

        private readonly PlayerFacade _playerFacade;
        private readonly InputService _inputService;
        private readonly HorizontalRestrictionService _horizontalRestrictionService;

        public PlayerController(PlayerFacade playerFacade, InputService inputService, HorizontalRestrictionService horizontalRestrictionService)
        {
            _horizontalRestrictionService = horizontalRestrictionService;
            _inputService = inputService;
            _playerFacade = playerFacade;
        }
    
        public void Initialize()
        {
            _playerFacade.PlayerSpawned.Take(1).Subscribe((player =>
            {
                _player = player;
                Jump();
            }));
        }

        public void Dispose() { }

        public void Tick()
        {
            var side = _inputService.Side;

            if (side == 1)
            {
                Move(-1); // left
            }
            else if (side == 2)
            {
                Move(1); // right
            }
        }

        private void Move(int sign)
        {
            sign = (int) Mathf.Sign(sign);

            if (sign == 1)
            {
                _player.PlayerView.FlipMainPerson(false);
            }
            else if (sign == -1)
            {
                _player.PlayerView.FlipMainPerson(true);
            }

            Transform transform = _player.transform;
            
            transform.position += Vector3.right * sign * Time.deltaTime * _player.SensitivityX;

            Vector3 restrictedPos =
                _horizontalRestrictionService.GetXPos(transform.position, transform.localScale.x * 5);

            transform.position = restrictedPos;
        }
        
        public void Jump()
        {
            _player.Rigidbody.velocity = Vector2.zero;
            _player.Rigidbody.angularVelocity = 0;
            _player.Rigidbody.velocity += Vector2.up * _player.JumpModifier;
        }
    }
}