using UnityEngine;

namespace Project
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Menu.Menu[] _menus;
        public ScoreView ScoreView => _scoreView;
        public Menu.Menu[] Menus => _menus;

        private void Awake()
        {
            _menus = GetComponentsInChildren<Menu.Menu>();
        }
    }
}