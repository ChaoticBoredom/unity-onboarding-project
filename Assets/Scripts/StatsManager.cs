using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public Text defenderText;
    public Text attackerText;

    public Text defenderGold;
    public Text defenderHP;
    public Text attackerGold;

    private GameManager m_GameManager;

    public void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        m_GameManager.defenderName.OnValueChanged += UpdateDefenderName;
        m_GameManager.attackerName.OnValueChanged += UpdateAttackerName;
    }

    public void FixedUpdate()
    {
        defenderGold.text = m_GameManager.defenderGold.Value.ToString();
        //defenderHP.text = GameManager.Singleton.defenderHP.Value.ToString();
        //attackerGold.text = GameManager.Singleton.attackerGold.Value.ToString();
    }

    public void UpdateDefenderName(string prevName, string newName)
    {
        defenderText.text = newName;
    }

    public void UpdateAttackerName(string prevName, string newName)
    {
        attackerText.text = newName;
    }
}
