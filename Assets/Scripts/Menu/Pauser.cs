using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class Pauser : MonoBehaviour
    {

        private bool _paused = false;

        public void TogglePause()
        {
            _paused = !_paused;

            if (_paused)
            {
                Time.timeScale = 0;
            }else
            {
                Time.timeScale = 1;
            }
        }
    }
}