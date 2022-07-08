using UniRx;
using UnityEngine;

namespace Project
{
    public class InputService : IMyTickable
    {
        public int Side { get; private set; }
        private bool _tapped;

        public bool IsTapped => _tapped;

        public Subject<Vector2> ScreenTapped { get; } = new Subject<Vector2>();
     
        public void Tick()
        {
            Side = 0;
            
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;

                var centerWidth = Screen.width / 2;

                if (mousePosition.x < centerWidth)
                {
                    Side = 1;
                }
                else
                {
                    Side = 2;
                }
            }
            else if(Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                
                Vector3 touchPosition = touch.position;

                var centerWidth = Screen.width / 2;

                if (touchPosition.x < centerWidth)
                {
                    Side = 1;
                }
                else
                {
                    Side = 2;
                }
            }
            
            bool isTapped = false;
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                isTapped = true;
                
                ScreenTapped.OnNext(Input.mousePosition);
            }
            else if (Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    isTapped = true;
                    ScreenTapped.OnNext(touch.position);
                }
            }

            _tapped = isTapped;
        }
    }
}