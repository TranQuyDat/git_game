using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] private float movespeed;
        [SerializeField] private Transform fier_pos;
        [SerializeField] private float fier_cooldown;
        [SerializeField] private fierLazeControler fier_Laze;
        [SerializeField] private int HP;
        
        private int cur_hp;
        private float tmpcooldwn;

        public Action<int, int> onhpchange;

        // Start is called before the first frame update
        void Start()
        { 
            cur_hp = HP;
            if (onhpchange != null)
                onhpchange(cur_hp, HP);
        }
      
        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isActive()) return;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 diretion = new Vector2(horizontal, vertical);
            transform.Translate(diretion * Time.deltaTime * movespeed);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -16.66f, 16.66f),
                        Mathf.Clamp(transform.position.y, -8.99f, 8.99f),transform.position.z);

            if (Input.GetKey(KeyCode.Space))
            {
                if (tmpcooldwn <= 0)
                {
                    tmpcooldwn = fier_cooldown;
                    this.fier();
                }
            }
            tmpcooldwn -= Time.deltaTime;

        }

        public void Hit(int damage)
        {
            Audiomanager.instance.playHitmusic();
            cur_hp -= damage;
            if (onhpchange != null)
                onhpchange(cur_hp, HP);
            if (cur_hp <= 0)
            {
                Audiomanager.instance.playDestroymusic();
                Destroy(gameObject);
                SpawnManager.instance.DestroyShipFX(transform.position, null);
                GameManager.instance.gameOver(false);
                
            }
        }

        private void fier()
        {
            Audiomanager.instance.playLazermusic();
            SpawnManager.instance.ShootingFX(fier_pos.position,null);
            fierLazeControler fierlaze = SpawnManager.instance.Pshoot(fier_pos.position,null);
            fierlaze.setlayer("Player");
            fierlaze.lineTime(1);
        }

    }
}