using UnityEngine;
using UnityEngine.EventSystems;

public class ClickyButtonTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _defaultImg;
    [SerializeField] private GameObject _pressedImg;
    [SerializeField] private bool isClicked = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        _defaultImg.SetActive(!isClicked);
        _pressedImg.SetActive(isClicked);
        AudioManager.PlaySoundStatic("Compressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _defaultImg.SetActive(isClicked);
        _pressedImg.SetActive(!isClicked);
        AudioManager.PlaySoundStatic("Uncompressed");
    }
}
