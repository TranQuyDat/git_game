using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class Audiomanager : MonoBehaviour
    {
        private static Audiomanager m_instance;
        public static Audiomanager instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = FindObjectOfType<Audiomanager>();
                return m_instance;
            }
        }
        [SerializeField] AudioSource music;
        [SerializeField] AudioSource SFX;
        [SerializeField] AudioSource echo;

        [SerializeField] AudioClip Homemusic;
        [SerializeField] AudioClip Battlemusic;
        [SerializeField] AudioClip Hitmusic;
        [SerializeField] AudioClip Plasmamusic;
        [SerializeField] AudioClip Lazermusic;
        [SerializeField] AudioClip Destroymusic;

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;
            else if (m_instance != this)
                Destroy(gameObject);
        }

        public void playHomeMusic()
        {
            if (music.clip == Homemusic) return;
            music.loop = true;
            music.clip = Homemusic;
            music.Play();
        }

        public void playBattlrmusic()
        {
            if (music.clip == Battlemusic) return;
            music.loop = true;
            music.clip = Battlemusic;
            music.Play();
        }

        public void playHitmusic()
        {
            SFX.pitch =Random.Range(1f,2f);
            SFX.PlayOneShot(Hitmusic);
        }

        public void playLazermusic()
        {
            SFX.pitch = Random.Range(1f, 2f);
            SFX.PlayOneShot(Lazermusic);
        }

        public void playPlasmaMusic()
        {
            SFX.pitch = Random.Range(1f, 2f);
            SFX.PlayOneShot(Plasmamusic);
        }

        public void playDestroymusic()
        {
            echo.pitch = Random.Range(1f, 2f);
            echo.PlayOneShot(Destroymusic);
        }


    }
}