using UnityEngine;

public class TieFighter : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destruye la bala cuando colisiona con un objeto
        Destroy(gameObject);

        // Destruye el objeto con el que colisiona
        // Destroy(collision.gameObject);
    }
}
