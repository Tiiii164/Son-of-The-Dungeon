using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _initialPosition;

    [SerializeField]
    private GameObject _messagePrefab;

    public void SpawnMessage(string msg)
    {
        var msgObj = Instantiate(_messagePrefab, GetSpawnPosition(), Quaternion.identity);
        var inGameMessage = msgObj.GetComponent<IInGameMessage>();
        inGameMessage.SetMessage(msg);
    }

    private Vector3 GetSpawnPosition()
    {
        return transform.position + (Vector3)_initialPosition;
    }

}
