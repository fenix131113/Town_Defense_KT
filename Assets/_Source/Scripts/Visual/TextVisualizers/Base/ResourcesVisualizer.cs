using TMPro;
using UnityEngine;
using UnityEditor;
using Zenject;

public class ResourcesVisualizer : MonoBehaviour
{
	[SerializeField] protected TMP_Text textToWrite;
	[SerializeField] protected string textPrefix;
	[SerializeField] protected string textSuffix;

	protected ResourcesContainer resources;
	protected WavesController waves;

	[Inject]
	private void Init(ResourcesContainer resources, WavesController waves)
	{
		this.resources = resources;
		this.waves = waves;
		Setup();
		UpdateTextNow();
	}

	public virtual void Setup()
	{
		Debug.LogError("You need to ovveride Setup method in resurcesVisualizer");
	}
	public virtual void UpdateTextNow()
	{
		Debug.LogError("You need to ovveride UpdateTextNow method in resurcesVisualizer");
	}

#if UNITY_EDITOR
	[ContextMenu("Use current text", false, -999999999)]
	public void UseCurrnetObjectAsTextComponent()
	{
		textToWrite = GetComponent<TMP_Text>();
	}
#endif
}
