using UnityEngine;
using UnityEngine.UI;

public class BoxSlotContoller : MonoBehaviour
{
    private int bugIndex;
    private int listIndex;
    [SerializeField]
    private Image bugImage;

    public void UpdateImage()
    {
        bugImage.sprite = BugBox.instance.getBugModel(bugIndex, true);
    }

    public void SetIndex(int index)
    {
        bugIndex = index;
    }

    public void SetListIndex(int index)
    {
        listIndex = index;
    }


    public void OnBugClicked()
    {
        BugBoxUIManager.instance.BugSelected(listIndex);
    }
}
