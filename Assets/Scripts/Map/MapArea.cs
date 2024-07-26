using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation;

    private Vector3 spawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnPos = spawnLocation.position;
            AreaManager.instance.UpdateMapLocation(spawnPos);
            if (gameObject.name == "City")
            {
                EventManager.instance.EnterCity();
            }

            if (gameObject.name == "dungeon")
            {
                EventManager.instance.EnterDungeon();
            }
        }
    }
}
