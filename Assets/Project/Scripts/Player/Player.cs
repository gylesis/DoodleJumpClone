using UniRx;
using UnityEngine;

namespace Project.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _jumpModifier = 2;
        [SerializeField] private float _sensitivityX = 1.4f;
        [SerializeField] private PlayerView _playerView;

        public float SensitivityX => _sensitivityX;
        public float JumpModifier => _jumpModifier;
        public Rigidbody2D Rigidbody => _rigidbody;
        public PlayerView PlayerView => _playerView;

        public Subject<Collision2D> Collision { get; } = new Subject<Collision2D>();
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Collision.OnNext(other);
        }
    }
}