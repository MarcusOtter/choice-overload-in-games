using Scripts.Examination;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu_and_UI
{
    public class LoadPlayerGraphics : MonoBehaviour
    {
        [Header("Images")]
        [SerializeField] private Image _headImage;
        [SerializeField] private Image _bodyImage;

        [Header("Sprite Renderers")]
        [SerializeField] private SpriteRenderer _headRenderer;
        [SerializeField] private SpriteRenderer _bodyRenderer;

        private CharacterData? _characterData;
        private AvailableSprites _availableSprites;

        private void Start()
        {
            _characterData = DataCollector.Instance?.CharacterData;
            _availableSprites = ExaminationHandler.Instance?.GetAvailableSprites();

            SetGraphics();
        }

        private void SetGraphics()
        {
            if (_characterData == null || _availableSprites == null)
            {
                Logger.Instance.LogWarning("Cannot load graphics, missing CharacterData or AvailableSprites", gameObject);
                return;
            }

            if (_headImage != null)
            {
                _headImage.sprite = _availableSprites.HeadSprites[_characterData.Value.SelectedHead - 1];
            }

            if (_bodyImage != null)
            {
                _bodyImage.sprite = _availableSprites.BodySprites[_characterData.Value.SelectedBody - 1];
            }

            if (_headRenderer != null)
            {
                _headRenderer.sprite = _availableSprites.HeadSprites[_characterData.Value.SelectedHead - 1];
            }

            if (_bodyRenderer != null)
            {
                _bodyRenderer.sprite = _availableSprites.BodySprites[_characterData.Value.SelectedBody - 1];
            }
        }
    }
}
