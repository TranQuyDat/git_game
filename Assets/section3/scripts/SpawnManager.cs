using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{

    [System.Serializable]
    public class EnemiPool
    {
        public EnemyControler prefab;
        public List<EnemyControler> deadenemy;
        public List<EnemyControler> liveenemy;

        public EnemyControler Spawn(Vector3 pos, Transform parent)
        {
            if (deadenemy.Count == 0)
            {
                EnemyControler eobj = GameObject.Instantiate(prefab, parent);
                eobj.transform.position = pos;
                liveenemy.Add(eobj);
                return eobj;
            }
            else
            {
                EnemyControler eobj = deadenemy[0];
                eobj.gameObject.SetActive(true);
                eobj.transform.SetParent(null);
                eobj.transform.position = pos;
                liveenemy.Add(eobj);
                deadenemy.RemoveAt(0);
                return eobj;
            }
        }
        public void delete(EnemyControler prefab)
        {
            if (liveenemy.Contains(prefab))
            {
                deadenemy.Add(prefab);
                liveenemy.Remove(prefab);
                prefab.gameObject.SetActive(false);
            }
        }

        public void clearall()
        {
            while (liveenemy.Count > 0)
            {
                EnemyControler obj = liveenemy[0];
                obj.gameObject.SetActive(false);
                deadenemy.Add(obj);
                liveenemy.RemoveAt(0);
            }
        }

    }
    [System.Serializable]
    public class projectilePool
    {
        public fierLazeControler prefab;
        public List<fierLazeControler> after_shoot;
        public List<fierLazeControler> shooting;
        
        public fierLazeControler shoot(Vector3 pos, Transform parent)
        {
            if (after_shoot.Count == 0)
            {
                fierLazeControler obj = GameObject.Instantiate(prefab,parent);
                obj.transform.position = pos;
                shooting.Add(obj);
                return obj;
            }
            else
            {
                fierLazeControler obj = after_shoot[0];
                obj.gameObject.SetActive(true);
                obj.transform.position = pos;

                shooting.Add(obj);
                after_shoot.RemoveAt(0);
                return obj;
            }
        }
        public void deleteprjt(fierLazeControler prefab)
        {
            if (shooting.Contains(prefab))
            {
                after_shoot.Add(prefab);
                shooting.Remove(prefab);
                prefab.gameObject.SetActive(false);
            }
        }
        
        public void clearall()
        {
            while (shooting.Count > 0)
            {
                fierLazeControler obj = shooting[0];
                obj.gameObject.SetActive(false);
                after_shoot.Add(obj);
                shooting.RemoveAt(0);
            }
        }

    }

    [System.Serializable]
    public class particlePool 
    {

        public ParticleFX prefab;

        public List<ParticleFX> Enab_particleFX;
        public List<ParticleFX> dis_particleFX;

        public ParticleFX spawnFX(Vector3 pos,Transform parent )
        {
            if (dis_particleFX.Count <= 0)
            {
                ParticleFX obj =GameObject.Instantiate(prefab,parent);
                obj.transform.position = pos;
                Enab_particleFX.Add(obj);
                return obj;
            }

            else
            {
                ParticleFX obj = dis_particleFX[0];
                obj.transform.position = pos;
                /*obj.transform.SetParent(parent);*/
                obj.gameObject.SetActive(true);
                Enab_particleFX.Add(obj);
                dis_particleFX.RemoveAt(0);
                return obj;
            }

        }
        public void clear(ParticleFX prefab)
        {
            if (Enab_particleFX.Contains(prefab))
            {
                dis_particleFX.Add(prefab);
                Enab_particleFX.Remove(prefab);
                prefab.gameObject.SetActive(false);
            }
        }
        
        public void clearall()
        {
            while (Enab_particleFX.Count > 0)
            {
                ParticleFX obj = Enab_particleFX[0];
                obj.gameObject.SetActive(false);
                dis_particleFX.Add(obj);
                Enab_particleFX.RemoveAt(0);
            }
        }
    }

    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager m_instance;
        public static SpawnManager instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = FindObjectOfType<SpawnManager>();
                return m_instance;
            }
        }

        [SerializeField] private bool active = false;
        [SerializeField] private waypointPath[] path;

        [SerializeField] private int num_min_enemy;
        [SerializeField] private int num_max_enemy;
        [SerializeField] private float timespaw;
        [SerializeField] private int gr_enemi;
        [SerializeField] private PlayerControler prefabPlayer;

        [SerializeField] private projectilePool pjtP_p;
        [SerializeField] private projectilePool pjtP_e;
        [SerializeField]  private EnemiPool EP;

        //particleFX
        [SerializeField] private particlePool HitFXpool;
        [SerializeField] private particlePool ShootingFXpool;
        [SerializeField] private particlePool DestroyShipFXpool;
 
        
        public PlayerControler getPlayer => player;
        private PlayerControler player;
        private bool isspawning = true;
        private wavedata cur_wave;
        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;
            else if (m_instance != this)
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            
         
        }
        
        //play battle
        public void startbattle(wavedata wave)
        {
            cur_wave = wave;
            if (GameManager.instance.isActive()) return;
            Spawnplayer();
            StartCoroutine(IEspawGrenemi(gr_enemi));
        }

        
        //clear game
        public void Clear()
        {
            HitFXpool.clearall();
            ShootingFXpool.clearall();
            DestroyShipFXpool.clearall();
            EP.clearall();
            pjtP_e.clearall();
            pjtP_p.clearall();
            if(player!=null)
                Destroy(player.gameObject);
        }
        
        
        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.getstate() == gamestate.gameplay)
            {
                this.active = true;

            }
            else this.active = false;
        }
        
        
        //spaw grp enemy
        private IEnumerator IEspawGrenemi(int gr_enemi)
        {
            isspawning = true;
            for (int i = 0; i < gr_enemi; i++)
            {
                int totalenemi = Random.Range(cur_wave.mintotalenemi, cur_wave.maxtotalenemi);
                waypointPath p = path[Random.Range(0, path.Length - 1)];
                StartCoroutine(IEspawnEnemy(totalenemi, p));
                if(i < gr_enemi-1)
                    yield return new WaitForSeconds(3f/cur_wave.speedmultiplier);
            }
            isspawning = false;
        }
        
        
        
        //ktr clear
        public bool isclear()
        {
            if (isspawning ||EP.liveenemy.Count>0 )
                return false;
            return true;
        }
        
        
        //spaw player
        private void Spawnplayer()
        {
            if (player == null)
            {
                player = Instantiate(prefabPlayer);
                player.transform.position = new Vector3(0, -6, 0);
            }
        }
        
        
        
        
        //spaw enemy
        private IEnumerator IEspawnEnemy(int totalenemi,waypointPath path)
        {

            
            for (int i = 0; i < totalenemi; i++)
            {
                yield return new WaitUntil(() => active);
                yield return new WaitForSeconds(timespaw/cur_wave.speedmultiplier);

                EnemyControler enemy = EP.Spawn(path.wp[0].position, null);
                enemy.Init(path.wp,cur_wave);
            }
        }



        //projectilePool
        public fierLazeControler Eshoot (Vector3 pos, Transform parent)
        {
            
           return pjtP_e.shoot(pos,parent);
        }
        public void deleteprjt_e(fierLazeControler obj)
        {
            pjtP_e.deleteprjt(obj);
        }
        public fierLazeControler Pshoot(Vector3 pos,Transform parent)
        {
            
            return pjtP_p.shoot(pos,parent);
        }
        public void deleteprjt_P(fierLazeControler obj)
        {
            pjtP_p.deleteprjt(obj);
        }

        
        
        //Enemy Pool
        public void delete(EnemyControler obj)
        {
            EP.delete(obj);
        }

        
        
        
        
        //ParticleFX pool
        public ParticleFX HitFX( Vector3 pos,Transform parent)
        {
            ParticleFX fx = HitFXpool.spawnFX( pos, null);
            fx.setPool(HitFXpool);
            return fx;
        }

        public ParticleFX ShootingFX( Vector3 pos, Transform parent)
        {
            ParticleFX fx = ShootingFXpool.spawnFX( pos, null);
            fx.setPool(ShootingFXpool);
            return fx;
        }

        public ParticleFX DestroyShipFX(Vector3 pos, Transform parent)
        {
            ParticleFX fx = DestroyShipFXpool.spawnFX(pos, parent);
            fx.setPool(DestroyShipFXpool);
            return fx;
        }

    }

}