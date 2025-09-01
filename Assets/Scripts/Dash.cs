using UnityEngine;

public class Dash : MonoBehaviour
{
    public string chavePlayerPref;

    public LevelEnd fim;

    public AudioClip cenora;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Se tiver PlayerController, podemos dar efeito nele
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.podeDarDash = true;
            }

            if (!string.IsNullOrEmpty(chavePlayerPref))
            {
                PlayerPrefs.SetInt(chavePlayerPref, 1);
                PlayerPrefs.Save();
                Debug.Log(chavePlayerPref + " coletado!");
            }

            AudioManager.instance.PlaySFX(cenora);

            fim.AdicionarItem();

            gameObject.SetActive(false);
        }
    }
}
