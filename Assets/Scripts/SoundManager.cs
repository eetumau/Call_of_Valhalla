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

        private float _musicVolume;
        private float _soundVolume;

        private bool _musicMuted = false;
        private bool _soundMuted = false;


        public static SoundManager instance = null;

        public bool MusicMuted
        {
            get { return _musicMuted; }
            set { _musicMuted = value; }
        }
        public bool SoundMuted
        {
            get { return _soundMuted; }
            set { _soundMuted = value; }
        }

        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = value;
                if (!_musicMuted)
                {
                    musicSource.volume = _musicVolume;
                }
            }
        }

        public float SoundVolume
        {
            get { return _soundVolume; }
            set { _soundVolume = value; }
        }

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

        public void PlaySound(string name, AudioSource source, bool loop = false, bool noRandomPitch = false)
        {
            for (int i = 0; i < _sfx.Length; i++)
            {
                if (_sfx[i].name == name)
                {
                    if (!noRandomPitch)
                    {
                        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
                        source.pitch = randomPitch;
                    }
                   
                    source.clip = _sfx[i];

                    if (_soundMuted)
                    {
                        source.volume = 0;
                    }else
                    {
                        source.volume = _soundVolume;
                    }

                    if (loop)
                    {
                        source.loop = true;
                    }
                    else
                    {
                        source.loop = false;
                    }

                    source.Play();
                }
            }
        }

        public void ToggleMusic(bool toggle)
        {
            _musicMuted = !toggle;

            if (_musicMuted)
            {
                musicSource.volume = 0;
            }else
            {
                musicSource.volume = _musicVolume;
            }

            GameManager.Instance.Save();
        }

        public void ToggleSound(bool toggle)
        {
            _soundMuted = !toggle;
            GameManager.Instance.Save();
        }

    }
}