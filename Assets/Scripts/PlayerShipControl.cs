using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace spacegame
{
    public class PlayerShipControl : MonoBehaviour
    {
        bool ShieldOn;
        int ActiveAsteroids = 100;
        public Transform ShieldSpawnPosition;
        public GameObject PlayerShip;
        public GameObject ShieldPrefab;
        public GameObject AsteroidPrefab;
        private Vector3 moveDirection;
        public float playerShipSpeed = 100f;


        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < ActiveAsteroids; i++)
            {
                SpawnAsteroid();
            }
        }

        // Update is called once per frame
        void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Space))
                ShieldOn = true;

            moveDirection.x = Input.GetAxis("HorizontalX");
            moveDirection.z = Input.GetAxis("HorizontalZ");
            moveDirection.y = Input.GetAxis("VerticalY");
        }

        private void FixedUpdate()
        {
            MovePlayerShip(Time.fixedDeltaTime);
            if (ShieldOn)
            {
                ShieldOn = false;
                SpawnShield();
            }

            GameObject[] Asteroids;
            Asteroids = GameObject.FindGameObjectsWithTag("Enemy");
            int AsteroidsNowActive = Asteroids.Length;
            foreach (GameObject Asteroid in Asteroids)
            {
                if (Asteroid.transform.position.z < PlayerShip.transform.position.z)
                {
                    Destroy(Asteroid);
                    SpawnAsteroid();
                }
            }

            if (AsteroidsNowActive < ActiveAsteroids) SpawnAsteroid();


        }

        private void SpawnShield()
        {
            var PlayerShieldObj = Instantiate(ShieldPrefab, ShieldSpawnPosition.position, ShieldSpawnPosition.rotation);
            var PlayerShieldScript = PlayerShieldObj.GetComponent<PlayerShield>();
            PlayerShieldScript.Init(100f);
            PlayerShieldObj.transform.SetParent(ShieldSpawnPosition);          

        }

        private void MovePlayerShip(float delta)
        {           
            PlayerShip.transform.position += moveDirection * playerShipSpeed * delta;
        }

        private void SpawnAsteroid()
        {
            float AsteroidSpawnRange = 300f;
            var Asteroid = Instantiate(AsteroidPrefab, ShieldSpawnPosition.position + new Vector3(Random.Range(-AsteroidSpawnRange, AsteroidSpawnRange), Random.Range(-AsteroidSpawnRange, AsteroidSpawnRange), Random.Range(AsteroidSpawnRange, AsteroidSpawnRange+600)), ShieldSpawnPosition.rotation);

        }

    }
}

