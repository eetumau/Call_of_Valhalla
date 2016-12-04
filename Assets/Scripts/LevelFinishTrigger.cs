using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla {
    public class LevelFinishTrigger : MonoBehaviour {


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.Instance.LevelCompleted = GameManager.Instance.Level;
                GameManager.Instance.Save();
                GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();

            }
        }


    }
}