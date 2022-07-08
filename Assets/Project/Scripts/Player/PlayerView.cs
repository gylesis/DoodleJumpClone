using UnityEngine;

namespace Project.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _mainPersonImg;

        public void FlipMainPerson(bool flipX)
        {
            _mainPersonImg.flipX = flipX;
        }
        
    }
}