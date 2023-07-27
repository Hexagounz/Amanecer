using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    

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




