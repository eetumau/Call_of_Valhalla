using UnityEngine;
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
        [SerializeField] private GameObject _objectToMove;
        private GameObject _dialogueMovePoint;
        private GameObject _endLevelTrigger;
        private Player_CameraFollow _cameraFollow;
        [SerializeField]      
        private Text _theText;
        private NPC _npc;
        private Player_Movement _playerMovement;

        private Player_InputController _playerInput;
        private WeaponController _weaponController;
        private TextLoader _textLoader;
        private int _currentLine = 0;
        private string _tempString;
        private bool _nextText;
        private bool _textBoxActive;
        public bool _needsToMove;
        public bool _endsLevel;
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
            _npc = FindObjectOfType<NPC>();
            _weaponController = FindObjectOfType<WeaponController>();

            if (_needsToMove)
                _dialogueMovePoint = GameObject.Find("DialogueMovePoint");
            if (_endsLevel)
            {
                _endLevelTrigger = GameObject.Find("EndLevelTrigger");
                _endLevelTrigger.SetActive(false);
            }

            _playerMovement = FindObjectOfType<Player_Movement>();
            _textBox.SetActive(false);
            _cameraFollow = FindObjectOfType<Player_CameraFollow>();
        }

        private void PrepareText()
        {
            _textFile = _textLoader.GetTextAsset();
            
            if (_textFile != null)
            {
                _textLines = (_textFile.text.Split('\n'));
            }
            _theText.text = _textLines[_currentLine];
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
                        if (_textLines[_currentLine].Contains("(move)"))
                        {
                            DisableTextBox();
                            _objectToMove.transform.position = _dialogueMovePoint.transform.position;
                        }
                        if (_textLines[_currentLine].Contains("(equiphammer)"))
                        {
                            _weaponController.EnableHammer();
                            _weaponController.ChangeCurrentWeapon();
                        }
                        if (_textLines[_currentLine].Contains("(endlevel)"))
                        {
                            DisableTextBox();
                            _endLevelTrigger.SetActive(true);
                        }
                        if (_textLines[_currentLine].Contains("(change_thor)"))
                        {
                            _npc.SetThor();
                            _currentLine += 1;
                        }
                        if (_textLines[_currentLine].Contains("(change_thorhammer)"))
                        {
                            _npc.SetThorHammer();
                            _currentLine += 1;
                        }
                        if (_textLines[_currentLine].Contains("(change_odin)"))
                        {
                            _npc.SetOdin();
                            _currentLine += 1;
                        }                        
                        else
                        {
                            StartCoroutine(TextScroll(_textLines[_currentLine]));
                        }
                            
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

            
            _currentLine += 1;
            _playerInput.DisableControls(true);

            if (_playerMovement._isGrounded)
            {
                _textBox.SetActive(true);
                _textBoxActive = true;
                StartCoroutine(TextScroll(_textLines[_currentLine]));
                _cameraFollow.StartCenteringCamera();
            } else
                StartCoroutine(TextBoxDelay());
            
        }

        private IEnumerator TextBoxDelay()
        {

            yield return new WaitForSeconds(0.5f);
            _textBox.SetActive(true);
            _textBoxActive = true;
            StartCoroutine(TextScroll(_textLines[_currentLine]));
            _cameraFollow.StartCenteringCamera();
        }
    

        public void DisableTextBox()
        {
            _playerInput.DisableControls(false);
            _textBox.SetActive(false);
            _textBoxActive = false;
        }
    }
}