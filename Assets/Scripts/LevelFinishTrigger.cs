using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla {
    public class LevelFinishTrigger : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();
            }
        }


    }
}