using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSpawner : MonoBehaviour
{
    public static TempSpawner Instance = null;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private float range = 20;
    [SerializeField] private GameObject enemyPrefab;

    public void Spawn()
    {
        Instantiate(enemyPrefab, (Vector2)transform.position + Random.insideUnitCircle * range, transform.rotation);
    }
}
