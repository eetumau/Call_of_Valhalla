using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
        
        private TextLoader _textLoader;
        private int _currentLine = 0;
        private int _endAtLine;
        private string _tempString;
        private bool _nextText;
        private bool _textBoxActive;

        // Use this for initialization
        void Awake()
        {
            _textLoader = GetComponent<TextLoader>();

            PrepareText();


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
                    _currentLine += 1;

                Debug.Log(_textLines[_currentLine].Length);

                if (_textLines[_currentLine].Contains("(end)"))
                {                    
                    DisableTextBox();
                }

                _theText.text = _textLines[_currentLine];
            }
        }

        public void EnableTextBox()
        {
            if (_textFile == null)
            {
                PrepareText();
            }

            _currentLine += 1;
            _textBox.SetActive(true);
            _textBoxActive = true;
        }

        public void DisableTextBox()
        {
            _textBox.SetActive(false);
            _textBoxActive = false;
        }
    }
}