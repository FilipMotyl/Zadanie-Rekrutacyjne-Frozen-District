using Moe.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DragonManager DragonManager => dragonManager;

    [SerializeField] private List<Transform> objectPoolParentList = new List<Transform>();
    [SerializeField] private List<ObjectPoolSO> objectPoolSOList = new List<ObjectPoolSO>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerContainer playerPrefab;
    [SerializeField] private DragonManager dragonManager;
    [SerializeField] private GameUI gameUI;

    private PlayerContainer playerContainer = null;

    private void Awake()
    {
        for (int i = 0; i < objectPoolSOList.Count; i++)
        {
            if (objectPoolSOList[i] != null && objectPoolParentList[i])
            {
                objectPoolSOList[i].InitializePool(objectPoolParentList[i]);
            }
        }
        playerContainer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public PlayerContainer GetPlayerContainer()
    {
        if (playerContainer == null)
        {
            Debug.LogError("No player reference");
            return null;
        }
        return playerContainer;
    }

    public void ResetPlayerPosition()
    {
        if (playerContainer == null)
        {
            Debug.LogError("No spawn point set");
            return;
        }
        playerContainer.transform.position = spawnPoint.position;
        playerContainer.RB.velocity = Vector3.zero;
    }

    public void StartEndGameSequence()
    {
        gameUI.IsWaitingForExit = true;
    }

    public void UpdateUIText()
    {
        gameUI.UpdateUIText();
    }
}