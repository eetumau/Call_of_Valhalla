using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CallOfValhalla {
    public class LevelFinishTrigger : MonoBehaviour {

        private AudioSource _source;

        void Start()
        {
            _source = gameObject.AddComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                if(GameManager.Instance.Level >= GameManager.Instance.LevelCompleted)
                {
                    GameManager.Instance.LevelCompleted = GameManager.Instance.Level;
                }
                GameManager.Instance.Save();
                SoundManager.instance.PlaySound("level_finish", _source);
                GameManager.Instance.LevelCompleteUI.ToggleLevelCompleteUI();

            }
        }

    }
}