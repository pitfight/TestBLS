using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagmant : MonoBehaviour
{
    private float currTimeRemaining = 20f;
    private float currTimeBtwFire = 0f;
    private float currTimeBtwSpawn = 0f;

    private const float TIME_MATCH = 20f;
    private const float TIME_BTW_FIRE = 0.3f;
    private const float TIME_BTW_SPAWN_AI = 4f;

    private bool canfire = true;
    private bool startGame = false;
    private bool inMenu = true;

    private int currScore = 0;
    private const int GET_SCORE = 100;
    private const string SCORE = "Score";

    [SerializeField] private DisplayManager displayManager;

    [SerializeField] private Transform spawnPointPlayer;
    [SerializeField] private Transform spawnPointProjectile;
    [SerializeField] private Transform spawnPointMap;
    [SerializeField] private Transform[] spawnPointsAI;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileCountSpawn;
    [SerializeField] private GameObject aIPrefab;
    [SerializeField] private int aICountSpawn;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int mapCountSpawn;

    private IObjectContralable<Player, float> playerControlable;
    private IObjectContralable<Map, Action<GameObject>>[] mapsControlables;
    private IObjectContralable<Projectile, Action<GameObject>>[] projectileControlables;
    private IObjectContralable<Aircraft, Action<GameObject>>[] aiControlables;

    private void Awake()
    {
        // Spawn Object Pool
        InitIObjects(ObjectPool.GetCreatePool(projectilePrefab, projectileCountSpawn), ref projectileControlables);
        InitIObjects(ObjectPool.GetCreatePool(mapPrefab, mapCountSpawn), ref mapsControlables);
        InitIObjects(ObjectPool.GetCreatePool(aIPrefab, aICountSpawn), ref aiControlables);

        // Spawn Map
        ObjectPool.Spawn(mapPrefab, Vector2.zero);
        ObjectPool.Spawn(mapPrefab, spawnPointMap.transform.position);

        displayManager.ShowUiGame(false);
        displayManager.UpdateScoreInMenu(PlayerPrefs.GetInt(SCORE, currScore));

        foreach (var pp in projectileControlables)
        {
            pp.GetValue().OnHit += () =>
            {
                currScore += GET_SCORE;
                displayManager.UpdateScore(currScore);
            };
        }
    }

    private void FixedUpdate()
    {
        if (startGame)
        {
            float input = Input.GetAxis("Vertical");
            if (input != 0)
            {

                if (input > 0)
                    playerControlable.Move(Vector2.up, input);
                if (input < 0)
                    playerControlable.Move(Vector2.down, input);
            }

            MoveObjects(projectileControlables, Vector2.right, (g) => ObjectPool.Recycle(g));
            MoveObjects(aiControlables, Vector2.left, (g) => ObjectPool.Recycle(g));
        }

        MoveObjects(mapsControlables, Vector2.left, (g) => g.transform.position = spawnPointMap.position);
    }

    void Update()
    {
        if (startGame)
        {
            if (currTimeRemaining > 0)
            {
                currTimeRemaining -= Time.deltaTime;
                displayManager.UpdateTime(currTimeRemaining);
            }
            else
            {
                currTimeRemaining = 0;
                displayManager.UpdateTime(currTimeRemaining);
                EndGame();
                startGame = false;
            }

            if (currTimeBtwFire > 0)
            {
                currTimeBtwFire -= Time.deltaTime;
            }
            else
            {
                if (canfire == false) canfire = true;
            }

            if (currTimeBtwSpawn > 0)
            {
                currTimeBtwSpawn -= Time.deltaTime; 
            }
            else
            {
                StartCoroutine(nameof(SpawnAI));
            }

            if (canfire && Input.GetKeyDown(KeyCode.Space))
            {
                ObjectPool.Spawn(projectilePrefab, spawnPointProjectile.position, Quaternion.identity);
                canfire = false;
                currTimeBtwFire = TIME_BTW_FIRE;
            }
        }

        if (inMenu)
        {
            if (Input.anyKey)
                StartGame();
        }
    }

    private IEnumerator SpawnAI()
    {
        currTimeBtwSpawn = TIME_BTW_SPAWN_AI;
        var waitTime = new WaitForSeconds(0.23f);
        int countSpawn = UnityEngine.Random.Range(3, 6);
        do
        {
            countSpawn--;
            ObjectPool.Spawn(aIPrefab, spawnPointsAI[UnityEngine.Random.Range(0, spawnPointsAI.Length)].position, Quaternion.identity);
            yield return waitTime;
        } while (countSpawn > 0);
    }

    private void StartGame()
    {
        var player = Instantiate(playerPrefab, spawnPointPlayer.position, Quaternion.identity);

        spawnPointProjectile = player.transform.Find("FirePoint").transform;
        playerControlable = player.GetComponent<IObjectContralable<Player, float>>();

        displayManager.ShowUiGame(true);
        displayManager.UpdateScore(currScore);
        displayManager.UpdateTime(currTimeRemaining);
        displayManager.UpdateLive(playerControlable.GetValue().GetLives());

        playerControlable.GetValue().OnHit += () =>
        {
            displayManager.UpdateLive(playerControlable.GetValue().GetLives());
        };

        playerControlable.GetValue().OnDead += () =>
        {
            EndGame();
        };

        inMenu = false;
        startGame = true;
    }

    private void EndGame()
    {
        if (startGame) startGame = false;
        inMenu = true;

        var maxScore = PlayerPrefs.GetInt(SCORE);

        displayManager.ShowUiGame(false);
        displayManager.UpdateScoreInMenu(currScore > maxScore ? currScore : maxScore);
        // Save
        PlayerPrefs.SetInt(SCORE, currScore > maxScore ? currScore : maxScore);
        // Resets
        currScore = 0;
        currTimeRemaining = TIME_MATCH;

        ObjectPool.RecycleAll(aIPrefab);
        ObjectPool.RecycleAll(projectilePrefab);
    }

    private void InitIObjects<T1, T2>(List<GameObject> list, ref IObjectContralable<T1, T2>[] objectContralables)
    {
        objectContralables = new IObjectContralable<T1, T2 >[list.Count];
        for (int i = 0; i < list.Count; i++)
            objectContralables[i] = list[i].GetComponent<IObjectContralable<T1, T2>>();
    }

    private void MoveObjects<T1, T2>(IObjectContralable<T1, T2>[] objects, Vector2 direction, T2 callBack)
    {
        foreach (var obj in objects)
            obj.Move(direction, callBack);
    }
}
