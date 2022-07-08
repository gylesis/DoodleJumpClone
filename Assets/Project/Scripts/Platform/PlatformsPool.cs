using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Platform
{
    public class PlatformsPool
    {
        private readonly Stack<Platform> _pool = new Stack<Platform>();
        private readonly PlatformsFactory _platformsFactory;

        public List<Platform> Platforms => _pool.ToList();
        
        public PlatformsPool(PlatformsFactory platformsFactory)
        {
            _platformsFactory = platformsFactory;
        }
        
        public void InitializePool(int count)
        {
            for (int i = 0; i < count; i++)
            {   
                Platform platform = _platformsFactory.Create();
                ReturnToPool(platform);
            }
        }
        
        public Platform TakeFromPool(PlatformSetupContext context, Vector3 pos)
        {
            Platform platform = _pool.Pop();

            platform.transform.position = pos;
            
            platform.Setup(context);
            platform.gameObject.SetActive(true);

            return platform;
        }

        public void ReturnToPool(Platform platform)
        {
            platform.gameObject.SetActive(false);
            _pool.Push(platform);
        }
    }
}