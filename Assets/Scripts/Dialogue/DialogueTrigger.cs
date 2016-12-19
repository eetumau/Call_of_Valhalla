using UnityEngine;
using System.Collections;
using CallOfValhalla.Dialogue;

namespace CallOfValhalla.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        private TextBoxManager _textBoxManager;
        private GameObject _trigger;

        // Use this for initialization
        void Awake()
        {
            _textBoxManager = FindObjectOfType<TextBoxManager>();
            _trigger = gameObject;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        	if (other.tag == "Player")
        	{
            	_textBoxManager.EnableTextBox();
            	_trigger.SetActive(false);
        	}
        }
    }
}