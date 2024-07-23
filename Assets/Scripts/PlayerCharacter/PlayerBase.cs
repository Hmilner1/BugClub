using System;
using UnityEngine;

[Serializable]
public class PlayerBase
{
    private Vector3 basePlayerLocation;
    public float x;
    public float y;
    public float z;

    public int itemAmount;

    public PlayerBase(float newx, float newy, float newz, int iAmount)
    {
        basePlayerLocation = new Vector3(x, y, z);
        x = newx;
        y = newy;
        z = newz;
        itemAmount = iAmount;
    }
}
