using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5;
    public float forcaPulo = 6f;

    private Rigidbody rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = -Input.GetAxis("Horizontal");
        float y = -Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(h, 0f, y) * velocidade;
        direcao.y = rb.linearVelocity.y;
        rb.linearVelocity = direcao;

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