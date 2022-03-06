using spacegame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{    
    public float AsteroidSpeed1, AsteroidSpeed2, AsteroidSpeed3;
    [SerializeField] private PlayerShipControl _player;
    
    // Start is called before the first frame update
    void Start()
    {
        AsteroidSpeed1 = Random.Range(-20.0f, 20.0f);
        AsteroidSpeed2 = Random.Range(-20.0f, 20.0f);
        AsteroidSpeed3 = Random.Range(-20.0f, 20.0f);        
        Destroy(gameObject, 15f);
        //_player = FindObjectOfType<PlayerShipControl>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {        
        gameObject.transform.position += new Vector3(AsteroidSpeed1, AsteroidSpeed2, AsteroidSpeed3) * Time.deltaTime;
    }
}
