using UnityEngine;
using Zenject;

namespace Project
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = 1f;
        
        private HeightObserver _heightObserver;
        private Camera _camera;

        [Inject]
        private void Init(HeightObserver heightObserver, Camera camera)
        {
            _camera = camera;
            _heightObserver = heightObserver;
        }

        private void Update()
        {
            Vector3 movePos = _camera.transform.position;
            movePos.y = _heightObserver.MaxReachedHeight;

            _camera.transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * _lerpSpeed); 
        }
    }
    
}