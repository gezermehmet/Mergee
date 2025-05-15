using System;
using Unity.VisualScripting;
using UnityEngine;

public class Merge : MonoBehaviour
{
    [SerializeField] private MergeData mergeData;
    private bool isDragging;
    private Vector3 offset;
    private float mergeDistance = 0.5f;


    private void Awake()
    {
    }

    private void TryMerge()
    {
        var allObjects = FindObjectsOfType<MergeableObject>();

        foreach (var other in allObjects)
        {
            if (other.gameObject == gameObject) continue;

            float distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance <= mergeDistance)
            {
                MergeData(gameObject, other.gameObject);
                break;
            }
        }
    }


    private void MergeData(GameObject a, GameObject b)
    {
        var dataA = a.GetComponent<MergeableObject>().mergeData;
        var dataB = b.GetComponent<MergeableObject>().mergeData;

        if (dataA == dataB && dataA.nextLevelItem != null)
        {
            var newData = dataA.nextLevelItem;

            Instantiate(newData.prefab, a.transform.position, a.transform.rotation);
            Destroy(a);
            Destroy(b);
        }
        else if (dataB == dataA && dataB.nextLevelItem == null)
        {
            Instantiate(dataA.prefab, a.transform.position, a.transform.rotation);
        }
    }


    #region MOUSE DRAGGING

    // =============================================================
    // ==================   MOUSE DRAGGING LOGIC   =================
    // =============================================================
    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        offset = transform.position - mouseWorld;
        // I used offset because when I click the object with mouse, object relocate to mouse cursor.
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        TryMerge();
    }

    #endregion
}