using UnityEngine;

[CreateAssetMenu(fileName = "ItemData")]
public class item : ScriptableObject
{
    public int index;
    public string Name = "item";
    public Sprite Icon;
    public int Count = 1;
}
