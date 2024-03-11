using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
	[SerializeField] private GameObject mainScriptsContainer;
	public override void InstallBindings()
	{
		Container.Bind<ResourcesContainer>().FromNew().AsSingle().NonLazy();
		Container.Bind<WavesController>().FromComponentOn(mainScriptsContainer).AsSingle();
		Container.Bind<ResourcesConditionsController>().FromComponentOn(mainScriptsContainer).AsSingle();
		Container.Bind<TowersController>().FromComponentOn(mainScriptsContainer).AsSingle();
		Container.Bind<AudioPlayer>().FromComponentOn(mainScriptsContainer).AsSingle();

		Container.Bind<MainInstaller>().FromComponentOn(gameObject).AsSingle();
	}

	public GameObject InjectInGameobject(GameObject obj)
	{
		Container.InjectGameObject(obj);
		return obj;
	}
}