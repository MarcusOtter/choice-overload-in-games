using System;
using System.Collections.Generic;
using Scripts.Examination;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Scripts.Menu_and_UI
{
    public class CharacterCreation : MonoBehaviour
    {
        [Header("Big character preview")]
        [SerializeField] private Image _headRenderer;
        [SerializeField] private Image _bodyRenderer;

        [Header("Icons and index text")]
        [SerializeField] private Image _headIcon;
        [SerializeField] private TextMeshProUGUI _headIndexText;

        [SerializeField] private Image _bodyIcon;
        [SerializeField] private TextMeshProUGUI _bodyIndexText;

        [Header("Other references")]
        [SerializeField] private TMP_InputField _nameInput;

        [Header("Available sprites")]
        [SerializeField] private AvailableSprites _availableSprites; // TODO: Hide from inspector.

        private int _headIndex;
        private int _bodyIndex;

        private readonly List<int> _visitedHeads = new List<int>();
        private readonly List<int> _visitedBodies = new List<int>();

        private float _startTime;

        private float _timeSpentOnName;

        private void Start()
        {
            _availableSprites = ExaminationHandler.Instance?.GetAvailableSprites();

            _visitedHeads.Add(1);
            _visitedBodies.Add(1);
            _startTime = Time.time;

            UpdateUi();
        }

        private void Update()
        {
            if (_nameInput.isFocused)
            {
                _timeSpentOnName += Time.deltaTime;
            }
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
            if (ExaminationHandler.Instance == null)
            {
                Logger.Instance.LogError("Couldn't find the Examination Handler");
                SceneTransitioner.Instance.LoadNextScene();
                return;
            }

            var optionAmount = ExaminationHandler.Instance.GetAvailableSprites().OptionAmount;

            var characterData = new CharacterData
            (
                hasChangedName:             !string.IsNullOrWhiteSpace(_nameInput.text),
                characterName:              _nameInput.text,
                timeSpentOnName:            Math.Round(_timeSpentOnName, 2),
                spriteOptionAmount:         optionAmount,
                visitedHeadsPercentage:     Math.Round((_visitedHeads.Count / (double)optionAmount) * 100d, 2),
                visitedBodiesPercentage:    Math.Round((_visitedBodies.Count / (double) optionAmount) * 100d, 2),
                selectedHead:               _headIndex + 1,
                selectedBody:               _bodyIndex + 1,
                isMatchingSet:              _headIndex == _bodyIndex,
                timeSpentOnCharacter:       Math.Round(Time.time - _startTime, 2)
            );
            DataCollector.Instance.SetCharacterData(characterData);

            SceneTransitioner.Instance.LoadNextScene();
        }

        private void ModifyIndex(int indexDelta, CharacterSpriteType spriteType)
        {
            if (indexDelta == 0) { return; }

            var index = spriteType == CharacterSpriteType.Head 
                ? _headIndex 
                : _bodyIndex;

            var spriteListMax = spriteType == CharacterSpriteType.Head 
                ? _availableSprites.HeadSprites.Count - 1 
                : _availableSprites.BodySprites.Count - 1;

            if (indexDelta > 0)
            {
                // Increment and prevent index from falling out of bounds
                index = index >= spriteListMax ? 0 : index + 1;
            }
            else
            {
                // Decrement and prevent index from falling out of bounds
                index = index == 0 ? spriteListMax : index - 1;
            }

            switch (spriteType)
            {
                case CharacterSpriteType.Head:
                    _headIndex = index;
                    if (!_visitedHeads.Contains(_headIndex + 1)) { _visitedHeads.Add(_headIndex + 1); }
                    break;

                case CharacterSpriteType.Body:
                    _bodyIndex = index;
                    if (!_visitedBodies.Contains(_bodyIndex + 1)) { _visitedBodies.Add(_bodyIndex + 1); }
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
