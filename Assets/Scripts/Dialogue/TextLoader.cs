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

            Debug.Log(tmp);
            if (tmp.Equals("level1"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level1", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level2"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level2", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level3"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level3", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level4"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level4", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level5"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level5", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level6"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level6", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level7"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level7", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level8"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level8", typeof(TextAsset)) as TextAsset;
            if (tmp.Equals("Level9"))
                _currentTextFile = Resources.Load("Dialogue/Dialogue_Level9", typeof(TextAsset)) as TextAsset;




        }

        public TextAsset GetTextAsset()
        {

            LoadTextAsset();

            return _currentTextFile;
        }
    }
}