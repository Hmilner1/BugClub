using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField]
    private BugDataBase bugDataBase;

    private void OnEnable()
    {
        EventManager.instance.OnBattle.AddListener(BattleStarted);
    }

    private void OnDisable()
    {
        EventManager.instance.OnBattle.RemoveListener(BattleStarted);
    }

    private void BattleStarted(bool wild)
    {
        if (!wild) { return; }
        BugBox.instance.ChangeCurrentWildBug(GenerateBug());
    }

    private Bug GenerateBug()
    {
        Bug GeneratedBug = new Bug(GenererateBugIndex(), GenerateBugLvl(), GenerateBugClass());
        return GeneratedBug;
    }

    private int GenererateBugIndex()
    {
        int index = Random.Range(0, bugDataBase.bugDataBase.Count);
        return index;
    }

    public int GenerateBugLvl()
    {
        int lvl = Random.Range(1, 10);
        return lvl;
    }

    private BugClass GenerateBugClass()
    {
        int bugClass = Random.Range(1,4);

        switch (bugClass)
        {
            case 1:
                return BugClass.Tank;
            case 2:
                return BugClass.Support;

            case 3:
                return BugClass.DPS;
        }

        return BugClass.Tank;
    }


}
