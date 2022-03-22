using UnityEngine;

namespace SpaceGame
{
    public class PlayerFire : MonoBehaviour
    {
        private float _bulletDamage = 10;
        private float _bulletSpeed = 1200;

        void Start()
        {
            Destroy(gameObject, 4f);
        }

        void FixedUpdate()
        {
            transform.position += transform.forward * _bulletSpeed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITakeDamage takeDamage))
            {
                takeDamage.Hit(_bulletDamage);
            }
        }
    }
}

