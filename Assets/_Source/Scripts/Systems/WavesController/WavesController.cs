using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class WavesController : MonoBehaviour
{
	[SerializeField][Range(1, 100)] private int enemyCountPlusPercent;
	[SerializeField] private float timeToNewWave;
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private Transform enemySpawnPoint;
	[SerializeField] private TMP_Text waveTimerText;
	[SerializeField] private TMP_Text waveCounterText;

	private int nextWaveEnemyCount = 2;
	private bool isWaveEnd = true;
	private int currentEnemyCount;

	public int CurrentEnemyCount { get { return currentEnemyCount; } set { currentEnemyCount = value; onCurrentEnemyCountChanged?.Invoke(); } }
	public int waveNumber { get; private set; } = 1;
	public bool isWaveProceed { get; private set; }
	public Enemy CurrentEnemy { get; set; }


	//Timer
	private bool timerStopped;
	private float startTimer;

    private MainInstaller installer;
    private AudioPlayer audioPlayer;

    public float currentTimer { get; private set; }


	public delegate void OnCurrentEnemyCountChanged();
	public OnCurrentEnemyCountChanged onCurrentEnemyCountChanged;

	public delegate void OnWaveStatusChanged();
	public OnWaveStatusChanged onWaveStatusChanged;


	[Inject]
	private void Init(MainInstaller installer, AudioPlayer audioPlayer)
	{
		this.installer = installer;
		this.audioPlayer = audioPlayer;
		StartCoroutine(WaitForNextWave());

		onCurrentEnemyCountChanged += () => { if (currentEnemyCount <= 0) EndWave(); };
		UpdateWaveCounterText();
	}

	private void Update()
	{
		if (!timerStopped && Time.time - startTimer < timeToNewWave)
		{
			currentTimer = (timeToNewWave - (Time.time - startTimer)) * Time.timeScale;
			waveTimerText.text = $"{Mathf.RoundToInt(currentTimer)} с.";
		}
		else if (!timerStopped)
		{
			audioPlayer.PlaySound(audioPlayer.WaveStartSound, 0.7f);
			timerStopped = true;
			StartWave();
			StartCoroutine(WaitForNextWave());
		}
	}

	// Waiting for end of current wave and start timer for next wave
	private IEnumerator WaitForNextWave()
	{
		yield return new WaitUntil(() => isWaveEnd);
		isWaveEnd = false;
		currentTimer = 0;
		startTimer = Time.time;
		timerStopped = false;
	}
	private void StartWave()
	{
		isWaveProceed = true;
		currentEnemyCount = nextWaveEnemyCount;
		CurrentEnemy = installer.InjectInGameobject(Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation)).GetComponent<Enemy>();
		onWaveStatusChanged?.Invoke();
	}
	private void EndWave()
	{
		if (!isWaveProceed)
			return;
		isWaveProceed = false;
		waveNumber++;
		nextWaveEnemyCount = (int)(nextWaveEnemyCount + (nextWaveEnemyCount * (float)enemyCountPlusPercent / 100));
		isWaveEnd = true;
		onWaveStatusChanged?.Invoke();

		UpdateWaveCounterText();
	}

	private void UpdateWaveCounterText()
	{
		waveCounterText.text = $"Волна {waveNumber}\n{nextWaveEnemyCount} врагов";
	}
}