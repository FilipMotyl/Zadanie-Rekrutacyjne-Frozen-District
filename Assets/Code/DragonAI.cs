using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour {

	enum State {
		 Chasing,
		 Attacking,
		 Dead
	}

	[SerializeField] private int maxHitPoints = 30;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float timeToReturnToPool = 30f;

    [SerializeField] private List<Renderer> renderers = new List<Renderer>();
    [SerializeField] private DragonAnimator dragonAnimator;
	[SerializeField] private ObjectPoolSO dragonObjectPool;
    [SerializeField] private GameManager gameManager = null;

    private int currentHitPoints;
    private State state;
	private float timer = 0f;

    public delegate void DragonKilledEvent(DragonAI dragon);
    public event DragonKilledEvent OnDragonDeath;

    public void Init(GameManager gm)
    {
		this.gameManager = gm;
    }

    private void OnEnable() 
	{
        state = State.Chasing;
        currentHitPoints = maxHitPoints;
		timer = 0f;
	}

    private void Update ()
	{
		switch (state) 
		{
			case State.Chasing:
                Vector3 playerDirection = (gameManager.GetPlayerContainer().transform.position - transform.position);
                Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                dragonAnimator.PlayWalkAnimation();
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                if (Vector3.Distance(transform.position, gameManager.GetPlayerContainer().transform.position) < 10)
                {
                    state = State.Attacking;
                }
                break;
			case State.Attacking:
                dragonAnimator.PlayAttackAnimation();
                gameManager.StartEndGameSequence();
                break;
			case State.Dead:
				timer += Time.deltaTime;
				if (timer >= timeToReturnToPool)
				{
					Disappear();
                }
                break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void Die()
	{
		if (state != State.Dead)
		{
			state = State.Dead;
			OnDragonDeath?.Invoke(this);
			dragonAnimator.PlayDeadAnimation();
        }
    }
	
	private void Disappear()
	{
		if (dragonObjectPool)
		{
            dragonObjectPool.ReturnObject(this.gameObject);
        }
		else
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.tag == "Trap")
		{
            Die();
        }
	}

	public void TakeDamage(int damage) 
	{
		currentHitPoints -= damage;
		if (currentHitPoints <= 0) 
		{
            Die();
		}
	}
}
