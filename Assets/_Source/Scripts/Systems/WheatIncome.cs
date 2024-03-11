using TMPro;
using UnityEngine;
using Zenject;

public class WheatIncome : MonoBehaviour
{
	[SerializeField] private int wheatPerPeasant;
	[SerializeField] private float timerInterval;
	[SerializeField] private TMP_Text timerText;

	private ResourcesContainer resources;
	private AudioPlayer audioPlayer;

	private float startTimer;
	public float currentTimer { get; private set; }

	[Inject]
	private void Init(ResourcesContainer resources, AudioPlayer audioPlayer)
	{
		this.resources = resources;
		this.audioPlayer = audioPlayer;
		ResetTimer();
	}
	private void Update()
	{
		if (Time.time - startTimer < timerInterval)
			currentTimer = (timerInterval - (Time.time - startTimer)) * Time.timeScale;
		else
			OnTimerElapsed();
		timerText.text = Mathf.RoundToInt(currentTimer).ToString() + " ñ.";
	}

	private void ResetTimer()
	{
		currentTimer = 0;
		startTimer = Time.time;
	}

	private void OnTimerElapsed()
	{
		if (resources.Peasant > 0)
		{
			resources.Wheat += resources.Peasant * wheatPerPeasant;
			audioPlayer.PlaySound(audioPlayer.GetWheatSound, 0.7f);
		}
		ResetTimer();
	}
}