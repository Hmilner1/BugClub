using System.Threading.Tasks;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance { get; private set; }
    public PlayerBase Player;
    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private Vector3 MainSpawn = new Vector3(-32, 1, 36);

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        loadData(true);
    }

    public async void loadData(bool start)
    {
        if (Player == null)
        {
            Player = new PlayerBase(MainSpawn.x, MainSpawn.y, MainSpawn.z, 0);
        }
        CloudSaveManager.instance.LoadPlayerSave();
        if (start)
        {
            await Task.Delay(600);
            SetPosition();
        }
    }

    public void SetPosition()
    {
        playerPos.position = new Vector3(Player.x, Player.y, Player.z);
    }

    public int ItemAmount()
    {
        loadData(false);
        return Player.itemAmount;
    }
    public void SaveItemAmount(int amount)
    {
        Player.itemAmount = amount;
        CloudSaveManager.instance.SavePlayerData(Player);
    }

    public void DefaultLocation()
    {
        Player.x = -32;
        Player.z = 36;
    }

    public void SavePlayerLocation(Vector3 locationNum)
    {
        Player.x = locationNum.x;
        Player.z = locationNum.z;
        Player.y = locationNum.y;

        CloudSaveManager.instance.SavePlayerData(Player);
    }
}
