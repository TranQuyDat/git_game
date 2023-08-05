using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class ParticleFX : MonoBehaviour
    {
        [SerializeField] private float LifeTime;
        private particlePool Partclefxpool;
        // Start is called before the first frame update
        private float cur_time;
    
        private void OnEnable()
        {
            cur_time = LifeTime;
        }
        void Start()
        { 
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isActive()) return;

            if (cur_time <= 0)
            {
                Partclefxpool.clear(this);
            }
            cur_time -= Time.deltaTime;
        }

        public void  setPool(particlePool pool)
        {
            Partclefxpool = pool;
        }
    }
}