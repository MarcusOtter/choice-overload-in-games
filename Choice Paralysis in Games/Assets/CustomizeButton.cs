using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomizeButton : MonoBehaviour
{
    [Header("Button setting")]
    [SerializeField] private bool _changeColor;
    [SerializeField] private bool _changeImage;

    [Header("References")]
    [SerializeField] private SpriteRenderer _imageToChange;

    private Image _thisImage;

    private void Start()
    {
        _thisImage = GetComponent<Image>();
    }

    // Called by pressing the button (preferrably on this object)
    public void ApplyChange()
    {
        if (_changeColor)
        {
            _imageToChange.color = _thisImage.color;
        }

        if (_changeImage)
        {
            _imageToChange.sprite = _thisImage.sprite;
        }
    }
}
