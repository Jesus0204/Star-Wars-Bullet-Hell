using UnityEngine;

public class TieFighter : MonoBehaviour
{
    public string objectToDestroy;
    public string laserTag;

    public int lifeLeft;

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

        if (collision.gameObject.CompareTag(laserTag))
        {
            if (lifeLeft - 1 > 0)
            {
                lifeLeft--;
            }
            else
            {
                // Destruye el TieFighter
                Destroy(gameObject);
            }
        }

        // Opcional: Destruye el objeto con el que colisiona
        // Destroy(collision.gameObject);
    }
}
