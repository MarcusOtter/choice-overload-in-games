using Scripts.Examination;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Menu_and_UI
{
    public class CharacterCreationUI : MonoBehaviour
    {
        [Header("Big character preview")]
        [SerializeField] private Image _headRenderer;
        [SerializeField] private Image _bodyRenderer;

        [Header("Icons and index text")]
        [SerializeField] private Image _headIcon;
        [SerializeField] private TextMeshProUGUI _headIndexText;

        [SerializeField] private Image _bodyIcon;
        [SerializeField] private TextMeshProUGUI _bodyIndexText;

        [Header("Available sprites")]
        [SerializeField] private AvailableSprites _availableSprites; // TODO: Hide from inspector.

        private int _headIndex;
        private int _bodyIndex;

        private void Start()
        {
            _availableSprites = ExaminationHandler.Instance.GetAvailableSprites();
            UpdateUi();
        }

        public void IncrementHead()
        {
            ModifyIndex(1, CharacterSpriteType.Head);
            UpdateUi();
        }

        public void DecrementHead()
        {
            ModifyIndex(-1, CharacterSpriteType.Head);
            UpdateUi();
        }

        public void IncrementBody()
        {
            ModifyIndex(1, CharacterSpriteType.Body);
            UpdateUi();
        }

        public void DecrementBody()
        {
            ModifyIndex(-1, CharacterSpriteType.Body);
            UpdateUi();
        }

        public void Done()
        {
            // TODO
            var characterData = new CharacterData
            {
                HasChangedName = true,
                CharacterName = "marcus",
                SpriteChoices = 30,
                VisitedHeads = 12,
                VisitedBodies = 3,
                IsMatchingSet = true,
                TimeSpent = 35.51f,
                SatisfactionScore = 9
            };

            DataCollector.Instance.SetCharacterData(characterData);
            SceneTransitioner.Instance.LoadNextScene();
        }

        private void ModifyIndex(int indexDelta, CharacterSpriteType spriteType)
        {
            if (indexDelta == 0) { return; }

            var oldIndex = spriteType == CharacterSpriteType.Head 
                ? _headIndex 
                : _bodyIndex;

            var spriteListMax = spriteType == CharacterSpriteType.Head 
                ? _availableSprites.HeadSprites.Count - 1 
                : _availableSprites.BodySprites.Count - 1;

            if (indexDelta > 0)
            {
                // Increment and prevent index from falling out of bounds
                oldIndex = oldIndex >= spriteListMax ? 0 : oldIndex + 1;
            }
            else
            {
                // Decrement and prevent index from falling out of bounds
                oldIndex = oldIndex == 0 ? spriteListMax : oldIndex - 1;
            }

            switch (spriteType)
            {
                case CharacterSpriteType.Head:
                    _headIndex = oldIndex;
                    break;

                case CharacterSpriteType.Body:
                    _bodyIndex = oldIndex;
                    break;
            }
        }

        private void UpdateUi()
        {
            // Head
            var newHead = _availableSprites.HeadSprites[_headIndex];
            _headRenderer.sprite = newHead;
            _headIcon.sprite = newHead;
            _headIndexText.text = $"{_headIndex + 1}/{_availableSprites.HeadSprites.Count}";

            // Body
            var newBody = _availableSprites.BodySprites[_bodyIndex];
            _bodyRenderer.sprite = newBody;
            _bodyIcon.sprite = newBody;
            _bodyIndexText.text = $"{_bodyIndex + 1}/{_availableSprites.BodySprites.Count}";
        }

    }
}
