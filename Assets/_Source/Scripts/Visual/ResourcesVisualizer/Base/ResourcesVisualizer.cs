using TMPro;
using UnityEngine;
using Zenject;

public class ResourcesVisualizer : MonoBehaviour
{
    [SerializeField] protected TMP_Text textToWrite;
    [SerializeField] protected string textPrefix;
    [SerializeField] protected string textSuffix;

    protected ResourcesContainer resources;

    [Inject]
    private void Init(ResourcesContainer resources)
    {
        this.resources = resources;
    }

    private void Start()
    {
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

}
