using UnityEngine;

public class Bala : MonoBehaviour
{

    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destruye la bala cuando colisiona con un objeto
        Destroy(gameObject);
    }
}
