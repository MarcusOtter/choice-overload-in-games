using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Menu_and_UI
{
    public class ModifyCharacterSprite : MonoBehaviour
    {
        [Header("Big character")]
        [SerializeField] private SpriteRenderer _head;
        [SerializeField] private SpriteRenderer _body;

        [Header("Icons and index text")]
        [SerializeField] private Image _headIcon;
        [SerializeField] private TextMeshProUGUI _headIndexText;
        [SerializeField] private Image _bodyIcon;
        [SerializeField] private TextMeshProUGUI _bodyIndexText;

        [Header("Available sprites")]
        [SerializeField] private List<Sprite> _headSprites;
        [SerializeField] private List<Sprite> _bodySprites;

        private int _headIndex;
        private int _bodyIndex;

        // TODO: Load x amount of sprite options depending on which mode & pick random ones (?)
        // TODO: Add which indexes have been visited & send to some data collection thing

        public void IncrementHead()
        {
            ModifyIndex(1, CharacterSpriteType.Head);
            UpdateUI();
        }

        public void DecrementHead()
        {
            ModifyIndex(-1, CharacterSpriteType.Head);
            UpdateUI();
        }

        public void IncrementBody()
        {
            ModifyIndex(1, CharacterSpriteType.Body);
            UpdateUI();
        }

        public void DecrementBody()
        {
            ModifyIndex(-1, CharacterSpriteType.Body);
            UpdateUI();
        }

        private void ModifyIndex(int indexDelta, CharacterSpriteType spriteType)
        {
            if (indexDelta == 0) { return; }

            var oldIndex = spriteType == CharacterSpriteType.Head ? _headIndex : _bodyIndex;
            var spriteListMax = spriteType == CharacterSpriteType.Head ? _headSprites.Count - 1 : _bodySprites.Count - 1;

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

        private void UpdateUI()
        {
            // Head
            var newHead = _headSprites[_headIndex];
            _head.sprite = newHead;
            _headIcon.sprite = newHead;
            _headIndexText.text = $"{_headIndex + 1}/{_headSprites.Count}";

            // Body
            var newBody = _bodySprites[_bodyIndex];
            _body.sprite = newBody;
            _bodyIcon.sprite = newBody;
            _bodyIndexText.text = $"{_bodyIndex + 1}/{_bodySprites.Count}";
        }
    }
}
