using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void OnClickButton(int index)
    {
        AudioMan.instance.PlaySfx(index);
    }
}
