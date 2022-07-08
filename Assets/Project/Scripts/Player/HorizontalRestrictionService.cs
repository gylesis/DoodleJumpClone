using UnityEngine;
using Zenject;

namespace Project.Player
{
    public class HorizontalRestrictionService : IInitializable
    {
        private Camera _camera;
        
        private float _screenWidthWorld;
        private float _screenHeightWorld;

        public Vector3 RightUpWorld { get; private set; }
        public Vector3 RightDownWorld { get; private set; }
        public Vector3 LeftUpWorld { get; private set; }
        public Vector3 LeftDownWorld { get; private set; }
        

        public HorizontalRestrictionService(Camera camera)
        {
            _camera = camera;
        }

        public void Initialize()
        {
            var rightUp = new Vector2(Screen.width, Screen.height);
            var rightDown = new Vector2(Screen.width, 0);
            var leftUp = new Vector2(0, Screen.height);
            var leftDown = new Vector2(0, 0);

            RightUpWorld = _camera.ScreenToWorldPoint(rightUp);
            RightDownWorld = _camera.ScreenToWorldPoint(rightDown);
            LeftUpWorld = _camera.ScreenToWorldPoint(leftUp);
            LeftDownWorld = _camera.ScreenToWorldPoint(leftDown);

            _screenWidthWorld = (LeftDownWorld - RightDownWorld).magnitude;
            _screenHeightWorld = (LeftUpWorld - LeftDownWorld).magnitude;
        }

        public Vector3 GetXPos(Vector3 pos, float xRadiusOffset = 1)
        {
            Vector3 position = _camera.WorldToScreenPoint(pos);

            Vector3 playerPos = pos;

            if (position.x > Screen.width + xRadiusOffset)
            {
                playerPos.x -= _screenWidthWorld;
            }

            if (position.x < 0 - xRadiusOffset)
            {
                playerPos.x += _screenWidthWorld;
            }

            /*if (position.y > Screen.height)
            {
                playerPos.y -= _screenHeightWorld + 1;
            }

            if (position.y < 0)
            {
                playerPos.y += _screenHeightWorld - 1;
            }*/

            return playerPos;
        }
    }
}