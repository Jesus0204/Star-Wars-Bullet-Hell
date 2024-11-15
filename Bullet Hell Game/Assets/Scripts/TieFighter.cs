using UnityEngine;

public class TieFighter : MonoBehaviour
{
    public string objectToDestroy;
    public string laserTag;
    public string JediShipTag;

    public int lifeLeft;

    private EnemyShips enemyShips;

    // Método para asignar la referencia al EnemyShips
    public void SetEnemyShips(EnemyShips es)
    {
        enemyShips = es;
    }

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

        if (collision.gameObject.CompareTag(JediShipTag))
        {

            // Pausa el juego
            Time.timeScale = 0f;
        }

        // Opcional: Destruye el objeto con el que colisiona
        // Destroy(collision.gameObject);
    }

    void OnDestroy()
    {
        if(enemyShips != null)
        {
            enemyShips.TieFighterDestroyed();
        }
    }
}
