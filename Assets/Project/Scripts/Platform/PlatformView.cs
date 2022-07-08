using UnityEngine;

namespace Project.Platform
{
    public class PlatformView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        public void Restart()
        {
            _sprite.color = Color.white;
        }
        
        public void PlayBreakAnimation()
        {
            _sprite.color = Color.red;
        }
        
    }
}