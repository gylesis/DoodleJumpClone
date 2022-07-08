using UniRx;
using UnityEngine;

namespace Project.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformView _platformView;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody => _rigidbody2D;
        public PlatformView PlatformView => _platformView;
        public Collider2D Collider => _collider;
        public Subject<Platform> ToRecycle { get; } = new Subject<Platform>();
        public Subject<Platform> PlatformJumped { get; private set; } = new Subject<Platform>();

        public void Setup(PlatformSetupContext setupContext)
        {
            Restart();

            setupContext.Strategy.Process(this);
        }

        private void Restart()
        {
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles = Vector3.zero;
            transform.rotation = Quaternion.Euler(eulerAngles);

            _rigidbody2D.isKinematic = true;
            _collider.enabled = true;
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.angularVelocity = 0;
            _rigidbody2D.velocity = Vector2.zero;
            _platformView.Restart();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector2 hitPoint = other.contacts[0].point;

                var direction = hitPoint - (Vector2) transform.position;

                direction.Normalize();

                var atan2 = Mathf.Atan2(direction.y, direction.x);

                if (atan2 > 0)
                {
                    PlatformJumped.OnNext(this);
                }
            }
        }
    }


    public struct PlatformSetupContext
    {
        public IPlatformStrategy Strategy;
    }
}