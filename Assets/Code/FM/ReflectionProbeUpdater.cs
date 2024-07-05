using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ReflectionProbeUpdater : MonoBehaviour
{
    [SerializeField] private float updateCooldown = 1f;
    [SerializeField] private ReflectionProbe reflectionProbe;

    private float timer = 0f;
    void Start()
    {
        UpdateReflectionProbe();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > updateCooldown)
        {
            timer = 0f;
            UpdateReflectionProbe();
        }
    }

    public void UpdateReflectionProbe()
    {
        reflectionProbe.RequestRenderNextUpdate();
    }
}