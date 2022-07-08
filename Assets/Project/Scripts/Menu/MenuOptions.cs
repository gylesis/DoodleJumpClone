using UnityEngine;

namespace Project.Menu
{
    [CreateAssetMenu(menuName = "Menu/New MenuOptions", fileName = "MenuOptions", order = 0)]
    public class MenuOptions : ScriptableObject
    {
        [SerializeField] private bool _shouldPauseTime;
        [SerializeField] private bool _isAdditive;

        public bool ShouldPauseTime => _shouldPauseTime;
        public bool IsAdditive => _isAdditive;

    }
}