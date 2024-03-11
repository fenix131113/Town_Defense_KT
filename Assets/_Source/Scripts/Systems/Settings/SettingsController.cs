using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Image backgroundBlocker;
    [SerializeField] private RectTransform settingsPanel;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeValueText;

    private bool isSettingsOpened;

    private void Start()
    {
        closeSettingsButton.onClick.AddListener(CloseSettingsPanel);
    }
    public void ChangeTimeScale()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void OpenSettingsPanel()
    {
        if (isSettingsOpened)
            return;

        isSettingsOpened = true;
        ChangeTimeScale();
        backgroundBlocker.gameObject.SetActive(true);
        Sequence openAnim = DOTween.Sequence().SetUpdate(true);
        openAnim.Insert(0, settingsPanel.DOMoveY(Screen.height / 2, .5f).SetUpdate(true));
        openAnim.Insert(0, backgroundBlocker.DOFade(0.5f, .5f).SetUpdate(true));
    }

    private void CloseSettingsPanel()
    {
        if (!isSettingsOpened)
            return;

        ChangeTimeScale();
        isSettingsOpened = false;
        Sequence closeAnim = DOTween.Sequence().SetUpdate(true);
        closeAnim.Insert(0, settingsPanel.DOLocalMoveY(804, .5f).SetUpdate(true));
        closeAnim.Insert(0, backgroundBlocker.DOFade(0, .5f).SetUpdate(true));

        closeAnim.onComplete += () => backgroundBlocker.gameObject.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        volumeValueText.text = Mathf.RoundToInt((volumeSlider.value * 100)).ToString() + "%";
    }
}