using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
    public List<Bug> bugTeam;
    [SerializeField]
    private float visionDistance;
    [SerializeField]
    private Transform BattlePos;
    private Vector3 rayPos;
    private bool battled;


    private void Start()
    {
        rayPos= new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        battled = false;
        PopulateTeam();
    }

    private void PopulateTeam()
    {
        for (int i = 0; i < bugTeam.Count; i++)
        {
            bugTeam[i] = new Bug(bugTeam[i].baseBugIndex, bugTeam[i].lvl, bugTeam[i].bugClass, bugTeam[i].equippedItems);
            for (int j = 0; j < bugTeam[i].equippedItems.Length; j++)
            {
                bugTeam[i].equippedItems[j] = new BattleItem(bugTeam[i].equippedItems[j].itemIndex);
            }
        }


    }

    private void FixedUpdate()
    {
        if (battled) { return; }
        RaycastHit hit;

        if (Physics.Raycast(rayPos, transform.TransformDirection(Vector3.forward), out hit, visionDistance))
        {
            battled = true;
            MovePlayer(hit.transform.gameObject);
            BattleManager.instance.PopulateTrainerTeam(bugTeam);
            Debug.DrawRay(rayPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            EventManager.instance.BattleStart(false);
        }
        else
        {
            Debug.DrawRay(rayPos, transform.TransformDirection(Vector3.forward) * visionDistance, Color.white);
        }
    }

    private void MovePlayer(GameObject Player)
    { 
        GameObject playerToRotate = Player.GetComponentInChildren<PlayerController>().gameObject;
        Player.transform.position = new Vector3(BattlePos.position.x, Player.transform.position.y, BattlePos.position.z);
      
        Vector3 directionToTarget = gameObject.transform.position - Player.transform.position;
        directionToTarget.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        playerToRotate.transform.rotation = targetRotation;
    }
}
