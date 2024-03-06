using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ResourcesContainer>().FromNew().AsSingle().NonLazy();
    }
}