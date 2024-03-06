using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HireControllerBase : MonoBehaviour
{
    // ������� ��� ���������� ������� ����� ����� �� ������ ������� ��� ����, ����� ������ ��� �� ������������ GetComponent,
    //������� �� ����� ��������� ����
    [SerializeField] protected Button hireButton;
    [SerializeField] protected Image hireButtonImage;
    [SerializeField] protected TMP_Text hireButtonText;
    [SerializeField] protected string hireTextPrefix;
    [SerializeField] protected string hireTextSuffix;
    [SerializeField] protected GameObject characterPrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected int hireCost;
    [SerializeField] protected float hireCostMultiplier;

    [Tooltip("In Seconds")]
    [SerializeField] protected float hiringTime;

    protected ResourcesContainer resources;
    private float startTimer;

    [Inject]
    private void Init(ResourcesContainer resources)
    {
        this.resources = resources;
        hireButton.onClick.AddListener(Hire);
        UpdateHireButtonTextVisual();
    }

    public void Hire()
    {
        // add wheat condition to buy
        if (hireButton.interactable && resources.Wheat >= hireCost)
        {
            resources.Wheat -= hireCost;
            hireCost = (int)(hireCost * hireCostMultiplier);
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
            hireButtonImage.fillAmount = (Time.time - startTimer) / hiringTime;
            if (hireButtonImage.fillAmount == 1)
            {
                OnHire();
                hireButton.interactable = true;
            }
        }
    }
    public void UpdateHireButtonTextVisual() => hireButtonText.text = $"{hireTextPrefix}{hireCost}{hireTextSuffix}";
}
