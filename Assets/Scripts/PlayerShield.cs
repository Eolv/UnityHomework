using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public void Init(float _durability)
    {
        Destroy(gameObject, 5f);
    }
}
