using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Loki_AttackSound : MonoBehaviour
    {

        private AudioSource _source;
        // Use this for initialization
        void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlaySound()
        {
            SoundManager.instance.PlaySound("loki_attack2", _source, false, false);
        }
    }
}