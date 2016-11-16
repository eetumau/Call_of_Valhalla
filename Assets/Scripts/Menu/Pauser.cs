using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CallOfValhalla
{
    public class Pauser : MonoBehaviour
    {

        private Canvas _canvas;
        private GraphicRaycaster _raycaster;

        private bool _paused = false;

        private void Start()
        {
            GameManager.Instance.Pauser = this;
            _canvas = GetComponentInChildren<Canvas>();
            _raycaster = GetComponentInChildren<GraphicRaycaster>();
        }

        public void TogglePause()
        {
           
            _paused = !_paused;

            if (_paused)
            {
                Debug.Log("Paused");
                _canvas.enabled = true;
                Time.timeScale = 0;
            }else
            {
                Debug.Log("Unpaused");
                _canvas.enabled = false;
                Time.timeScale = 1;
            }
        }
    }
}