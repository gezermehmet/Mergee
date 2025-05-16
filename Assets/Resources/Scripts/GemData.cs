using System;
using UnityEngine;

public class GemData : ScriptableObject
{

    public int gemID;
    public string gemName;
    public string gemColor;
    public Sprite gemSprite;
    public GameObject gemPrefab;


    private void Awake()
    {
        gemPrefab = Resources.Load<GameObject>("Prefabs/GemPrefab");
    }
}
