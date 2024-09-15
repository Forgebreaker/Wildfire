using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Itemlist;
    [SerializeField] private float SpawnTime = 15f;
    private float CurrentSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSpawnTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector2(Random.Range(-14, 14), transform.position.y);
        CurrentSpawnTime -= Time.deltaTime;

        if (CurrentSpawnTime <= 0) 
        {
            CurrentSpawnTime = SpawnTime;
            Instantiate(Itemlist[Random.Range(0, 2)], transform.position, transform.rotation);
        }
    }
}
