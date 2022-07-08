using Zenject;

namespace Project.Player
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerFacade>().AsSingle().NonLazy();
            Container.BindFactory<Player, PlayerFactory>().FromComponentInNewPrefabResource("Player").AsSingle();
        }
    }
}