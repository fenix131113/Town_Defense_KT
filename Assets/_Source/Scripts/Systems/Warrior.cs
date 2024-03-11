using System.Collections;
using UnityEngine;
using Zenject;

public class Warrior : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody[] bodyParts;
    [SerializeField] private Transform explosivePoint;
    [SerializeField] private GameObject bodyParticlesPrefab;
    [SerializeField] private GameObject counterText;
    private Enemy currentEnemy;
    private ResourcesConditionsController resourcesConditions;

    [Inject]
    private void Init(ResourcesConditionsController resourcesConditions)
    {
        this.resourcesConditions = resourcesConditions;
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        resourcesConditions.ClearGateWarrior();
        GetComponent<Collider>().enabled = false;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void SetCurrentEnemyAndAttack(Enemy enemy)
    {
        currentEnemy = enemy;
        Attack();
    }

    public void SendDieSingalToEnemy()
    {
        currentEnemy?.Die();
    }


    public void BlowBody()
    {
        foreach (Rigidbody rb in bodyParts)
        {
            rb.transform.parent = null;
            Destroy(rb.transform.gameObject, 4.1f);
            rb.isKinematic = false;
            rb.GetComponent<Collider>().enabled = true;
            rb.AddExplosionForce(Random.Range(3f, 6f), explosivePoint.position, Random.Range(2f, 3f), 1, ForceMode.Impulse);
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

    public void OffCounterText()
    {
        counterText.SetActive(false);
    }
}
