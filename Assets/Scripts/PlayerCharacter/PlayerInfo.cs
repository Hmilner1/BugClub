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

        loadData();
    }

    public async void loadData()
    {
        Player = new PlayerBase(0, 0, 0);
        CloudSaveManager.instance.LoadPlayerSave();
        await Task.Delay(1000);
        playerPos.position = new Vector3(Player.x, Player.y, Player.z);
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
