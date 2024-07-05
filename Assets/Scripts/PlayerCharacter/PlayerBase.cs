using System;
using UnityEngine;

[Serializable]
public class PlayerBase
{
    private Vector3 basePlayerLocation;
    public float x;
    public float y;
    public float z;

    public PlayerBase(float newx, float newy, float newz)
    {
        x= newx;
        y= newy;
        z= newz;
        basePlayerLocation = new Vector3(x,y,z);
    }
}
