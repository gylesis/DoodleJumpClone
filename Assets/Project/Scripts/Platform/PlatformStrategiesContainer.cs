using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Project.Platform
{
    public class PlatformStrategiesContainer
    {
        private Dictionary<Type, IPlatformStrategy> _platformStrategies;

        public PlatformStrategiesContainer(IPlatformStrategy[] strategies)
        {
            _platformStrategies = strategies.ToDictionary(x => x.GetType());
        }
        
        public IPlatformStrategy GetRandomStrategy()
        {
            var value = Random.value;

            IPlatformStrategy platformStrategy;
            
            if (value > 0.4 && value < 0.7)
            {
                platformStrategy = GetStrategy<BreakablePlatformStrategy>();
            }
            else if (value > 0.7 && value < 1)
            {
                platformStrategy = GetStrategy<MovePlatformStrategy>();
            }
            else
            {
                platformStrategy = GetStrategy<DefaultPlatformStrategy>();
            }

            return platformStrategy;
        }


        private IPlatformStrategy GetStrategy<TStrategy>() where TStrategy : IPlatformStrategy
        {   
            return _platformStrategies[typeof(TStrategy)];
        }
        
    }
}