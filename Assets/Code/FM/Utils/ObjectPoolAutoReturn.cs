using UnityEngine;

public class ObjectPoolAutoReturn : MonoBehaviour
{
    [SerializeField] private float returnDelay = 10f;
    [SerializeField] private ObjectPoolSO objectPoolSO;

    private float timeElapsed;

    private void OnEnable()
    {
        timeElapsed = 0;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= returnDelay)
        {
            objectPoolSO.ReturnObject(gameObject);
        }
    }
}