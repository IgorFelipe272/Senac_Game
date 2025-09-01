using UnityEngine;
using UnityEngine.SceneManagement;

public class Morte : MonoBehaviour
{
    public IF_CircleFade fade;

    public AudioClip morte;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem bateu foi o jogador (coloque a tag "Player" no objeto do jogador)
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(morte);
            fade.EncolherERestartar();
        }
    }
}
