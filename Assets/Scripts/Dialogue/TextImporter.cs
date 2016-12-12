using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

    [SerializeField]
    private TextAsset _textFile;
    [SerializeField]
    private string[] _textLines;

	// Use this for initialization
	void Awake () {
	    
        if (_textFile != null)
        {
            _textLines = (_textFile.text.Split('\n'));
        }
	}
	

}
