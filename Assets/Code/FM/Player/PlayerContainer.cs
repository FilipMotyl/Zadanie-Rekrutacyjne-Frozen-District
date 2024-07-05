using ARFC;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] private FPController fpController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Weapon weapon;

    public FPController FPController => fpController;
    public PlayerController PlayerController => playerController;
    public Rigidbody RB => rb;
    public Camera MainCamera => mainCamera;
    public Weapon Weapon => weapon;
}
