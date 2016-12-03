using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla {
    public class LevelFinishTrigger : MonoBehaviour {


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            }
        }


    }
}