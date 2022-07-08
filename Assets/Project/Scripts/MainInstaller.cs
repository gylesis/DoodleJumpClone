using Project.Menu;
using Project.Platform;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Platform.Platform _platformPrefab;
        [SerializeField] private UIContainer _uiContainer;
        [SerializeField] private TriggerBox _deathTriggerBox;
        [SerializeField] private SessionObserver _sessionObserver;
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            Container.Bind<PlatformStrategiesContainer>().AsSingle().NonLazy();

            Container.Bind<IPlatformStrategy>().To<BreakablePlatformStrategy>().AsTransient();
            Container.Bind<IPlatformStrategy>().To<DefaultPlatformStrategy>().AsTransient();
            Container.Bind<IPlatformStrategy>().To<MovePlatformStrategy>().AsTransient().WithArguments(_camera.gameObject);
            
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerCollisionHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlatformsDeathWatcher>().AsSingle().WithArguments(_deathTriggerBox);
            Container.Bind<PlatformsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<HorizontalRestrictionService>().AsSingle().NonLazy();
            
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            
            Container.Bind<SessionObserver>().FromInstance(_sessionObserver).AsSingle();
            
            Container.BindFactory<Platform.Platform, PlatformsFactory>().FromComponentInNewPrefab(_platformPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<PlatformsSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeathWatcher>().AsSingle().WithArguments(_deathTriggerBox);

            Container.Bind<PlayerFacade>().FromSubContainerResolve().ByInstaller<PlayerInstaller>().AsSingle();
            
            Container.Bind<MenuSwitchService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<HeightObserver>().AsSingle();
            
            Container.Bind<UIContainer>().FromInstance(_uiContainer);
        }
    }
}