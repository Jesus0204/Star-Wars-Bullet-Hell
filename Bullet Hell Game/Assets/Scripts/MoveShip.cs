using UnityEngine;

public class MoveShip : MonoBehaviour
{
    // Velocidad del Vehículo (se modifica el valor en Unity)
    public float normalSpeed = 0.0f;
    public float boostedSpeed = 0.0f;
    private float currentSpeed;

    // Velocidad de Rotación (se modifica el valor en Unity)
    public float RotateSpeed = 0.0f;

    // Variable para el input del jugador horizontal (derecha o izquierda)
    public float horizontalInput;

    // Variable para el input del jugador vertical (adelante o atrás)
    public float forwardInput;

    // Variable para el input del jugador para rotar
    public float RotateInput;

    // Variable para la tecla de velocidad
    public KeyCode speedKey;

    // Variable para tecla de disparo
    public KeyCode shootKey;

    // Variable para cambio de cámara
    public KeyCode switchKey;

    // Variable para la cámara
    public Camera mainCamera;
    public Camera hoodCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el input horizontal del jugador, usando el nombre del InputManager
        horizontalInput = Input.GetAxis("Horizontal");

        // Obtener el input vertical del jugador, usando el nombre del InputManager
        forwardInput = Input.GetAxis("Vertical");

        // Obtener el input de rotación del jugador, usando el nombre del InputManager
        RotateInput = Input.GetAxis("Rotate");

        if (Input.GetKeyDown(speedKey))
        {
            if (currentSpeed == normalSpeed)
            {
                currentSpeed = boostedSpeed;
            }
            else
            {
                currentSpeed = normalSpeed;
            }
        }

        // Mover vehículo hacia adelante
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * forwardInput);

        // Modificar el giro
        transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);

        // Rotar el vehículo
        transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeed * RotateInput);

        // Cambio entre cámaras
        if(Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

    }
}
