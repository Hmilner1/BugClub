using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrivateMatchUIManager : MonoBehaviour
{ 
    [SerializeField]
    private GameObject buttonPanel;
    [SerializeField]
    private GameObject hostPanel;
    [SerializeField]
    private GameObject clientPanel;
    //[SerializeField]
    //private TextMeshProUGUI hostJoinCode;
    [SerializeField]
    private TMP_InputField clientJoinCode;
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private Canvas matchmakingcanvas;

    private PrivateMatchMaker matchMaker;

    private void OnEnable()
    {
        PrivateMatchMaker.OnPrivateLobbyHost += DisplayJoinCode;
    }

    private void OnDisable()
    {
        PrivateMatchMaker.OnPrivateLobbyHost -= DisplayJoinCode;

    }

    private void Awake()
    {
        matchMaker = GameObject.FindAnyObjectByType<PrivateMatchMaker>();
    }

 
    public void OnClickClientPanel()
    {
        DisableMainButtonPanel();
        clientPanel.SetActive(true);
    }

    public void OnJoinCodeChanaged()
    {
        string code = clientJoinCode.text.ToString();
        matchMaker.SetClientJoinCode(code);
    }

    private void DisableMainButtonPanel()
    { 
        buttonPanel.SetActive(false);
    }

    private void DisplayJoinCode(string JoinCode)
    {
        TMP_Text text = GameObject.FindGameObjectWithTag("JoinCode").GetComponent<TMP_Text>();
        text.text = "JOIN CODE: " + JoinCode;
        DisableUI();
    }

    private void DisableUI()
    {
        SceneController.Instance.UnLoadScene("Multiplayer Lobby");
    }
}
