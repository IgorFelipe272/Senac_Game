using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    [Header("Pulo")]
    public float forcaPulo = 12f;
    public Transform checadorChao;
    public float raioChao = 0.2f;
    public LayerMask camadaChao;

    private Rigidbody2D rb;
    private bool estaNoChao;
    private SpriteRenderer sprite;
    private Animator anim;

    private string animacaoAtual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento horizontal
        float movimento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimento * velocidade, rb.linearVelocity.y);

        // Verifica se está no chão
        estaNoChao = Physics2D.OverlapCircle(checadorChao.position, raioChao, camadaChao);

        // Pulo
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
            TrocarAnimacao("iniciojump");
        }

        // Controle de animações
        if (estaNoChao)
        {
            if (movimento != 0)
                TrocarAnimacao("run");
            else
                TrocarAnimacao("idle");
        }
        else
        {
            // Se estiver caindo
            if (rb.linearVelocity.y < 0)
                TrocarAnimacao("fimjump");
        }

        // Inverte o sprite
        if (movimento > 0)
            sprite.flipX = false;
        else if (movimento < 0)
            sprite.flipX = true;
    }

    private void TrocarAnimacao(string novaAnimacao)
    {
        // Evita ficar repetindo a mesma animação todo frame
        if (animacaoAtual == novaAnimacao) return;

        anim.Play(novaAnimacao);
        animacaoAtual = novaAnimacao;
    }

    private void OnDrawGizmosSelected()
    {
        if (checadorChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checadorChao.position, raioChao);
        }
    }
}
