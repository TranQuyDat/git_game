using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{ 
    

   
    
    public class EnemyControler : MonoBehaviour
    {
        [SerializeField] private float movespeed;
        [SerializeField] private Transform[] waypoint;

        [SerializeField] private Transform fier_pos;

        [SerializeField] private float min_fier_cooldown;
        [SerializeField] private float max_fier_cooldown;

        [SerializeField] private fierLazeControler fier_Laze;
        [SerializeField] private int HP;
 
        protected float tmpcooldwn;
        private int cur_point;
        private int cur_hp;
        private wavedata cur_wave;
        

        // Start is called before the first frame update
        void Start()
        {
        }
        
        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isActive()) return;
            int next_point = cur_point+1 ;
            if(next_point > waypoint.Length - 1)
            {
                next_point = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoint[next_point].position, (movespeed + cur_wave.speedmultiplier) * Time.deltaTime);

            if(transform.position == waypoint[next_point].position)
            {
                cur_point = next_point;
            }

            Vector3 direction = waypoint[next_point].position - transform.position;
            float alpha = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(alpha+90, Vector3.forward);

            if (tmpcooldwn <= 0)
            {
                tmpcooldwn = Random.Range(min_fier_cooldown, max_fier_cooldown);
                tmpcooldwn /= cur_wave.speedmultiplier;
                this.fier();
            }
        
             tmpcooldwn -= Time.deltaTime;
        
        }

         private void fier()
        {
            Audiomanager.instance.playPlasmaMusic();
            fierLazeControler fierlaze = SpawnManager.instance.Eshoot(fier_pos.position,null) ;
            fierlaze.setlayer("Enemy");
            fierlaze.lineTime(cur_wave.speedmultiplier);
        }

        public void Hit(int damage)
        {
            Audiomanager.instance.playHitmusic();
            cur_hp -= damage;
            if (cur_hp <= 0)
            {
                Audiomanager.instance.playDestroymusic();
                SpawnManager.instance.delete(this);
                SpawnManager.instance.DestroyShipFX(transform.position, null);
                GameManager.instance.addscore(1);
            }
        }

        public void Init(Transform[] wp,wavedata wave)
        {
            cur_wave = wave;
            waypoint = wp;
            cur_hp = HP;
            //active = true;
            transform.position = waypoint[0].position;
        }
    }
}