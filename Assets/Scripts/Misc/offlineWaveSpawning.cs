using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using UnityEngine.SceneManagement;

public class offlineWaveSpawning : MonoBehaviour {

    public bool ActiveinScene;
    LayerMask playerLayer;
    public float randomRange = 2;
    public float radius = 10f;
    GameObject waveTextObj;
    Text waveText;
    List<GameObject> Players = new List<GameObject>();
    m_Health[] p_hp = new m_Health[10];

    public GameObject Minion;
    public GameObject Gunner;
    public GameObject suicideBomber;
    public GameObject[] enemySpawnPoints = new GameObject[25];
    Transform[] enemyBaseTransforms = new Transform[10];
    public bool[] spawnAvailable = new bool[25];

    public List<GameObject> currentEnemies;
    public float BaseEnemyCount = 20;
    float EnemyAmt = 0;

    public Transform maxX;
    public Transform maxY;
    public Transform minX;
    public Transform minY;

    public int currentWave = 0;

    bool everyBodyIsDead = false;
    int amtPlayerDowned = 0;

    public GameObject endScreenCanvas;
    public bool gameHasBegun;

    // Use this for initialization
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        if (ActiveinScene)
        {
            waveTextObj = GameObject.Find("waveTextObj");
            waveText = waveTextObj.GetComponent<Text>();
            waveText.text = "Wave: 1";

            int x = 0;
            while (x < 25)
            {
                enemySpawnPoints[x] = GameObject.Find("enemySpawn");
                enemySpawnPoints[x].name = enemySpawnPoints[x].name + x;
                x++;
            }
            currentEnemies = new List<GameObject>();

            currentWave = 0;
        }
    }

    public void addPlayer(GameObject pToAdd)
    {
        Players.Add(pToAdd);
        if (gameHasBegun == false)
        {
            gameHasBegun = true;
        }
        return;
    }
    
    
    private void Update()
    {

        

        //Debug.Log(EnemyAmt);
        if (ActiveinScene)
        {
            if (gameHasBegun)
            {
                if (EnemyAmt == 0)
                {

                    //Debug.Log("Enemies are missing, players are active, proceeding to next wave");
                    NewWave();
                    UpdatePossibleSpawns();
                    CmdSpawnEnemies(currentWave);

                }
            }
        }
    }
    

    public void playerDown()
    {
        amtPlayerDowned++;
        if (amtPlayerDowned >= Players.Count)
        {
            GameObject deathCanvas = Instantiate(endScreenCanvas);
            Text canText = deathCanvas.GetComponentInChildren<Text>();
            canText.text = "You survived " + currentWave.ToString() + " waves before dying\n  Returning to Menu";
            StartCoroutine("endGame");
        }
        return;
    }
    IEnumerator endGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    void NewWave()
    {
       // Debug.Log("Calling New Wave");
        if (amtPlayerDowned < Players.Count)
        {
           // Debug.Log("Players are alive, continuing to next wave");
            currentWave++;
            int maxCount = Players.Count;
            int i = 0;
            while (i < maxCount)
            {
                if (Players[i] != null)
                {
                    m_Health hp = Players[i].GetComponent<m_Health>();
                    if (hp.amDed)
                    {
                        offlinePlayerMovement p_mov = Players[i].GetComponent<offlinePlayerMovement>();
                        p_mov.respawn();
                    }
                }
                i++;
            }
            waveText.text = "Wave: " + currentWave;
        }
    }
    
    void UpdatePossibleSpawns()
    {


        int x = 0;
        while (x < enemySpawnPoints.Length - 1)
        {



            bool available = Physics.CheckSphere(enemySpawnPoints[x].transform.position, radius, playerLayer);
            
            if (!available)
            {
                spawnAvailable[x] = true;
            }
            else if (available)
            {
                spawnAvailable[x] = false;
            }
            x++;
        }
    }

    void CmdSpawnEnemies(int waveNum)
    {
        if (ActiveinScene)
        {
            int x = 0;
            //Y will eventually choose what enemies to spawn, but for now it'll just spawn the small minions
            int y = 0;
            EnemyAmt = BaseEnemyCount * waveNum;
            while (x < EnemyAmt)
            {
                int randomVal = Random.Range(0, enemySpawnPoints.Length);
                if (spawnAvailable[randomVal] == true)
                {
                   // Debug.Log("Spawn Available, spawning enemy");
                    y = Random.Range(0, 3);
                    GameObject enemyToAdd = null;
                    MinionCtrl minion;
                    gunEnemyCtrl gunMinion;
                    explosiveMinionCtrl suicideMinion;
                    Vector3 randPos = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
                    if (y == 1)
                    {
                        enemyToAdd = Instantiate(suicideBomber, enemySpawnPoints[randomVal].transform.position + randPos, Quaternion.Euler(0, 0, 0));
                    }
                    else if (y == 2)
                    {
                        enemyToAdd = Instantiate(Gunner, enemySpawnPoints[randomVal].transform.position + randPos, Quaternion.Euler(0, 0, 0));
                    }
                   else
                    {
                        enemyToAdd = Instantiate(Minion, enemySpawnPoints[randomVal].transform.position + randPos, Quaternion.Euler(0, 0, 0));
                    }
                    if(enemyToAdd == null)
                    {
                        enemyToAdd = Instantiate(Minion, enemySpawnPoints[randomVal].transform.position + randPos, Quaternion.Euler(0, 0, 0));
                    }

                    enemySpawnPoints[randomVal].transform.Translate(Random.Range(-2, 2), 0, Random.Range(-2, 2));

                    currentEnemies.Add(enemyToAdd);
                    if (y == 0)
                    {
                        minion = enemyToAdd.GetComponent<MinionCtrl>();
                        minion.myofflineSpawner = this;

                    }
                    if (y == 2)
                    {
                        gunMinion = enemyToAdd.GetComponent<gunEnemyCtrl>();
                        gunMinion.myofflineSpawner = this;
                    }
                    if (y == 3)
                    {
                        suicideMinion = enemyToAdd.GetComponent<explosiveMinionCtrl>();
                        suicideMinion.myofflineSpawner = this;
                    }


                }
                x++;
            }
        }

    }
    public void removeEnemy(GameObject enemyToRemove)
    {
        Debug.Log("removing Enemy");
        currentEnemies.Remove(enemyToRemove);
        EnemyAmt--;


    
    }
    
}
