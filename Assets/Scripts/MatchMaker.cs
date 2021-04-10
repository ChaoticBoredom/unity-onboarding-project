using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MLAPI;
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
    
    private const string URL_LOCATION = "http://26c3cb6d9270.ngrok.io";

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
        m_searchingText.text = "Searching" + new String('.', elipses);
    }

    private IEnumerator CheckMatchMaker()
    {
        UnityWebRequest webReq = new UnityWebRequest(string.Format(URL_LOCATION + "/search/{0}", username));
        webReq.downloadHandler = new DownloadHandlerBuffer();
        yield return webReq.SendWebRequest();

        if (webReq.result == UnityWebRequest.Result.Success)
        {
            MatchData data = JsonUtility.FromJson<MatchData>(webReq.downloadHandler.text);
            m_searching = false;
            m_searchingText.text = string.Format("Match Found!\n{0}\nvs.\n{1}", data.attacker_username, data.defender_username);
            NetworkManager.Singleton.StartClient();
            if (data.attacker_username == username)
            {
                Instantiate(attackerController);
            }
            else
            {
                Instantiate(defenderController);
            }
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(5);
        
        StartCoroutine(CheckMatchMaker());
    }
}

