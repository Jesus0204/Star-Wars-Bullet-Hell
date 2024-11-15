using UnityEngine;
using TMPro;

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
            UpdateText();
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
        }
    }

    void OnDestroy()
    {
        if(enemyShips != null)
        {
            enemyShips.TieFighterDestroyed();
        }
    }

    void UpdateText()
    {
        GameObject uiObject = GameObject.FindGameObjectWithTag("GameStateText");
        if (uiObject != null)
        {
            TextMeshProUGUI gameStateText = uiObject.GetComponent<TextMeshProUGUI>();
            if (gameStateText != null)
            {
                gameStateText.text = "Game Over";
            }
        }
    }
}
