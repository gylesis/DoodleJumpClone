using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Menu
{
    public class DeathMenu : Menu
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Button _restartButton;

        private GamePauseService _gamePauseService;
        private HeightObserver _heightObserver;
        private SessionObserver _sessionObserver;

        [Inject]
        private void Init(GamePauseService gamePauseService, HeightObserver heightObserver, SessionObserver sessionObserver)
        {
            _sessionObserver = sessionObserver;
            _heightObserver = heightObserver;
            _gamePauseService = gamePauseService;
        }

        private void Start()
        {
            _restartButton.OnClickAsObservable().Take(1).Subscribe((unit => _sessionObserver.RestartSession())).AddTo(CompositeDisposable);
        }

        public override void Show()
        {
            _gamePauseService.Pause();

            _score.enabled = true;
            _score.text = $"Your score: {(int)_heightObserver.MaxReachedHeight}";
            
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public override void Hide()
        {
            _score.enabled = false; 
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}