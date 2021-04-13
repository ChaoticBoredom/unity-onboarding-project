using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public GameObject towerPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void BuildTowerServerRpc(Vector3 position)
    {
        position.y = 1;
        var newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<NetworkObject>().Spawn();
    }
}
