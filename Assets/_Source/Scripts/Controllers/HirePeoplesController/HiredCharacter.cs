using System;
using UnityEngine;
using UnityEngine.AI;

public class HiredCharacter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ParticleSystem characterParticles;

    private Vector3 targetPos;
    private Action pathCompleteAction;

    private void Start()
    {
        // Можно было бы прокинуть через Zenject (Что было бы лучше), но т.к. это просто кт и игра меняться не будет,
        // то можно обойтись и просто координатами, чтобы не морочить себе голову с прокидыванием цели через Zenject
        targetPos = new Vector3(-7.443985f, -14.7f, -0.1855316f);
        agent.destination = targetPos;
    }

    private void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
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
