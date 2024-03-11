using UnityEngine;

public class AudioPlayer: MonoBehaviour
{
	[SerializeField] private AudioSource player;

	// SOUNDS
	[Header("Sounds")]
	[SerializeField] private AudioClip buttonSound_01;
	[SerializeField] private AudioClip buttonSound_02;
	[SerializeField] private AudioClip eatSound;
	[SerializeField] private AudioClip waveStartSound;
	[SerializeField] private AudioClip getWheatSound;


	// PROPERTIES
	public AudioClip ButtonSound_01 => buttonSound_01;
	public AudioClip ButtonSound_02 => buttonSound_02;
	public AudioClip EatSound => eatSound;
	public AudioClip WaveStartSound => waveStartSound;
	public AudioClip GetWheatSound => getWheatSound;

	public void PlaySound(AudioClip clip, float volume = 1f)
	{
		player.PlayOneShot(clip, volume);
	}
}