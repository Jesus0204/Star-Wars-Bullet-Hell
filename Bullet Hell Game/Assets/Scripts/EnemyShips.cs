using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyShips : MonoBehaviour
{
    public Transform EstrellaDeLaMuerte;
    public GameObject TieFighter;
    public float velocidadTieFighter = 0.0f;

    public int numChorros = 0;
    public float tiempoEntreDisparos = 0.0f;
    public float numeroDeBatches = 0.0f;
    public float radioSpawn = 22.0f;  
    public float rotacionIncremento = 0.0f;
    private float currentRotation = 0.0f; 

    public TextMeshProUGUI numTieFightersText;

    // Contador de TieFighters
    private int tieFighterCount = 0;

    void Start()
    {
        StartCoroutine(SpawnTieFighters());
    }

    IEnumerator SpawnTieFighters()
    {
        for(int j = 0; j < numeroDeBatches; j++)
        {
            SpawnBatch();
            yield return new WaitForSeconds(tiempoEntreDisparos); 
        }
    }

    void SpawnBatch()
    {
        float anguloSeparacion = 360f / numChorros;

        for(int i = 0; i < numChorros; i++) {
            float angulo = i * anguloSeparacion + currentRotation;
            float rad = angulo * Mathf.Deg2Rad;

            Vector3 direccion = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;
            Vector3 posicionSpawn = EstrellaDeLaMuerte.position + direccion * radioSpawn;

            GameObject tieFighter = Instantiate(TieFighter, posicionSpawn, Quaternion.identity);

            Rigidbody rb = tieFighter.GetComponent<Rigidbody>();
            rb.linearVelocity = direccion * velocidadTieFighter;

            IncrementTieFighterCount(tieFighter);
        }

        currentRotation += rotacionIncremento;
        currentRotation %= 360f; 
    }

    void IncrementTieFighterCount(GameObject tieFighter)
    {
        tieFighterCount++;
        UpdateText();

        // Asignar el EnemyShips al script del TieFighter
        TieFighter tfScript = tieFighter.GetComponent<TieFighter>();
        if(tfScript != null)
        {
            tfScript.SetEnemyShips(this);
        }
    }

    public void TieFighterDestroyed()
    {
        tieFighterCount--;
        UpdateText();
    }

    void UpdateText()
    {
        numTieFightersText.text = $"Número de Tie Fighters actuales: {tieFighterCount} ";
    }
}
