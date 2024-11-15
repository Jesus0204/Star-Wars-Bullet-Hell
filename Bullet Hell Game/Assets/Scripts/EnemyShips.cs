using UnityEngine;
using System.Collections;

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

            GameObject bala = Instantiate(TieFighter, posicionSpawn, Quaternion.identity);

            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.linearVelocity = direccion * velocidadTieFighter;
        }

        currentRotation += rotacionIncremento;
        currentRotation %= 360f; 
    }
}
