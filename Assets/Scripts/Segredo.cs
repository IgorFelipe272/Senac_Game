using UnityEngine;

public class Segredo : MonoBehaviour
{
    [Header("Objeto que será escondido/mostrado")]
    public GameObject objetoAlvo;

    [Header("Tag do jogador")]
    public string tagJogador = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagJogador) && objetoAlvo != null)
        {
            objetoAlvo.SetActive(false); // Esconde
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagJogador) && objetoAlvo != null)
        {
            objetoAlvo.SetActive(true); // Mostra de novo
        }
    }
}
