using System;
using System.Collections;
using System.Linq;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchMaker : MonoBehaviour
{
    public string username;
    public GameObject defenderController;
    public GameObject attackerController;

    private Text m_searchingText;
    private Button m_searchButton;
    private bool m_searching;
    private GameManager m_GameManager;

    private const string URL_LOCATION = "https://26c3cb6d9270.ngrok.io";

    [Serializable]
    private class MatchData
    {
        public string id;
        public string server;
        public string attacker_id;
        public string attacker_username;
        public string defender_id;
        public string defender_username;
    }

    public void Start()
    {
        Text[] texts = GetComponentsInChildren<Text>(true);
        
        m_searchingText = texts.Single(val => val.name == "Searching");
        m_searching = false;
        m_searchButton = GetComponentInChildren<Button>(true);
        m_GameManager = FindObjectOfType<GameManager>();
    }
    
    public void SetUsername(string input)
    {
        username = input.ToLower();
    }

    public void FindMatch()
    {
        UnityWebRequest webReq = new UnityWebRequest(string.Format(URL_LOCATION + "/search/{0}", username));
        webReq.method = "PUT";
        webReq.SendWebRequest();
        m_searching = true;

        StartCoroutine(CheckMatchMaker());
    }

    public void UpdateButtonState(string input)
    {
        m_searchButton.interactable = input.Length > 0;
    }

    public void FixedUpdate()
    {
        if (!m_searching) return;
        
        int elipses = (int) Mathf.Repeat(Time.fixedTime, 4);
        m_searchingText.text = "Searching" + new string('.', elipses);
    }

    private IEnumerator CheckMatchMaker()
    {
        UnityWebRequest webReq = new UnityWebRequest(string.Format(URL_LOCATION + "/search/{0}", username))
        {
            downloadHandler = new DownloadHandlerBuffer()
        };
        yield return webReq.SendWebRequest();

        if (webReq.result == UnityWebRequest.Result.Success)
        {
            MatchData data = JsonUtility.FromJson<MatchData>(webReq.downloadHandler.text);
            m_searching = false;
            m_searchingText.text = $"Match Found!\n{data.attacker_username}\nvs.\n{data.defender_username}";

            var serverData = data.server.Split(':');
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = serverData.First();
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectPort = int.Parse(serverData.Last());
            NetworkManager.Singleton.StartClient();

            if (data.attacker_username == username)
            {
                m_GameManager.SetAttackerDataServerRpc(data.attacker_id, data.attacker_username, data.id);
                Instantiate(attackerController);
            }
            else
            {
                m_GameManager.SetDefenderDataServerRpc(data.defender_id, data.defender_username, data.id);
                Instantiate(defenderController);
            }

            gameObject.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(5);
        
        StartCoroutine(CheckMatchMaker());
    }
}

