using UnityEngine;

public class Fire : MonoBehaviour
{    
    private float _bulletSpeed = 1200;
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * _bulletSpeed * Time.fixedDeltaTime;
    }
}
