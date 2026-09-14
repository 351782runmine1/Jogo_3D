using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 6f;

    [Header("Sensibilidade do Mouse")]
    public float sensibilidadeMouse = 3f;

    private Rigidbody rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // MOUSE X gira o personagem no eixo Y
        float giroHorizontal = Input.GetAxis("Mouse X") * sensibilidadeMouse;
        transform.Rotate(0, giroHorizontal, 0);

        // Pulo
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // O (-) inverte a direcao para bater exatamente com o modelo 180°
        Vector3 direcao = (-transform.forward * v - transform.right * h).normalized;
        Vector3 velocidadeFinal = direcao * velocidade;
        velocidadeFinal.y = rb.linearVelocity.y;

        rb.linearVelocity = velocidadeFinal;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }
}