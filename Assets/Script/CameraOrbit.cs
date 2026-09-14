using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Alvo")]
    public Transform alvo; // Arraste o Player aqui
    public Renderer[] renderersDoPlayer; // Roupas, cabelo, corpo, etc.

    [Header("Configuracoes Gerais")]
    public float sensibilidadeMouseY = 3.0f;
    public bool primeiraPessoa = false;

    [Header("Terceira Pessoa")]
    public float distancia3P = 4.0f;
    public float altura3P = 1.8f;
    public float limiteMin3P = -20f;
    public float limiteMax3P = 50f;

    [Header("Primeira Pessoa")]
    public float altura1P = 1.5f;
    public float limiteMin1P = -70f;
    public float limiteMax1P = 80f;

    private float rotacaoX = 15f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            primeiraPessoa = !primeiraPessoa;
            AtualizarVisibilidadeModelo(!primeiraPessoa);
        }
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        rotacaoX -= Input.GetAxis("Mouse Y") * sensibilidadeMouseY;

        if (primeiraPessoa)
        {
            rotacaoX = Mathf.Clamp(rotacaoX, limiteMin1P, limiteMax1P);
            Vector3 posicaoOlhos = alvo.position + Vector3.up * altura1P;
            transform.position = posicaoOlhos;

            Quaternion rotacaoCamera = Quaternion.Euler(rotacaoX, alvo.eulerAngles.y + 180f, 0);
            transform.rotation = rotacaoCamera;
        }
        else
        {
            rotacaoX = Mathf.Clamp(rotacaoX, limiteMin3P, limiteMax3P);

            Quaternion rotacaoCamera = Quaternion.Euler(rotacaoX, alvo.eulerAngles.y + 180f, 0);
            Vector3 posicaoAlvo = alvo.position + Vector3.up * altura3P;

            transform.position = posicaoAlvo - (rotacaoCamera * Vector3.forward * distancia3P);
            transform.LookAt(posicaoAlvo);
        }
    }

    void AtualizarVisibilidadeModelo(bool visivel)
    {
        if (renderersDoPlayer != null)
        {
            foreach (Renderer r in renderersDoPlayer)
            {
                if (r != null) r.enabled = visivel;
            }
        }
    }
}