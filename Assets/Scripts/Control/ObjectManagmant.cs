using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectManagmant : MonoBehaviour
{
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

    private IObjectContralable playerControlable;
    private IObjectContralable[] mapsControlables;
    private IObjectContralable[] projectileControlables;
    private IObjectContralable[] aiControlables;

    private void Awake()
    {
        InitIObjects(ObjectPool.GetCreatePool(projectilePrefab, projectileCountSpawn), ref projectileControlables);
        InitIObjects(ObjectPool.GetCreatePool(mapPrefab, projectileCountSpawn), ref mapsControlables);
        InitIObjects(ObjectPool.GetCreatePool(aIPrefab, projectileCountSpawn), ref aiControlables);
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    private void InitIObjects(List<GameObject> list, ref IObjectContralable[] objectContralables)
    {
        objectContralables = new IObjectContralable[list.Count];
        for (int i = 0; i < list.Count; i++)
            objectContralables[i] = list[i].GetComponent<IObjectContralable>();
    }
}
