using UnityEngine;

public class WarriorHireController : HireControllerBase
{
    protected override void OnHire()
    {
        //SpawnLogic on complete hiring
        Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<HiredCharacter>().SetPathCompleteAction(() => resources.Warriors++);
    }
}
