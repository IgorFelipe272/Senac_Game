using UnityEngine;

public class Coletavel : MonoBehaviour
{
    public string chavePlayerPref;

    public LevelEnd fim;

    public AudioClip cenora;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

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
