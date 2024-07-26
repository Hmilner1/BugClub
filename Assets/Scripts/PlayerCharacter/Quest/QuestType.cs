using UnityEngine;

[System.Serializable]
public class QuestType
{
    public Type questType;
    public Zone zoneType;


    public int amountNeeded;
    public int currentAmount;

    public bool reached()
    {
        return (currentAmount >= amountNeeded);
    }

    public void Killed()
    {
        if (questType == Type.Kill)
        {
            currentAmount++;
        }
    }

    public void Captured()
    {
        if (questType == Type.Capture)
        {
            currentAmount++;
        }
    }

    public void GoToCity()
    {
        if (questType == Type.GoTo)
        {
            if (zoneType == Zone.City)
            {
                currentAmount++;
            }
        }
    }

    public void GoToDungeon()
    {
        if (questType == Type.GoTo)
        {
            if (zoneType == Zone.Dungeon)
            {
                currentAmount++;
            }
        }
    }

}

public enum Type
{ 
    Capture,
    Kill,
    GoTo
}

public enum Zone
{
    NON,
    City,
    Dungeon
}

