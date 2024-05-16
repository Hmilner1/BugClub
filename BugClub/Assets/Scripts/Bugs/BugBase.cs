using UnityEngine;

[CreateAssetMenu(fileName = "Bug", menuName = "Bug/New Bug")]
public class BugBase : ScriptableObject
{
    [SerializeField]
    public string bugName;

    [SerializeField]
    public int hp;
    [SerializeField]
    public int attack;
    [SerializeField]
    public int spAttack;
    [SerializeField]
    public int defence;
    [SerializeField]
    public int spDefence;
    [SerializeField]
    public int speed;

    [SerializeField]
    public Sprite frontSprite;
    [SerializeField]
    public Sprite backSprite;
    [TextArea]
    [SerializeField]
    public string description;
}
