using UnityEngine;
using System.Collections;
using System;

namespace CallOfValhalla.Dialogue
{
    public class TextLoader : MonoBehaviour
    {

        private TextAsset _currentTextFile;

        // Use this for initialization
        void Awake()
        {
            LoadTextAsset();
        }

        private void LoadTextAsset()
        {
            string tmp = Application.loadedLevelName;

            
            if (tmp.Equals("level1"))
            {                
                _currentTextFile = Resources.Load("Dialogue/LevelOneDialogue", typeof(TextAsset)) as TextAsset;                
            }

            
        }

        public TextAsset GetTextAsset()
        {

            LoadTextAsset();

            return _currentTextFile;
        }
    }
}