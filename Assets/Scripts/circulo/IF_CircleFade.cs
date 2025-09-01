using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IF_CircleFade : MonoBehaviour
{
    public GameObject tela;
    public RectTransform rectTransform;

    public Vector2 tamanhoMaximo = new Vector2(2500, 2000);
    public float duracao = 1f;

    public GameObject player;

    private void Awake()
    {
        tela.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
    }
    private void Start()
    {
        Crescer();
    }

    // Vai de tamanhoMaximo para 0x0
    public void Encolher()
    {
        player.GetComponent<PlayerController>().enabled = false;
        StopAllCoroutines();
        StartCoroutine(AnimarTamanho(rectTransform.sizeDelta, Vector2.zero));
    }

    // Vai de 0x0 para tamanhoMaximo
    public void Crescer()
    {
        StopAllCoroutines();
        StartCoroutine(AnimarTamanho(rectTransform.sizeDelta, tamanhoMaximo));
    }

    private IEnumerator AnimarTamanho(Vector2 de, Vector2 para)
    {
        float tempo = 0f;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            rectTransform.sizeDelta = Vector2.Lerp(de, para, tempo / duracao);
            yield return null;
        }
        rectTransform.sizeDelta = para;

        player.GetComponent<PlayerController>().enabled = true;
    }

    // Chama Encolher e só depois reinicia a cena
    public void EncolherERestartar()
    {
        StartCoroutine(EncolherERestartarCoroutine());
    }

    private IEnumerator EncolherERestartarCoroutine()
    {
        yield return StartCoroutine(AnimarTamanho(rectTransform.sizeDelta, Vector2.zero));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
