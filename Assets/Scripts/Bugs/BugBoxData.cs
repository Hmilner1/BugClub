using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BugBoxData
{
    public List<Bug> PlaysOwnedBugs;

    public BugBoxData(List<Bug> OwnedBugs)
    {
        PlaysOwnedBugs = OwnedBugs;
    }
}
