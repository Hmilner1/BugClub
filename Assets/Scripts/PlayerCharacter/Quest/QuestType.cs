using UnityEngine;

[System.Serializable]
public class QuestType
{
    public Type questType;

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
}

public enum Type
{ 
    Capture,
    Kill
}
