using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{
    private bool isDragging = false;
    private bool hasMerged = false;

    private float mergeDistance = 0.5f;

    private Collider gameObject;
    public GameObject squarePrefab, circlePrefab, trianglePrefab;

    public GameObject sq1, sq2;
    public MergeData mdsq1, mdsq2;

    private void Awake()
    {
        squarePrefab = Resources.Load<GameObject>("Prefabs/Square");
        circlePrefab = Resources.Load<GameObject>("Prefabs/Circle");
        trianglePrefab = Resources.Load<GameObject>("Prefabs/Triangle");
    }

    private void Update()
    {
    }

    public class MergeableItem : MonoBehaviour
    {
        public MergeData data;
        public Sprite color;

        public void SetData(MergeData newData)
        {
            data = newData;

            GetComponent<SpriteRenderer>().sprite = data.icon;
            // GetComponent<Weapon>().UpdateStats(data.damage, data.attackSpeed);
        }
    }


    public MergeData Merge(MergeData a, MergeData b)
    {
        if (a == b)
        {
            return a.nextLevelItem;
        }

        return null;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Debug.Log("OnMouseDown");
    }


    private void OnMouseDrag()
    {
        isDragging = true;


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;

        MergeData dataA = sq1.GetComponent<MergeableObject>().mergeData;
        MergeData dataB = sq2.GetComponent<MergeableObject>().mergeData;
        MergeData result = MergeManager.Instance.Merge(dataA, dataB);

        if (sq1 && sq2 != null && Vector3.Distance(sq1.transform.position, sq2.transform.position) <=
            GetMergeDistance(sq1, sq2))
        {
            
            Destroy(sq1);
            Destroy(sq2);
            Instantiate(result.prefab);
            return;
        }


        Debug.Log("OnMouseDrag");
    }
    
    public void MergeAndSpawn(MergeData a, MergeData b, Vector3 spawnPosition)
    {
        MergeData result = MergeManager.Instance.Merge(a, b);
        if (result != null && result.prefab != null)
        {
            Instantiate(result.prefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Merge sonucu veya prefab null!");
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Debug.Log("OnMouseUp");
    }

    private static float GetMergeDistance(GameObject go1, GameObject go2)
    {
        var sq1Bounds = go1.GetComponent<Collider2D>().bounds;
        var sq2Bounds = go2.GetComponent<Collider2D>().bounds;

        var avgDistance = (sq1Bounds.extents.magnitude + sq2Bounds.extents.magnitude) / 2;
        return avgDistance * 1.2f;
    }
}