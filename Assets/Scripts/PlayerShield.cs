using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private float _durability = 100f;

    public void Init(float _durability)
    {
        Destroy(gameObject, 3f);
    }

}
