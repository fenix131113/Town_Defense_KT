using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private AnimationClip moveAnimation;
	[SerializeField] private Animator anim;
	[SerializeField] private Rigidbody[] bodyParts;
	[SerializeField] private Transform explosivePoint;
	[SerializeField] private GameObject bodyParticlesPrefab;
	[SerializeField] private GameObject bigCloudParticlesPrefab;
	[SerializeField] private TMP_Text counterText;

	private Vector3 targetPos = new Vector3(-7.443985f, -14.7f, -0.1855316f);
	private ResourcesContainer resources;
	private WavesController waves;
	private ResourcesConditionsController conditionsController;

	private Transform hitTarget;

	[Inject]
	private void Init(ResourcesContainer resources, WavesController waves, MainInstaller installer, ResourcesConditionsController conditionsController)
	{
		this.resources = resources;
		this.waves = waves;
		this.conditionsController = conditionsController;
		installer.InjectInGameobject(counterText.gameObject);
	}
	private void Start()
	{
		agent.destination = targetPos;
	}

	private void Update()
	{
		// When enemy goes through castle
		if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
		{
			resources.Peasant -= waves.CurrentEnemyCount * 3;
			if (resources.Peasant >= 0)
				waves.CurrentEnemyCount = 0;
			else
				conditionsController.Loose();


			Destroy(Instantiate(bigCloudParticlesPrefab, transform.position, Quaternion.identity), 2f);
			Destroy(gameObject);
		}
	}

	// Calling from animation
	public void OnHitAnimation()
	{
		if (hitTarget)
			hitTarget.GetComponent<Warrior>().Die();
	}

	public void OnTriggerEnter(Collider col)
	{
		// Tower and warriors interactions
		Warrior warrior = col.transform.GetComponent<Warrior>();
		Tower tower = col.transform.GetComponent<Tower>();
		if (warrior)
		{
			agent.isStopped = true;

			if (waves.CurrentEnemyCount > resources.Warriors)
			{
				anim.SetTrigger("Attack");
				warrior.OffCounterText();
				hitTarget = col.transform;
				waves.CurrentEnemyCount -= resources.Warriors;
				resources.Warriors = 0;
				StartCoroutine(RestoreMovement(moveAnimation.length));
			}
			else if (waves.CurrentEnemyCount < resources.Warriors)
			{
				resources.Warriors -= waves.CurrentEnemyCount;
				waves.CurrentEnemyCount = 0;
				warrior.SetCurrentEnemyAndAttack(this);
				counterText.gameObject.SetActive(false);

			}
			else
			{
				resources.Warriors = 0;
				waves.CurrentEnemyCount = 0;
				warrior.Die();
				Die();
			}
		}
		else if (tower)
		{
			agent.isStopped = true;
			if (tower.Armor > waves.CurrentEnemyCount)
			{
				tower.Armor -= waves.CurrentEnemyCount;
				Die();
			}
			else if (tower.Armor < waves.CurrentEnemyCount)
			{
				waves.CurrentEnemyCount -= tower.Armor;
				tower.DestoryTower();

				anim.SetTrigger("Attack");
				StartCoroutine(RestoreMovement(moveAnimation.length));
			}
			else
			{
				tower.DestoryTower();
				Die();
			}
		}
	}
	private IEnumerator RestoreMovement(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		agent.isStopped = false;
	}
	public void Die()
	{
		anim.SetTrigger("Die");
		waves.CurrentEnemyCount = 0;
		counterText.gameObject.SetActive(false);
		//waves.CurrentEnemy = null;
	}

	public void BlowBody()
	{
		foreach (Rigidbody rb in bodyParts)
		{
			rb.transform.parent = null;
			Destroy(rb.transform.gameObject, 5f);
			rb.isKinematic = false;
			rb.GetComponent<Collider>().enabled = true;
			rb.AddExplosionForce(Random.Range(2f, 4f), explosivePoint.position, Random.Range(.1f, .5f), 1, ForceMode.Impulse);
			StartCoroutine(DestoryingParticles(rb));
		}
		Destroy(counterText.gameObject);
	}

	private IEnumerator DestoryingParticles(Rigidbody rb)
	{
		yield return new WaitForSeconds(4f);
		rb.GetComponent<BodyPart>().StartDestroy(bodyParticlesPrefab);
		Destroy(gameObject);
	}

}