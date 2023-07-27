using UnityEngine;

public class LightFix : MonoBehaviour
{
    Light lighting;
    [SerializeField] float Lintensity = 1.7f;
    private void Awake()
    {
        lighting = GetComponent<Light>();
    }

    private void Start()
    {
        lighting.intensity = Lintensity;
    }
}
