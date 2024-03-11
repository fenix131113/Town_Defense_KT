using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class HiredCharacter : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private ParticleSystem characterParticles;

	// Можно было бы прокинуть через Zenject (Что было бы лучше), но т.к. это просто кт и игра меняться не будет,
	// то можно обойтись и просто координатами, чтобы не морочить себе голову с прокидыванием цели через Zenject
	private Vector3 targetPos = new Vector3(-7.443985f, -14.7f, -0.1855316f);
	private Action pathCompleteAction;

	private WavesController waves;

	[Inject]
	private void Init(WavesController waves)
	{
		this.waves = waves;
	}

	private void Start()
	{
		agent.destination = targetPos;
	}

	private void Update()
	{
		if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
		{
			if (waves.CurrentEnemy && Vector3.Distance(transform.position, waves.CurrentEnemy.transform.position) < 2f && waves.CurrentEnemyCount > 0)
				return;
			// Path complete
			pathCompleteAction();
			characterParticles.transform.parent = null;
			characterParticles.Play();
			Destroy(characterParticles.gameObject, 3f);
			Destroy(gameObject);
		}
	}

	public void SetPathCompleteAction(Action action) => pathCompleteAction = action;
}