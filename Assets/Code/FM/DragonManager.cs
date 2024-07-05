using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour 
{
    [SerializeField] private float timeToActivateSpawner = 10f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ObjectPoolSO dragonObjectPool;
    [SerializeField] private List<DragonAI> dragonAspects;
    [SerializeField] private List<Transform> spawnPoints;

    public delegate void DragonEvent();
    public event DragonEvent OnDragonSpawn;
    public event DragonEvent OnDragonDeath;

    private int deadDragons = 0;
    private int aliveDragons = 0;
    private float timer = 0f;
    private bool isActive = false;
    private float timeToSpawnNextDragon = 0f;
    private int SpawnerIndex = 0;

    public int DeadDragons => deadDragons;
    public int AliveDragons => aliveDragons;

    private void Start()
    {
        deadDragons = 0;
        aliveDragons = dragonAspects.Count;
        OnDragonSpawn?.Invoke();
        timer = 0f;

        foreach (DragonAI dragon in dragonAspects)
        {
            dragon.OnDragonDeath += DragonKilled;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (isActive)
        {
            if (timer >= timeToSpawnNextDragon)
            {
                timer = 0f;
                SpawnDragon();

                timeToSpawnNextDragon = Mathf.Max(3 + aliveDragons * 0.1f - deadDragons * 0.1f, 1);
            }
        }
        else
        {
            if (timer > timeToActivateSpawner)
            {
                isActive = true;
            }
        }
    }

    void SpawnDragon() 
    {
        DragonAI dragon = dragonObjectPool.GetObjectAtPosition(spawnPoints[SpawnerIndex].position).GetComponent<DragonAI>();
        dragon.Init(gameManager);

        dragon.OnDragonDeath += DragonKilled;
        SpawnerIndex++;
        aliveDragons++;
        gameManager.UpdateUIText();
        if (SpawnerIndex >= spawnPoints.Count)
        {
            SpawnerIndex = 0;
        }
    }

    public void DragonKilled(DragonAI dragon)
    {
        aliveDragons--;
        deadDragons++;
        OnDragonDeath?.Invoke();
        dragon.OnDragonDeath -= DragonKilled;
    }
}