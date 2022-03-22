using UnityEngine;

namespace SpaceGame
{
    public class EnemyFire : MonoBehaviour
    {
        private float _bulletDamage = 5;
        private float _bulletSpeed = 400;

        void Start()
        {
            Destroy(gameObject, 4f);
        }

        void FixedUpdate()
        {
            transform.position += transform.forward * _bulletSpeed * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))                
            {
                collision.gameObject.TryGetComponent(out ITakeDamage takeDamage);               
                takeDamage.Hit(_bulletDamage);
            }
        }
    }
}