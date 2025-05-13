using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class Square : MonoBehaviour
{
    private bool isDragging = false;
    private bool hasMerged = false;

    private float mergeDistance = 0.5f;

    private Collider gameObject;
    public GameObject squarePrefab, circlePrefab, trianglePrefab;

    public GameObject sq1, sq2;

    private void Awake()
    {
        squarePrefab = Resources.Load<GameObject>("Prefabs/Square");
        circlePrefab = Resources.Load<GameObject>("Prefabs/Circle");
        trianglePrefab = Resources.Load<GameObject>("Prefabs/Triangle");
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
        
         
        if ( sq1&&sq2 != null    &&  Vector3.Distance(sq1.transform.position, sq2.transform.position) <= GetMergeDistance(sq1,sq2))
        {
            Destroy(sq1);
            Destroy(sq2);
            Instantiate(trianglePrefab, sq2.transform.position, sq2.transform.rotation);
            return;
        }

        Debug.Log("OnMouseDrag");
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Debug.Log("OnMouseUp");
    }

    private static float GetMergeDistance(GameObject sq1, GameObject sq2)
    {
        var  sq1Bounds = sq1.GetComponent<Collider2D>().bounds;
        var sq2Bounds = sq2.GetComponent<Collider2D>().bounds;
        
        var avgDistance = (sq1Bounds.extents.magnitude + sq2Bounds.extents.magnitude) / 2;
        return avgDistance * 1.2f;
    }
}