using System;
using UnityEngine;

[Serializable]
public class BattleItem 
{
    private BattleItemDataBase dataBase = Resources.Load<BattleItemDataBase>("Items/DataBase/BattleItemDatabase");

    public int itemIndex;

    public BattleItem(int indexNumber)
    { 
        itemIndex = indexNumber;
    }
    
 

}
