using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField] private float objectSpawnTime = 3;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float spawnerSpawnInterval = 10f;
    private float timer;
    private float realTimeTimer;

    private MergeData mergeData;
    private Transform circle;


    private void Awake()
    {
        circle = GetComponent<Transform>();
        mergeData = GetComponent<MergeableObject>().mergeData;
        objectToSpawn = (GameObject)Resources.Load("Prefabs/Spawner");

        if (objectToSpawn == null)
            Debug.LogError("Prefab yüklenemedi! Yol ve dosya adını kontrol et.");
    }

    private void Start()
    {
        StartCoroutine(RepeatEveryTenSeconds());
    }

    void Update()
    {
        realTimeTimer += Time.deltaTime;
        timer += Time.deltaTime;

        RandomSpawnerPosition();

        if (timer >= objectSpawnTime && mergeData.id == 1)
        {
            Instantiate(mergeData.prefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }

    private void RandomSpawnerPosition()
    {
        float randomX = UnityEngine.Random.Range(-2f, 2f);
        float randomY = UnityEngine.Random.Range(-4.5f, 4.5f);

        circle.position = new Vector3(randomX, randomY, 0f);
    }

    private void CreateSpawner()
    {
        if (objectToSpawn == null)
        {
            Debug.Log("Spawner Prefabi Null"); return;
        }

        if (realTimeTimer >= spawnerSpawnInterval && gameObject.tag == "MainSpawner")
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }

        realTimeTimer = 0;
    }


    IEnumerator RepeatEveryTenSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnerSpawnInterval);
            CreateSpawner();
        }
    }
}