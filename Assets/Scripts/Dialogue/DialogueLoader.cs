using UnityEngine;
using System.Collections;
using System;

namespace CallOfValhalla.Dialogue
{
    public class DialogueLoader : MonoBehaviour
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
            

            if (tmp.Equals("Level1"))
                _currentTextFile = Resources.Load("Dialogue/LevelOneDialogue", typeof(TextAsset)) as TextAsset;

            Debug.Log("LOADED");
        }


        public TextAsset GetTextAsset()
        {
            return _currentTextFile;
        }
    }
}