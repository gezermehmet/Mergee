using UnityEngine;


[CreateAssetMenu(fileName = "New Merge Data", menuName = "Mergeable Object /Merge Data", order = 0)]
public class MergeData : ScriptableObject
{
    public int id;
    public string itemName;
    public GameObject prefab;
    public int damage;
    public int attackSpeed;
    public int itemPrice;

    public MergeData nextLevelItem;
}
