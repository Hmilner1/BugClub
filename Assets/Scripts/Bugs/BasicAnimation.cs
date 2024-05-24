using UnityEngine;

public class BasicAnimation : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;

    // Desired scale sizes
    public Vector3 origonalPos = new Vector3(.3f, .3f, .3f);
    public Vector3 bobOffset = new Vector3(.33f, .33f, .33f);

    // Duration for scaling up and down
    public float duration = .5f;

    private void Start()
    {
        Animate(player);
        Animate(enemy);
    }

    private void Animate(GameObject target)
    {
        LeanTween.scale(target, origonalPos+ bobOffset, duration).setEaseInOutSine().setLoopPingPong();
    }
}
