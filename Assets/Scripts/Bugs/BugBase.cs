using UnityEngine;

[CreateAssetMenu(fileName = "Bug", menuName = "Bug/New Bug")]
public class BugBase : ScriptableObject
{
    
    public string bugName;

    public int hp;
    public int attack;
    public int spAttack;
    public int defence;
    public int spDefence;
    public int speed;

    
    public Sprite frontSprite;
    public Sprite backSprite;

    [TextArea]
    public string description;
}
