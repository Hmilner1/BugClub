using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField]
    private GameObject loadCanvas;
    [SerializeField]
    private UnityEngine.UI.Image progressBar;

    private float target;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        await Task.Delay(10);
        loadCanvas.SetActive(false);
    }

    public async void LoadMainGame()
    {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync("DevelopmentScene");
        scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        }
        while (scene.progress < 0.9f);

        //await Task.Delay(1000);
        scene.allowSceneActivation = true;
        await Task.Delay(1400);
        loadCanvas.SetActive(false);

        PartyManager party = GameObject.Find("BugBoxManager").GetComponent<PartyManager>();
        party.enabled = true;

    }

    public async void LoadSceneAdditive(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        await Task.Delay(10);
        loadCanvas.SetActive(false);
    }

    public async void UnLoadScene(string sceneName)
    {
        target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.UnloadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        await Task.Delay(10);
        loadCanvas.SetActive(false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
