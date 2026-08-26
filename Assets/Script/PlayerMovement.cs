using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float gravity = -20f; // Gravidade mais firme para o 3D
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.6f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        // Checagem se encostou no chão
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        // Se estiver no chão, zera o acúmulo da gravidade
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Entrada do teclado (WASD / Setas)
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized;

        if (move.magnitude >= 0.1f)
        {
            // Faz o boneco olhar para a direção em que anda
            transform.forward = move;
            controller.Move(move * speed * Time.deltaTime);
        }

        // Pulo no Espaço
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplica gravidade contínua
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Desenha a esfera no editor para você ver a detecção do chão
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}