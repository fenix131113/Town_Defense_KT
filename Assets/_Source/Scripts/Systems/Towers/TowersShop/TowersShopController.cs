using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TowersShopController : MonoBehaviour
{
    [SerializeField] private Tower[] items;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text buyButtonText;
    [SerializeField] private TMP_Text towerArmorText;

    private int shopIndex;
    private TowersController towersController;
    private WavesController waves;


    [Inject]
    private void Init(TowersController towersController, WavesController waves)
    {
        this.towersController = towersController;
        this.waves = waves;

        waves.onWaveStatusChanged += UpdateShopCellVisual;
        towersController.onCurrentTowerChanged += UpdateShopCellVisual;
        UpdateShopCellVisual();
    }

    public void ChangeToNextItem()
    {
        if (shopIndex + 1 > items.Length - 1)
            shopIndex = 0;
        else
            shopIndex++;
        UpdateShopCellVisual();
    }

    public void ChangeToPreviousItem()
    {
        if (shopIndex - 1 < 0)
            shopIndex = items.Length - 1;
        else
            shopIndex--;
        UpdateShopCellVisual();
    }

    public void UpdateShopCellVisual()
    {
        towerArmorText.text = "<sprite=0>" + items[shopIndex].Armor.ToString();
        itemIcon.sprite = items[shopIndex].Icon;

        if(waves.isWaveProceed)
            buyButtonText.text = $"Волна!";
        else if(towersController.currentTower)
            buyButtonText.text = $"Уже есть!";
        else
            buyButtonText.text = $"Купить: {items[shopIndex].TowerCost} <sprite=0>";
    }

    public void RequestToBuy()
    {
        towersController.BuyTower(items[shopIndex]);
    }
}