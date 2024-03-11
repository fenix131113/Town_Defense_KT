using UnityEngine;
public class PeasantHireController : HireControllerBase
{
    protected override void OnHire()
    {
        //SpawnLogic on complete hiring
        GameObject unit = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        unit.GetComponent<HiredCharacter>().SetPathCompleteAction(() => { resources.Peasant++; resources.HiredPeasantCount++; });
        installer.InjectInGameobject(unit);
    }
}