using UnityEngine;

public class PlayerShootingController : MonoBehaviour {
    [SerializeField] private Weapon weapon;

    void Update() 
    {
        if (Input.GetMouseButton(0))
        {
            weapon.HandleGunInput();
        }
    }
}