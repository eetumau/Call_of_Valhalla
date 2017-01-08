using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class LightningFlash : MonoBehaviour
    {

        [SerializeField]
        private CanvasGroup _cG;

        private bool flash;


        public IEnumerator Flash()
        {
            _cG.alpha = 1;

            while(_cG.alpha > 0)
            {
                _cG.alpha -= Time.deltaTime;
                yield return null;

            }

            _cG.alpha = 0;
        }

    }
}