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
        m_GameManager.defenderGold.OnValueChanged += UpdateDefenderGold;
        m_GameManager.defenderHP.OnValueChanged += UpdateDefenderHP;
        m_GameManager.attackerGold.OnValueChanged += UpdateAttackerGold;
    }

    public void FixedUpdate()
    {
        attackerGold.text = m_GameManager.attackerGold.Value.ToString();
    }

    public void UpdateDefenderName(string prevName, string newName)
    {
        defenderText.text = newName;
    }

    public void UpdateAttackerName(string prevName, string newName)
    {
        attackerText.text = newName;
    }

    public void UpdateDefenderGold(int prevValue, int newValue)
    {
        defenderGold.text = newValue.ToString();
    }

    public void UpdateDefenderHP(int prevValue, int newValue)
    {
        defenderHP.text = newValue.ToString();
    }

    public void UpdateAttackerGold(int prevValue, int newValue)
    {
        attackerGold.text = newValue.ToString();
    }
}
