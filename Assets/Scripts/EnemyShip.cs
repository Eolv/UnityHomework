using SpaceGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour, ITakeDamage
{
    [SerializeField] private PlayerShip _player;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private Transform _laserSpawnPosition;
    [SerializeField] private float _lastFiringTime;
    [SerializeField] private int _enemyDistanceBehaviour;
    [SerializeField] private float _hp = 100;

    void Start()
    {
        _enemyDistanceBehaviour = Random.Range(50, 150);
        _player = FindObjectOfType<PlayerShip>();
    }

    private void Fire()
    {
        if (Time.time - _lastFiringTime > 0.4)
        {
            _lastFiringTime = Time.time;
            Instantiate(_laserPrefab, _laserSpawnPosition.position, _laserSpawnPosition.rotation);
        }
    }

    void FixedUpdate()
    {
        Vector3 DirectionToPlayer = _player.transform.position - transform.position;
        if (DirectionToPlayer.magnitude < 900)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(DirectionToPlayer), 180 * Time.fixedDeltaTime);
            Fire();

            if (DirectionToPlayer.magnitude  > _enemyDistanceBehaviour)
            transform.position += transform.forward * 50 * Time.fixedDeltaTime;
        }
        if (_hp <= 0)
        {           
            Destroy(gameObject);
            print("Вы уничтожили врага!");
        }

    }

    public void Hit(float damage)
    {
        _hp -= damage;        
    }
}
