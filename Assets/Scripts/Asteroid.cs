using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float minRandom = -5;
    private float maxRandom = 5;
    private Vector3 _asteroidRandomMovement;

    void Start()
    {
        _asteroidRandomMovement = new Vector3(Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom));
    }

    private void FixedUpdate()
    {        
        gameObject.transform.position += _asteroidRandomMovement * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Корабль врезался в астероид!");
        }
    }
}
