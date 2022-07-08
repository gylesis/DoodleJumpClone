using Zenject;

namespace Project.Menu
{
    public class MainMenu : Menu
    {
        private GamePauseService _gamePauseService;

        [Inject]
        private void Init(GamePauseService gamePauseService)
        {
            _gamePauseService = gamePauseService;
        }
        
        public override void Show()
        {
            _gamePauseService.Pause();
            
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public override void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            _gamePauseService.UnPause();
        }
    }
}