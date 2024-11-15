using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyShips : MonoBehaviour
{
    public Transform EstrellaDeLaMuerte;
    public GameObject TieFighter;
    public float velocidadTieFighter = 0.0f;

    public int numChorros = 0;
    public int numChorrosCirculo = 0;
    public float tiempoEntreDisparos = 0.0f;
    public float numeroDeBatches = 0.0f;
    public float radioSpawn = 22.0f;  
    public float rotacionIncremento = 0.0f;
    private float currentRotation = 0.0f; 

    public TextMeshProUGUI numTieFightersText;

    // Contador de TieFighters
    private int tieFighterCount = 0;

    // Indica si la Estrella de la Muerte está en movimiento
    private bool deathStarisMoving = false;
    private Vector3 movimientoCentro; // Centro del movimiento circular
    private float movimientoAngulo = 0.0f;
    public float movimientoRadio = 0.0f; // Radio del movimiento circular
    public float movimientoVelocidad = 0.0f; // Velocidad de rotación

    private bool gameFinished = false;

    void Start()
    {
        StartCoroutine(SpawnTieFighters());
    }

    void Update()
    {
        if(deathStarisMoving)
        {
            movimientoAngulo += movimientoVelocidad * Time.deltaTime;
            if(movimientoAngulo >= 360f)
                movimientoAngulo -= 360f;

            // Convertir el ángulo a radianes
            float rad = movimientoAngulo * Mathf.Deg2Rad;

            // Calcular el offset basado en el ángulo y el radio
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * movimientoRadio;

            // Actualizar la posición del objeto
            EstrellaDeLaMuerte.position = movimientoCentro + offset;
        }

        if (gameFinished && tieFighterCount == 0)
        {
            GameObject uiObject = GameObject.FindGameObjectWithTag("GameStateText");
            if (uiObject != null)
            {
                TextMeshProUGUI gameStateText = uiObject.GetComponent<TextMeshProUGUI>();
                if (gameStateText != null)
                {
                    gameStateText.text = "You Win!";
                }
            }
        }
    }

    IEnumerator SpawnTieFighters()
    {
        for(int j = 0; j < numeroDeBatches; j++)
        {
            SpawnBatch();
            yield return new WaitForSeconds(tiempoEntreDisparos);
        }

        yield return new WaitForSeconds(6.0f);

        movimientoCentro = EstrellaDeLaMuerte.position;
        deathStarisMoving = true;

        for (int i = 0; i < numeroDeBatches / 1.5; i++)
        {
            SpawnCircleBatch();
            yield return new WaitForSeconds(tiempoEntreDisparos * 3);
        }

        deathStarisMoving = false;

        yield return new WaitForSeconds(6.0f);

        for (int i = 0; i < numeroDeBatches; i++)
        {
            SpawnFlowerBatch();
            yield return new WaitForSeconds(tiempoEntreDisparos);
        }

        gameFinished = true;
        
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

    void SpawnCircleBatch() {
        float anguloSeparacion = 360f / numChorrosCirculo;

        for(int i = 0; i < numChorrosCirculo; i++) {
            float angulo = i * anguloSeparacion;
            float rad = angulo * Mathf.Deg2Rad;

            Vector3 direccion = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;
            Vector3 posicionSpawn = EstrellaDeLaMuerte.position + direccion * radioSpawn;

            GameObject tieFighter = Instantiate(TieFighter, posicionSpawn, Quaternion.identity);

            Rigidbody rb = tieFighter.GetComponent<Rigidbody>();
            rb.linearVelocity = direccion * velocidadTieFighter;

            IncrementTieFighterCount(tieFighter);
        }
    }

    void SpawnFlowerBatch() {
        float anguloSeparacion = 360f / numChorros;

        for(int i = 0; i < numChorros; i++) {
            float direccionAngulo;

            if (i % 2 == 0)
            {
                direccionAngulo = 1f; 
            }
            else
            {
                direccionAngulo = -1f; 
            }
            float angulo = i * anguloSeparacion + currentRotation * direccionAngulo;
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
        numTieFightersText.text = $"Número de Tie Fighters actuales: {tieFighterCount}";
    }
}
