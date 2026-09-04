using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    // COMPONENTES E CONFIGURAÇÕES
    public CharacterController controller; // Componente que move e trata colisões do personagem
    public float speed = 6f;               // Velocidade de caminhada
    public float jumpForce = 8f;           // Força aplicada ao pular
    public float gravityScale = 1.5f;      // Multiplicador de gravidade (queda mais rápida/lenta)

    // VARIÁVEIS INTERNAS DE FÍSICA E CONTROLE
    private Vector3 velocity = Vector3.zero; // Guarda a velocidade vertical (pulo/queda)
    private float gravity = -9.81f;          // Gravidade base
    private bool jumpPressed = false;        // Trava para controlar o pulo

    void Start()
    {
        // Se o controller não foi arrastado no Inspector, busca no próprio objeto
        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. LEITURA DE ENTRADA E DIREÇÃO (WASD / Setas)
        float inputX = -Input.GetAxisRaw("Horizontal"); // Inverte eixo X
        float inputZ = -Input.GetAxisRaw("Vertical");   // Inverte eixo Z
        Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized; // Normaliza para não andar rápido na diagonal
        Vector3 moveDirection = direction * speed;

        // Reduz levemente a velocidade no chão para não deslizar
        if (controller.isGrounded)
            moveDirection *= 0.9f;

        // 2. LÓGICA DO PULO
        // Pula se apertar Espaço, estiver tocando o chão e não tiver pulado ainda
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !jumpPressed)
        {
            velocity.y = jumpForce;
            jumpPressed = true;
        }

        // Reseta o controle de pulo ao encostar no chão
        if (controller.isGrounded)
            jumpPressed = false;

        // 3. APLICAÇÃO DA GRAVIDADE
        if (controller.isGrounded)
        {
            // No chão: zera a velocidade de queda
            if (velocity.y < 0)
                velocity.y = 0f;
        }
        else
        {
            // No ar: acelera a queda a cada frame
            velocity.y += gravity * gravityScale * Time.deltaTime;
        }

        // Limita a velocidade máxima de queda em -20
        velocity.y = Mathf.Max(velocity.y, -20f);

        // 4. MOVIMENTAÇÃO FINAL
        // Une o movimento horizontal (X, Z) com a gravidade/pulo (Y) ajustado pelo tempo de quadros (FPS)
        Vector3 finalMovement = new Vector3(moveDirection.x, velocity.y, moveDirection.z);
        controller.Move(finalMovement * Time.deltaTime);
    }
}