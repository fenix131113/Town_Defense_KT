using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ResourcesConditionsController : MonoBehaviour
{
	[SerializeField] private int needWheatToWin;
	[SerializeField] private GameObject gateWarriorPrefab;
	[SerializeField] private Transform gateWarriorSpawnPoint;

	[Header("Win Loose Panel Properties")]
    [SerializeField] private RectTransform winLoosePanelTransform;
    [SerializeField] private Image winLoosePanelBlocker;
	[SerializeField] private TMP_Text winLooseTitleText;
	[SerializeField] private TMP_Text winLooseInfoText;

	public bool IsGameEnded { get; private set; }

    private ResourcesContainer resources;
	private WavesController waves;
	private MainInstaller installer;
	private GameObject gateWarrior;

	[Inject]
	private void Init(ResourcesContainer resources, WavesController waves, MainInstaller installer)
	{
		this.resources = resources;
		this.waves = waves;
		this.installer = installer;
	}

	private void Start()
	{
		resources.onWarriorsChanged += CheckWarriorOnGates;
	}

    private void Update()
    {
        if(resources.Wheat >= needWheatToWin && waves.waveNumber - 1 == 10 && !IsGameEnded)
		{
			Win();
		}
    }

	private void Win()
	{
		IsGameEnded = true;
        winLooseTitleText.text = "Вы выиграли!";

        OpenLooseWinPanel();
    }

    public void Loose()
    {
        IsGameEnded = true;
        winLooseTitleText.text = "Вы прогирали!";

        OpenLooseWinPanel();
    }

	private void OpenLooseWinPanel()
	{
		Time.timeScale = 0;
		winLoosePanelBlocker.gameObject.SetActive(true);

		Sequence openPanelAnim = DOTween.Sequence().SetUpdate(true);
		openPanelAnim.Insert(0, winLoosePanelBlocker.DOFade(.5f, 0.5f));
		openPanelAnim.Insert(0, winLoosePanelTransform.DOMoveY(Screen.height / 2, .5f));

		winLooseInfoText.text = $"Итог\r\nПережито волн: {waves.waveNumber - 1}\r\nНанято войнов: {resources.HiredWarriorsCount}\r\nНанято крестьян: {resources.HiredPeasantCount}\r\n";
    }

	public void ReloadGame()
	{
		Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

	public void ExitGame() => Application.Quit();

    /// <summary>
    /// Check warriors count and spawn gate warrior if he doesn't exist already
    /// </summary>
    private void CheckWarriorOnGates()
	{
		if (resources.Warriors > 0 && gateWarrior == null)
		{
			gateWarrior = installer.InjectInGameobject(Instantiate(gateWarriorPrefab, gateWarriorSpawnPoint.position, gateWarriorSpawnPoint.rotation));
			installer.InjectInGameobject(gateWarrior);
		}
		else if (resources.Warriors == 0 && gateWarrior)
			gateWarrior.GetComponent<Warrior>().Die();
	}

	public void ClearGateWarrior() => gateWarrior = null;
}