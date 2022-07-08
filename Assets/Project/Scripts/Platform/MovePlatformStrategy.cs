using System.Collections;
using DG.Tweening;
using Project.Player;
using UnityEngine;

namespace Project.Platform
{
    public class MovePlatformStrategy : IPlatformStrategy
    {
        private readonly HorizontalRestrictionService _horizontalRestrictionService;

        private GameObject _gameObject;

        public MovePlatformStrategy(HorizontalRestrictionService horizontalRestrictionService, GameObject gameObject)
        {
            _gameObject = gameObject;
            _horizontalRestrictionService = horizontalRestrictionService;
        }

        public void Process(Platform platform)
        {
            _gameObject.GetComponent<MonoBehaviour>().StopCoroutine(Move(platform));
            _gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Move(platform));
        }
        
        IEnumerator Move(Platform platform)
        {
            var rightXBorder = _horizontalRestrictionService.RightDownWorld.x - platform.transform.localScale.x ;
            var leftXBorder = _horizontalRestrictionService.LeftDownWorld.x + platform.transform.localScale.x;
            
            while (true)
            {
                yield return platform.transform.DOMoveX(rightXBorder, 1.5f).WaitForCompletion();
                yield return platform.transform.DOMoveX(leftXBorder, 1.5f).WaitForCompletion();
            }
        }
        
    }
}