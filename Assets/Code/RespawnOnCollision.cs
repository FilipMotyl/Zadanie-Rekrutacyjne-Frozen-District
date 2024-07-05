using UnityEngine;

public class RespawnOnCollision : MonoBehaviour {
    [SerializeField] private GameManager gameManager;

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.ResetPlayerPosition();
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.ResetPlayerPosition();
        }
    }
}
