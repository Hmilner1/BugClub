using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class OnlineBattleUI : NetworkBehaviour
{
    [SerializeField]
    private SpriteRenderer playerBugSprite;
    [SerializeField]
    private SpriteRenderer enemyBugSprite;


    private void Awake()
    {
        if (IsOwner)
        {
            Destroy(playerBugSprite);
            Destroy(enemyBugSprite);
        }
    }

    private void Start()
    {
        //gameObject.SetActive(false);
    }

}
