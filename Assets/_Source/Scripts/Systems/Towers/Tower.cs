using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] private int _towerCost;
	[SerializeField] private int _armor;
	[SerializeField] private float _upperY;
	[SerializeField] private float _downY;
	[SerializeField] private float _spawnY;
	[SerializeField] private Sprite _icon;
	[SerializeField] private GameObject _prefab;
	[SerializeField] private TMP_Text armorCounterText;

	private TowersController towersController;

	public int Armor { get { return _armor; } set { _armor = value; UpdateArmorCounterText(); } }
	public float UpperY => _upperY;
	public float DownY => _downY;
	public float SpawnY => _spawnY;
	public int TowerCost => _towerCost;
	public Sprite Icon => _icon;
	public GameObject Prefab => _prefab;


	private void Start()
	{
		UpdateArmorCounterText();
	}
	public void Init(TowersController towersController)
	{
		this.towersController = towersController;
	}
	public void DestoryTower()
	{
		Armor = 0;
		towersController.DestroyCurrentTowerVisual();
	}

	private void UpdateArmorCounterText()
	{
		armorCounterText.text = _armor.ToString();
	}
}