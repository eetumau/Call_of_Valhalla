using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CallOfValhalla {
    public class GameComplete : MonoBehaviour {

        [SerializeField]
        private Text _congText;
        [SerializeField]
        private RawImage _creditText;

        private int paska;
        private Vector3 _creditPos;
        private float _newPos;
        private bool _ended;

        // Use this for initialization
        void Start()
        {
            _creditPos = _creditText.transform.position;
            _newPos = _creditPos.y;

            StartCoroutine(FadeText());

        }

        // Update is called once per frame
        void Update() {

            if (Input.GetKeyDown(KeyCode.Space) && _ended)
            {
                GameManager.Instance.MainMenu();
            }

        }

        private IEnumerator FadeText()
        {

            yield return new WaitForSeconds(5);
            _congText.CrossFadeAlpha(0, 1, true);

            yield return new WaitForSeconds(1);
            _ended = true;
            StartCoroutine(RollCredits());
           
        }

        private IEnumerator RollCredits()
        {
            while(_creditText.transform.position.y < 2700)
            {

                _newPos += 0.5f;
                _creditText.transform.position = new Vector3(_creditPos.x, _newPos, _creditPos.z);
                yield return null;

            }

           

        }


    }
}