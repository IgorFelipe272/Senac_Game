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
    public int pulosExtras = 1; // quantidade de pulos além do normal

    [Header("Dash")]
    public float forcaDash = 15f;
    public float tempoDash = 0.2f; // duração do dash
    public bool podeDarDash = true; // controla se o jogador já usou o dash

    private Rigidbody2D rb;
    private bool estaNoChao;
    private SpriteRenderer sprite;
    private Animator anim;

    private string animacaoAtual;
    private int pulosRestantes;
    private bool estaDandoDash = false;
    private float direcaoDash;

    public AudioClip pulo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        pulosRestantes = pulosExtras;
    }

    void Update()
    {
        // Verifica se está no chão
        estaNoChao = Physics2D.OverlapCircle(checadorChao.position, raioChao, camadaChao);

        // Reseta pulos e dash quando toca no chão
        if (estaNoChao)
        {
            pulosRestantes = pulosExtras;
        }

        // Movimento horizontal (apenas se não estiver dando dash)
        if (!estaDandoDash)
        {
            float movimento = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(movimento * velocidade, rb.linearVelocity.y);

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
                if (rb.linearVelocity.y < 0)
                    TrocarAnimacao("fimjump");
            }

            // Inverte o sprite
            if (movimento > 0)
                sprite.flipX = false;
            else if (movimento < 0)
                sprite.flipX = true;
        }

        // Pulo
        if (Input.GetButtonDown("Jump"))
        {
            if (estaNoChao) // pulo normal
            {
                Pular();
                TrocarAnimacao("iniciojump");
            }
            else if (pulosRestantes > 0) // pulos extras
            {
                Pular();
                TrocarAnimacao("iniciojump");
                pulosRestantes--;
            }
        }

        // Dash (pressione LeftShift, por exemplo)
        if (Input.GetKeyDown(KeyCode.LeftShift) && podeDarDash)
        {
            Dash();
        }
    }

    private void Pular()
    {
        AudioManager.instance.PlaySFX(pulo);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
    }

    private void Dash()
    {
        estaDandoDash = true;

        // direção: -1 esquerda / 1 direita
        direcaoDash = sprite.flipX ? -1f : 1f;

        // limpa velocidade Y e aplica força do dash
        rb.linearVelocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(direcaoDash * forcaDash, 0), ForceMode2D.Impulse);

        // pode adicionar uma animação de dash aqui
        //TrocarAnimacao("dash");

        // para o dash após um tempo
        Invoke(nameof(PararDash), tempoDash);
    }

    private void PararDash()
    {
        estaDandoDash = false;
    }

    private void TrocarAnimacao(string novaAnimacao)
    {
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
