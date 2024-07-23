using UnityEngine;

public class JellyItem : MonoBehaviour
{
    public void OnitemClicked()
    {
        ItemController.instance.OnItemClick();
    }
}
