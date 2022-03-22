using SpaceGame;
using UnityEngine;

public class HP : MonoBehaviour
{    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealable heal))
        {
            heal.Heal();
            Destroy(gameObject);
        }
    }
}
