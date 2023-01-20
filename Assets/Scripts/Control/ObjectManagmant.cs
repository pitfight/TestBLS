using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagmant : MonoBehaviour
{
    [SerializeField] private GameObject background;

    [SerializeField] private Vector2 spawnPointPlayer;
    [SerializeField] private Vector2 spawnPointMap;
    [SerializeField] private Vector2[] spawnPointsAI;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject aIPrefab;
    [SerializeField] private GameObject mapPrefab;

    private IObjectContralable playerControlable;
    private IObjectContralable[] mapsControlables;
    private IObjectContralable[] aiControlables;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        
    }
}
