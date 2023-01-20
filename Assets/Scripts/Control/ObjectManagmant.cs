using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectManagmant : MonoBehaviour
{
    public bool startGame = false;
    [SerializeField] private GameObject background;

    [SerializeField] private Transform spawnPointPlayer;
    [SerializeField] private Transform spawnPointMap;
    [SerializeField] private Transform[] spawnPointsAI;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int projectileCountSpawn;
    [SerializeField] private GameObject aIPrefab;
    [SerializeField] private int aICountSpawn;
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private int mapCountSpawn;

    private IObjectContralable<Aircraft> playerControlable;
    private IObjectContralable<Map>[] mapsControlables;
    private IObjectContralable<Projectile>[] projectileControlables;
    private IObjectContralable<Aircraft>[] aiControlables;

    private void Awake()
    {
        playerControlable = Instantiate(playerPrefab, spawnPointPlayer.position, Quaternion.identity).GetComponent<IObjectContralable<Aircraft>>();

        InitIObjects(ObjectPool.GetCreatePool(projectilePrefab, projectileCountSpawn), ref projectileControlables);
        InitIObjects(ObjectPool.GetCreatePool(mapPrefab, mapCountSpawn), ref mapsControlables);
        InitIObjects(ObjectPool.GetCreatePool(aIPrefab, aICountSpawn), ref aiControlables);

        //foreach (var s in mapsControlables)
        //    Debug.Log(s == null);
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (startGame)
        {
            float input = Input.GetAxis("Vertical");
            if (input != 0)
            {
                if (input > 0)
                    playerControlable.Move(Vector2.up);
                if (input < 0)
                    playerControlable.Move(Vector2.down);
            }
            MoveObjects(mapsControlables, Vector2.left);
            MoveObjects(projectileControlables, Vector2.right);
            MoveObjects(aiControlables, Vector2.left);
        }
    }

    private void InitIObjects<T>(List<GameObject> list, ref IObjectContralable<T>[] objectContralables)
    {
        objectContralables = new IObjectContralable<T>[list.Count];
        for (int i = 0; i < list.Count; i++)
            objectContralables[i] = list[i].GetComponent<IObjectContralable<T>>();
    }

    private void MoveObjects<T>(IObjectContralable<T>[] objects, Vector2 direction)
    {
        foreach (var obj in objects)
            obj.Move(direction);
    }
}
