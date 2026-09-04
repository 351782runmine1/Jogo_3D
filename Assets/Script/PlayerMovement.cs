using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [Header("Referências")]
    public CharacterController controller;

    [Header("Movimento")]
    public float speed = 6f;
    public float groundDrag = 0.1f;

    [Header("Pulo")]
    public float jumpForce = 5f;
    public float gravityScale = 2f;

    private Vector3 velocity = Vector3.zero;
    private float gravity = -9.81f;

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        // ============ ENTRADA ============
        float inputX = -Input.GetAxisRaw("Horizontal");
        float inputZ = -Input.GetAxisRaw("Vertical");

        // ============ MOVIMENTO HORIZONTAL ============
        Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized;
        Vector3 moveDirection = direction * speed;

        // Aplica drag quando está no chão
        if (controller.isGrounded)
        {
            moveDirection *= (1f - groundDrag);
        }

        // ============ PULO ============
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = jumpForce;
        }

        // ============ GRAVIDADE ============
        if (controller.isGrounded)
        {
            // Quando toca o chão, mantém no chão (sem forçar pra baixo)
            if (velocity.y < 0)
            {
                velocity.y = 0f;
            }
        }
        else
        {
            // Quando está no ar, aplica gravidade
            velocity.y += gravity * gravityScale * Time.deltaTime;
        }

        // Limita velocidade de queda máxima
        velocity.y = Mathf.Max(velocity.y, -20f);

        // ============ APLICA MOVIMENTO ============
        Vector3 finalMovement = new Vector3(moveDirection.x, velocity.y, moveDirection.z);
        controller.Move(finalMovement * Time.deltaTime);
    }
}