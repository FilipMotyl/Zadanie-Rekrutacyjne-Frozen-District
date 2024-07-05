using UnityEngine;

public class SawAnimation : MonoBehaviour 
{
    [SerializeField] private GameManager gameManager;

    private void Update() 
    {
        ChangeSawRotation();
    }

    private void ChangeSawRotation() 
    {
        Random.seed = (int)(gameManager.GetPlayerContainer().transform.position.x + gameManager.GetPlayerContainer().transform.position.y + gameManager.GetPlayerContainer().transform.position.z);
        gameObject.transform.Rotate(Random.value * CalculateBulletsShot(), 0, 0);        
    }

    private int CalculateBulletsShot()
    {
        return gameManager.GetPlayerContainer().Weapon.BulletsShot < 10 ? 10 : gameManager.GetPlayerContainer().Weapon.BulletsShot;
    }
}