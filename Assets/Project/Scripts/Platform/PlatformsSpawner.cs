using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Platform
{
    public class PlatformsSpawner : IInitializable, IDisposable
    {
        private int _platformsAliveCount = 7;

        private readonly List<Platform> _alivePlatforms = new List<Platform>();
        private readonly Dictionary<int, Vector3> _platformsOriginPoses = new Dictionary<int, Vector3>();
        private readonly HeightObserver _heightObserver;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly PlatformsPool _platformsPool;
        private readonly PlatformStrategiesContainer _platformStrategiesContainer;

        public PlatformsSpawner(HeightObserver heightObserver, PlatformsPool platformsPool, PlatformStrategiesContainer platformStrategiesContainer)
        {
            _platformStrategiesContainer = platformStrategiesContainer;
            _platformsPool = platformsPool;
            _heightObserver = heightObserver;
        }

        public void Initialize()
        {
            _platformsPool.InitializePool(_platformsAliveCount);
            
            foreach (Platform platform in _platformsPool.Platforms)
            {
                platform.ToRecycle.Subscribe((RecyclePlatform)).AddTo(_compositeDisposable);
                
                _heightObserver.Observe(platform);
                _alivePlatforms.Remove(platform);
                
                _platformsOriginPoses.Add(platform.GetInstanceID(), Vector3.zero);
            }
            
            UpdatePlatforms();
        }

        private void UpdatePlatforms()  
        {
            int countToSpawn = _platformsAliveCount - _alivePlatforms.Count;

            countToSpawn = Mathf.Abs(countToSpawn);

            Vector3 posToSpawn;

            if (_alivePlatforms.Count == 0)
            {
                posToSpawn = Vector3.zero + Vector3.up * 2;
            }
            else
            {
                Vector3 platformsOriginPose = _platformsOriginPoses[_alivePlatforms.Last().GetInstanceID()];
                platformsOriginPose.x = 0;
                
                posToSpawn = platformsOriginPose + Vector3.up * 5 + Vector3.right * Random.Range(-2, 2);
            }
            
            for (int i = 0; i < countToSpawn; i++)
            {
                var platformSetupContext = new PlatformSetupContext();
                platformSetupContext.Strategy = _platformStrategiesContainer.GetRandomStrategy();

                Platform platform = _platformsPool.TakeFromPool(platformSetupContext, posToSpawn);
                _platformsOriginPoses[platform.GetInstanceID()] = posToSpawn;
                _alivePlatforms.Add(platform);

                Vector3 platformsOriginPose = _platformsOriginPoses[_alivePlatforms.Last().GetInstanceID()];
                platformsOriginPose.x = 0;
                
                posToSpawn = platformsOriginPose + Vector3.up * 5 + Vector3.right * Random.Range(-2, 2);
            }
        }

        private void RecyclePlatform(Platform platform)
        {
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe((l =>
            {
                UpdatePlatforms();
                _platformsPool.ReturnToPool(platform);
                _alivePlatforms.Remove(platform);
            }));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}