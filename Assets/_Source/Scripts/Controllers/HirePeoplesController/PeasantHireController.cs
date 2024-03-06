using UnityEngine;
public class PeasantHireController : HireControllerBase
{
    protected override void OnHire()
    {
        //SpawnLogic on complete hiring
        Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity).GetComponent<HiredCharacter>().SetPathCompleteAction(() => resources.Peasant++);
    }
}
