using DG.Tweening;
using UnityEngine;
using Zenject;

public class TowersController : MonoBehaviour
{
	[SerializeField] private ParticleSystem dirtThrowParticlesTransform;
	[SerializeField] private float particlesUpperY;
	[SerializeField] private float particlesLowerY;
	[SerializeField] private Transform towerSpawnPoint;

	private ResourcesContainer resources;
	private WavesController waves;

	public Tower currentTower { get; private set; }

	public delegate void OnCurrentTowerChanged();
	public OnCurrentTowerChanged onCurrentTowerChanged;

	[Inject]
	private void Init(ResourcesContainer resources, WavesController waves)
	{
		this.resources = resources;
		this.waves = waves;
	}

	public void BuyTower(Tower tower)
	{
		if (!waves.isWaveProceed && resources.Wheat >= tower.GetComponent<Tower>().TowerCost && !currentTower)
		{
			// Можно было конечно сделать Object Pool. Но зачем если мне лень? (╯°□°）╯︵ ┻━┻
			SetCurrentTower(Instantiate(tower.Prefab, new Vector3(towerSpawnPoint.position.x, tower.GetComponent<Tower>().SpawnY, towerSpawnPoint.position.z), Quaternion.identity).GetComponent<Tower>());
			BuildTowerVisual();

			resources.Wheat -= currentTower.TowerCost;
		}
	}

	private void BuildTowerVisual()
	{
		if (!currentTower)
			Debug.LogWarning("Current tower doesn't exist!");

		Vector3 startCameraPos = Camera.main.transform.position;

		dirtThrowParticlesTransform.Play();
		Sequence towerBuildAnim = DOTween.Sequence();
		towerBuildAnim.Insert(0, dirtThrowParticlesTransform.transform.DOLocalMoveY(particlesUpperY, 1f));
		towerBuildAnim.Insert(0.4f, currentTower.transform.DOMoveY(currentTower.UpperY, 2.6f));

		Camera.main.DOShakePosition(4f, .1f, 20, 90, true);

		towerBuildAnim.onComplete += () =>
		{
			dirtThrowParticlesTransform.transform.DOLocalMoveY(particlesLowerY, .5f).onComplete +=
			() => dirtThrowParticlesTransform.Stop();
			Camera.main.transform.position = startCameraPos;
		};
	}

	public void DestroyCurrentTowerVisual()
	{
		if (!currentTower)
			Debug.LogWarning("Current tower doesn't exist!");

		dirtThrowParticlesTransform.Play();
		Sequence towerDestroyAnim = DOTween.Sequence();
		towerDestroyAnim.Insert(0, dirtThrowParticlesTransform.transform.DOLocalMoveY(particlesUpperY, 0.5f));
		towerDestroyAnim.Insert(0.4f, currentTower.transform.DOMoveY(currentTower.DownY, 1f));

		Camera.main.DOShakePosition(2f, .1f, 20, 90, true);

		towerDestroyAnim.onComplete += () =>
		{
			dirtThrowParticlesTransform.transform.DOLocalMoveY(particlesLowerY, .3f).onComplete +=
			() => dirtThrowParticlesTransform.Stop();
			Destroy(currentTower.gameObject);
			SetCurrentTower(null);
		};
	}

	/// <summary>
	/// Need to invoke after buying new tower
	/// </summary>
	private void SetCurrentTower(Tower tower)
	{
		currentTower = tower;
		onCurrentTowerChanged?.Invoke();
		tower?.Init(this);
	}
}