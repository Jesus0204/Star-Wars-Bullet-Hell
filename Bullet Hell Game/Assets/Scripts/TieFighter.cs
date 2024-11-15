using UnityEngine;

public class TieFighter : MonoBehaviour
{
    public string objectToDestroy;

    void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(objectToDestroy))
        {
            // Destruye el TieFighter
            Destroy(gameObject);
        }

        // Opcional: Destruye el objeto con el que colisiona
        // Destroy(collision.gameObject);
    }
}
