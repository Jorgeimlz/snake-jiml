using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Necesario para manejar el cambio de escenas y reinicios

public class SnakeMovement : MonoBehaviour
{
    public float speed = 5.0f;         // Velocidad a la que se mueve la serpiente hacia adelante
    public float rotationSpeed = 200.0f; // Velocidad de rotación al girar
    public GameObject tailPrefab;      // Prefab para las partes del cuerpo de la serpiente
    public float zOffset = 1.0f;       // Distancia entre las partes del cuerpo

    private List<Transform> tail = new List<Transform>();  // Lista para mantener las partes del cuerpo

    void Update()
    {
        // Mover hacia adelante de manera continua
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Mover la cola
        MoveTail();
    }

    void MoveTail()
    {
        // Recorre cada segmento de la cola
        for (int i = tail.Count - 1; i > 0; i--)
        {
            // Sigue al segmento delante de él
            tail[i].position = Vector3.Lerp(tail[i].position, tail[i - 1].position, Time.deltaTime * speed);
        }

        // El primer segmento sigue a la cabeza de la serpiente
        if (tail.Count > 0)
        {
            tail[0].position = Vector3.Lerp(tail[0].position, transform.position - transform.forward * zOffset, Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject); // Destruye la comida
            AddTail(); // Añade una parte al cuerpo de la serpiente
        }
    }

    void AddTail()
    {
        // Crea una nueva parte de la cola en la posición correcta
        Vector3 newTailPosition = tail.Count == 0 ? transform.position - transform.forward * zOffset : tail[tail.Count - 1].position - tail[tail.Count - 1].forward * zOffset;
        GameObject newTailPart = Instantiate(tailPrefab, newTailPosition, Quaternion.identity);
        tail.Add(newTailPart.transform);
        newTailPart.tag = "Tail";  // Asegúrate de que el nuevo segmento tenga el tag "Tail"
        newTailPart.GetComponent<Collider>().isTrigger = true;  // El collider debe ser un trigger
    }



}
