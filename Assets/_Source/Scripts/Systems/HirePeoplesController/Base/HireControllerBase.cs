using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HireControllerBase : MonoBehaviour
{
    // Сделано две переменные которые будут взяты из одного объекта для того, чтобы лишний раз не использовать GetComponent,
    //который не слабо нагружает игру
    [SerializeField] protected Button hireButton;
    [SerializeField] protected Image hireButtonImage;
    [SerializeField] protected TMP_Text hireButtonText;
    [SerializeField] protected string hireTextPrefix;
    [SerializeField] protected string hireTextSuffix;
    [SerializeField] protected GameObject characterPrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected int hireCost;

    [Tooltip("In Seconds")]
    [SerializeField] protected float hiringTime;

    protected ResourcesContainer resources;
    protected MainInstaller installer;
    private float startTimer;

    [Inject]
    private void Init(ResourcesContainer resources, MainInstaller installer)
    {
        this.resources = resources;
        this.installer = installer;
        hireButton.onClick.AddListener(Hire);
        UpdateHireButtonTextVisual();
    }

    public void Hire()
    {
        // add wheat condition to buy
        if (hireButton.interactable && resources.Wheat >= hireCost)
        {
            resources.Wheat -= hireCost;
            hireButton.interactable = false;
            hireButtonImage.fillAmount = 0;
            startTimer = Time.time;
            UpdateHireButtonTextVisual();
        }
    }

    protected virtual void OnHire()
    {
        Debug.LogError("You need to use certian hire controller (peasants or warriors)");
    }

    private void Update()
    {
        if (!hireButton.interactable && Time.time - startTimer >= 0)
        {
            hireButtonImage.fillAmount = ((Time.time - startTimer) / hiringTime) * Time.timeScale;
            if (hireButtonImage.fillAmount == 1)
            {
                OnHire();
                hireButton.interactable = true;
            }
        }
    }
    public void UpdateHireButtonTextVisual() => hireButtonText.text = $"{hireTextPrefix}{hireCost}{hireTextSuffix}";
}
