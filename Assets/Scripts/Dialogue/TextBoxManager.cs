﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using CallOfValhalla.Player;

namespace CallOfValhalla.Dialogue
{
    public class TextBoxManager : MonoBehaviour
    {

        private TextAsset _textFile;
        [SerializeField]
        private string[] _textLines;

        [SerializeField]
        private GameObject _textBox;

        [SerializeField]
        private Text _theText;

        private Player_InputController _playerInput;
        private TextLoader _textLoader;
        private int _currentLine = 0;
        private int _endAtLine;
        private string _tempString;
        private bool _nextText;
        private bool _textBoxActive;

        private bool _isTyping;
        private bool _cancelTyping;
        [SerializeField]
        private float _typeSpeed;

        // Use this for initialization
        void Awake()
        {
            _textLoader = GetComponent<TextLoader>();

            PrepareText();
            _playerInput = FindObjectOfType<Player_InputController>();

            Debug.Log(_textLines.Length);

            _textBox.SetActive(false);

        }

        private void PrepareText()
        {
            _textFile = _textLoader.GetTextAsset();

            if (_textFile != null)
            {
                
                _textLines = (_textFile.text.Split('\n'));
            }
            _endAtLine = _textLines.Length - 1;
        }

        private void Update()
        {
            if (_textBoxActive)
            {                

                if (Input.GetButtonDown("Continue"))
                {
                    if (!_isTyping)
                    {
                        _currentLine += 1;

                        if (_textLines[_currentLine].Contains("(end)"))
                            DisableTextBox();
                        else
                            StartCoroutine(TextScroll(_textLines[_currentLine]));
                    }
                    else if (_isTyping && !_cancelTyping)
                    {
                        _cancelTyping = true;
                    }                    
                }                    

            }
        }

        private IEnumerator TextScroll(string LineOfText)
        {
            int letter = 0;
            _theText.text = "";
            _isTyping = true;
            _cancelTyping = false;

            while (_isTyping && !_cancelTyping && letter < (LineOfText.Length -1))
            {
                _theText.text += LineOfText[letter];
                letter += 1;
                yield return new WaitForSeconds(_typeSpeed);
            }

            _theText.text = LineOfText;
            _isTyping = false;
            _cancelTyping = false;

        }

        public void EnableTextBox()
        {
            if (_textFile == null)
            {
                PrepareText();
            }

            _playerInput.DisableControls(true);
            _currentLine += 1;
            _textBox.SetActive(true);
            _textBoxActive = true;
        }

        public void DisableTextBox()
        {
            _playerInput.DisableControls(false);
            _textBox.SetActive(false);
            _textBoxActive = false;
        }
    }
}