using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class fierLazeControler : MonoBehaviour
    {
        [SerializeField] private float movespeed;
        [SerializeField] private Vector2 direction;
        [SerializeField] private int damage;

        private float cur_wave_speed;
        private float timedelayProjT;
   
        private string Layer;
        // Start is called before the first frame update
        void Start()
        {
    
           
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isActive()) return;
            timedelayProjT -= Time.deltaTime;
            if (timedelayProjT <= 0)
            {

                if (Layer == "Enemy")
                {
                    Debug.Log("ok");
                    SpawnManager.instance.deleteprjt_e(this);
                }
                if (Layer == "Player")
                    SpawnManager.instance.deleteprjt_P(this);
            }


            transform.Translate(direction * Time.deltaTime * (movespeed + cur_wave_speed));
        }
        //setlayer

        public void setlayer(string layer)
        {
            this.Layer = layer;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger " + collision.gameObject.name);
            if (collision.CompareTag("Enemy"))
            {
                Vector3 pos = collision.ClosestPoint(transform.position);
                SpawnManager.instance.HitFX(pos, null);
                SpawnManager.instance.deleteprjt_P(this);
                EnemyControler enemy;
                collision.gameObject.TryGetComponent(out enemy);
                enemy.Hit(damage);
            }

            if (collision.CompareTag("Player"))
            {
                Vector3 pos = collision.ClosestPoint(transform.position);
                SpawnManager.instance.HitFX(pos, null);
                SpawnManager.instance.deleteprjt_e(this);
                PlayerControler player;
                collision.gameObject.TryGetComponent(out player);
                player.Hit(damage);
            }

        }

        public void lineTime(int wave)
        {
            cur_wave_speed = wave;
            timedelayProjT = 5f;
        }

        

        

    }
}