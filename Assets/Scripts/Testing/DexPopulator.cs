using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DexPopulator : MonoBehaviour
{

    [SerializeField]
    Image bugImage;
    [SerializeField]
    TMP_Text bugName;
    [SerializeField]
    TMP_Text bugDescription;

    [SerializeField]
    int index;
    [SerializeField]
    BugClass bugclass;

    private void Start()
    {
        bugImage.sprite = BugBox.instance.getBugModel(index, true);
        bugName.text = BugBox.instance.GetBugName(index);
        bugDescription.text = BugBox.instance.GetBugDesc(index);
    }
}
