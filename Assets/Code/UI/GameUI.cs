using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
    [HideInInspector] public bool IsWaitingForExit = false;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text label;
    [SerializeField] private Text enemiesLabel;
    [SerializeField] private float timeToDisplayLoseScreen = 0.3f;
    [SerializeField] private float timeToEndGame = 0.5f;

    private float timer = 0f;

    private void OnEnable()
    {
        gameManager.DragonManager.OnDragonSpawn += UpdateUIText;
        gameManager.DragonManager.OnDragonDeath += UpdateUIText;
    }

    private void OnDisable()
    {
        gameManager.DragonManager.OnDragonSpawn -= UpdateUIText;
        gameManager.DragonManager.OnDragonDeath -= UpdateUIText;
    }

    private void Update() 
    {    
        if (IsWaitingForExit)
        {
            timer += Time.deltaTime;
            if (timer >= timeToEndGame + timeToDisplayLoseScreen)
            {
                Stop();
            }
            else if (timer >= timeToDisplayLoseScreen)
            {
                label.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateUIText()
    {
        enemiesLabel.text = "Enemies: " + gameManager.DragonManager.AliveDragons + "\nKilled: " + gameManager.DragonManager.DeadDragons;
    }

    void Stop() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Level complete, stopping playmode\n");
    }
}