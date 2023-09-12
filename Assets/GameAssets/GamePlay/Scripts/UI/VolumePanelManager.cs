using UnityEngine;
using UnityEngine.UI;

public class VolumePanelManager : MonoBehaviour
{
    [SerializeField] private Image _audioImage;
    [SerializeField] private Sprite _audioIcon;
    [SerializeField] private Sprite _vibrateIcon;

    [SerializeField] private Slider _volumeSlider;

    private void OnEnable()
    {
        _volumeSlider.value = AudioListener.volume;
        if (_volumeSlider.value > 0)
            _audioImage.sprite = _audioIcon;
        else
            _audioImage.sprite = _vibrateIcon;
    }

    public void OnChangeVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        if (_volumeSlider.value > 0)
            _audioImage.sprite = _audioIcon;
        else
            _audioImage.sprite = _vibrateIcon;
    }
}
