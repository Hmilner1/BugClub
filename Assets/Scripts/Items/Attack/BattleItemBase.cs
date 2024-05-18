using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/New Battle Item")]
public class BattleItemBase : ScriptableObject
{
    public string itemName;

    [TextArea]
    public string itemDesc;

    public int itemDamage;

    public Sprite itemSprite;

    public BugClass itemClass;
}
