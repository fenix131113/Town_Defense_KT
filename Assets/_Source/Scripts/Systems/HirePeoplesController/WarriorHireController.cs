using UnityEngine;

public class WarriorHireController : HireControllerBase
{
    protected override void OnHire()
    {
        //SpawnLogic on complete hiring
        GameObject unit = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        unit.GetComponent<HiredCharacter>().SetPathCompleteAction(() => { resources.Warriors++; resources.HiredWarriorsCount++; });
        installer.InjectInGameobject(unit);
    }
}