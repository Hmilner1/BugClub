using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bug", menuName = "Bug/New Bug")]
public class BugBase : ScriptableObject
{
    public string bugName;

    public int hp;
    public int attack;
    public int defence;
    public int speed;

    
    public Sprite frontSprite;
    public Sprite backSprite;

    [TextArea]
    public string description;

}
