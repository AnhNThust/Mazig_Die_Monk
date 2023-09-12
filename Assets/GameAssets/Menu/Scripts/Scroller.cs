using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _backgroundImage;
    [SerializeField] private float _x, _y;

    private void Update() {
        _backgroundImage.uvRect = new Rect(_backgroundImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _backgroundImage.uvRect.size);
    }
}
