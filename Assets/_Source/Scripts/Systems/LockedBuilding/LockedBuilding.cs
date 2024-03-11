using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LockedBuilding : MonoBehaviour
{
	[SerializeField] private int cost;
	[SerializeField] private Material activeMaterial;
	[SerializeField] private MeshRenderer lockedBuildingRenderer;
	[SerializeField] private GameObject objectToActive;
	[SerializeField] private Button buyButton;
	[SerializeField] private TMP_Text buyButtonText;


	private ResourcesContainer resources;

	[Inject]
	private void Init(ResourcesContainer resources)
	{
		this.resources = resources;
		buyButtonText.text = $"{cost} <sprite=0>";
		buyButton.onClick.AddListener(TryToBuy);
	}

	public void TryToBuy()
	{
		if (resources.Wheat >= cost)
		{
			resources.Wheat -= cost;
			lockedBuildingRenderer.material = activeMaterial;
			objectToActive?.SetActive(true);
			buyButton.gameObject.SetActive(false);
		}
	}
}