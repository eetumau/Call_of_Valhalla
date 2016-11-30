﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla
{
    public class SoundManager : MonoBehaviour
    {

        public AudioSource musicSource;

        [SerializeField]
        private AudioClip[] _music;
        [SerializeField]
        private AudioClip[] _sfx;

        [SerializeField]
        private float lowPitchRange = 0.95f;
        [SerializeField]
        private float highPitchRange = 1.05f;

        public static SoundManager instance = null;


        // Use this for initialization
        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }


        public void SetMusic(string name)
        {
            for (int i = 0; i < _music.Length; i++)
            {
                if (_music[i].name == name)
                {
                    musicSource.clip = _music[i];
                    musicSource.Play();
                }
            }
        }

        public void PlaySound(string name, AudioSource source)
        {
            for (int i = 0; i < _sfx.Length; i++)
            {
                if (_sfx[i].name == name)
                {
                    float randomPitch = Random.Range(lowPitchRange, highPitchRange);
                    source.pitch = randomPitch;

                    source.clip = _sfx[i];
                    source.Play();
                }
            }
        }

    }
}