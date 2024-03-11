using TMPro;
using UnityEngine;
using Zenject;

public class ArmyWheatConsuming : MonoBehaviour
{
	[SerializeField] private int wheatPerWarrior;
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
        {
            currentTimer = (timerInterval - (Time.time - startTimer)) *Time.timeScale;
            timerText.text = Mathf.RoundToInt(currentTimer).ToString() + " ñ.";
        }
        else
            OnTimerElapsed();
    }

    private void ResetTimer()
    {
        currentTimer = 0;
        startTimer = Time.time;
    }

    private void OnTimerElapsed()
    {
        if (resources.Warriors > 0)
        {
            if (resources.Warriors * wheatPerWarrior <= resources.Wheat)
            {
                resources.Wheat -= wheatPerWarrior * resources.Warriors;
                audioPlayer.PlaySound(audioPlayer.EatSound, 0.7f);
            }
            else
            {
                resources.Warriors -= resources.Warriors - resources.Wheat / wheatPerWarrior;
                resources.Wheat -= resources.Wheat / wheatPerWarrior;
            }
        }
        ResetTimer();
    }
}
