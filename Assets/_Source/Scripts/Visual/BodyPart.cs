using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public void StartDestroy(GameObject particlePrefab)
    {
        Destroy(Instantiate(particlePrefab, transform.position, Quaternion.identity), 3f);
    }
}
