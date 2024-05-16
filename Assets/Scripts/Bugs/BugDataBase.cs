using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BugDatabase", menuName = "Bug/New BugDatabase")]
public class BugDataBase : ScriptableObject
{
    [SerializeField]
    public List<BugBase> bugDataBase;
}
