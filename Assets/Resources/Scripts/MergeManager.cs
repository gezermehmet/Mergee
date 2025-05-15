using System;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager  Instance;

    private void Awake()
    {
        Instance = this;
    }

    public MergeData Merge(MergeData a, MergeData b)
    {
        if (a == b) //&& a.nextLevelItem != null)
        {
            Debug.Log($"Merging: {a.itemName} + {b.itemName}");
            return a.nextLevelItem;
        }
        return null;
    }
}