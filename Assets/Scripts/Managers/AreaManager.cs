using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    public Vector3 currentArea;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetMapLocation()
    { 
        return currentArea;
    }

    public void UpdateMapLocation(Vector3 newLocation)
    {
        PlayerInfo.instance.SavePlayerLocation(newLocation);
    }
}
