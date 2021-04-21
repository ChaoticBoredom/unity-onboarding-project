using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public GameObject towerPrefab;

    private Spawner m_Spawner;

    public override void NetworkStart()
    {
        if (NetworkManager.IsServer)
        {
            defenderGold.Value = 200;
            defenderHP.Value = 200;
            attackerGold.Value = 5000;

            defenderHP.OnValueChanged += CheckDefenderGameEnd;
            attackerGold.OnValueChanged += CheckAttackerGameEnd;

            m_Spawner = FindObjectOfType<Spawner>();
        }
    }

    public NetworkVariableInt defenderGold = new NetworkVariableInt(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableInt defenderHP = new NetworkVariableInt(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableInt attackerGold = new NetworkVariableInt(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableString defenderName = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableString defenderId = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableString attackerName = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableString attackerId = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public NetworkVariableString matchId = new NetworkVariableString(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public void CheckDefenderGameEnd(int prevValue, int newValue)
    {
        if (newValue <= 0)
        {
            Debug.Log("GAME OVER!");
        }
    }

    public void CheckAttackerGameEnd(int prevValue, int newValue)
    {
        if (newValue <= 0)
        {
            Debug.Log("Game maybe over? Creeps in control");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void BuildTowerServerRpc(Vector3 position)
    {
        int towerCost = towerPrefab.GetComponent<Tower>().cost;
        if (defenderGold.Value < towerCost) return;

        position.y = 1;
        defenderGold.Value -= towerCost;
        var newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnCreepsServerRpc(int spawnCount = 1)
    {
        m_Spawner.SpawnCreeps(spawnCount);
    }

    [ServerRpc]
    public void SetAttackerDataServerRpc(string id, string newName, string matchID)
    {
        Debug.Log("Set Attacker Data");
        attackerId.Value = id;
        attackerName.Value = newName;
        matchId.Value = matchID;
    }

    [ServerRpc]
    public void SetDefenderDataServerRpc(string id, string newName, string matchID)
    {
        Debug.Log("Set Defender Data");
        defenderId.Value = id;
        defenderName.Value = newName;
        matchId.Value = matchID;
    }
}
