using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 1.5f;

    private Vector3 velocity;

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        // Reseta gravidade quando toca o chão
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Inverte a leitura dos eixos com '-' para alinhar com a visão da câmera
        float inputX = -Input.GetAxisRaw("Horizontal");
        float inputZ = -Input.GetAxisRaw("Vertical");

        // Movimento ajustado à visão
        Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }

        // Pulo na tecla Espaço
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravidade contínua
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}