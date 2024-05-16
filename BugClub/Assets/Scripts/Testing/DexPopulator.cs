using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DexPopulator : MonoBehaviour
{

    [SerializeField]
    Image bugImage;
    [SerializeField]
    TMP_Text bugName;
    [SerializeField]
    TMP_Text bugDescription;
    //[SerializeField]
    //TMP_Text bugLVL;
    //[SerializeField]
    //TMP_Text bugClass;
    //[SerializeField]
    //TMP_Text hp;
    //[SerializeField]
    //TMP_Text attack;
    //[SerializeField]
    //TMP_Text spAttack;
    //[SerializeField]
    //TMP_Text defence;
    //[SerializeField]
    //TMP_Text spDefence;
    //[SerializeField]
    //TMP_Text speed;

    [SerializeField]
    int index;
    [SerializeField]
    int lvl;
    [SerializeField]
    BugClass bugclass;
    [SerializeField]
    BugDataBase bugData;

    public Bug bug { get; set; }

    private void Start()
    {
        bug = new Bug(index, lvl, bugclass, bugData);

        bugImage.sprite = bug.dataBase.bugDataBase[index].frontSprite;
        bugName.text = bugName.text + bug.dataBase.bugDataBase[index].name;
        bugDescription.text = bug.dataBase.bugDataBase[index].description;
        //bugLVL.text = bugLVL.text + bug.lvl.ToString();
        //bugClass.text = bugClass.text + bug.bugClass.ToString();
        //hp.text = bug.HP.ToString();
        //attack.text = bug.Attack.ToString();
        //spAttack.text = bug.SpAttack.ToString();
        //defence.text = bug.Defence.ToString();
        //spDefence.text = bug.SpDefence.ToString();
        //speed.text = bug.Speed.ToString();

    }
}
