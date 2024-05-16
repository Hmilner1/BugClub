using UnityEngine;

public class Bug
{
    public BugDataBase dataBase;
    public int baseBugIndex;
    public int lvl;
    public BugClass bugClass;

    public  Bug(int bugIndex, int currentLvl, BugClass Class, BugDataBase data)
    {
        dataBase = data;
        baseBugIndex = bugIndex;
        lvl = currentLvl;
        bugClass = Class;
    }

    public int HP
    { 
        get {return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].hp * lvl) / 100f) + 10;}
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].attack * lvl) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].spAttack * lvl) / 100f) + 5; }
    }

    public int Defence
    {
        get { return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].defence * lvl) / 100f) + 5; }
    }

    public int SpDefence
    {
        get { return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].spDefence * lvl) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((dataBase.bugDataBase[baseBugIndex].speed * lvl) / 100f) + 5; }
    }
}


public enum BugClass
{ 
    Tank,
    Support,
    DPS
}
