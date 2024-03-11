using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowersShopSwitchButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private float upperY;
	[SerializeField] private float downY;
	[SerializeField] private RectTransform switchArrowTextRect;
	[SerializeField] private RectTransform towersShopPanel;
	[SerializeField] private TowersShopController shopController;

	private bool isOpened;
	private bool canClick = true;
	private AudioPlayer audioPlayer;

	[Inject]
	private void Init(AudioPlayer audio)
	{
		this.audioPlayer = audio;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (isOpened && canClick)
		{
			isOpened = false;
			canClick = false;

			audioPlayer.PlaySound(audioPlayer.ButtonSound_02);

			towersShopPanel.DOLocalMoveY(downY, .5f).onComplete += () => canClick = true;
			switchArrowTextRect.DORotate(new Vector3(0, 0, 90), .2f);
		}
		else if(canClick)
		{
			isOpened = true;
			canClick = false;

            audioPlayer.PlaySound(audioPlayer.ButtonSound_02);

            towersShopPanel.DOLocalMoveY(upperY, .5f).onComplete += () => canClick = true;
			switchArrowTextRect.DORotate(new Vector3(0, 0, -90), .2f);
		}
	}
}