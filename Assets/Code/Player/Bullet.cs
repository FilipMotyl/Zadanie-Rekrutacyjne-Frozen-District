using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private int damage = 10;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody rb;

    private readonly Color[] colors = {Color.yellow, Color.red, Color.white, Color.blue, Color.green};
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    private void OnEnable()
    {
        material.color = colors[Random.Range(0, colors.Length)];
        audioSource.Play();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision other) 
    {
        rb.useGravity = true;
        if (other.collider.tag == "Enemy")
        {
            if (other.collider.TryGetComponent<DragonAI>(out DragonAI dragon))
            {
                dragon.TakeDamage(damage);
            }
        }
    }
}