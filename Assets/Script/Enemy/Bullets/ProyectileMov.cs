using UnityEngine;

public class ProyectileMov : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        Destroy(gameObject, 4f);
    }
}
