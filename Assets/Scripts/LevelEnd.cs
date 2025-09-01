using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
    [Header("Configurações de Fase")]
    public string proximaCena;           // Nome da próxima cena
    public int itensNecessarios = 3;     // Quantos itens precisa coletar

    [Header("Indicadores Visuais (slots)")]
    public GameObject[] indicadores;     // Arraste aqui os 3 objetos (deixe eles DESATIVADOS no início)

    private int itensColetados = 0;

    public IF_CircleFade fade;

    public AudioClip fim;

    private void Start()
    {   
        // Garante que todos os indicadores comecem desativados
        foreach (GameObject obj in indicadores)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    // Função chamada quando coleta um item
    public void AdicionarItem()
    {
        if (itensColetados < itensNecessarios)
        {
            indicadores[itensColetados].SetActive(true); // Ativa o próximo indicador
            itensColetados++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itensColetados >= itensNecessarios)
            {
                Debug.Log("Todos os itens coletados! Mudando de fase...");

                StartCoroutine(TransicaoDeCena());
            }
            else
            {
                Debug.Log("Ainda faltam itens para coletar!");
            }
        }
    }

    private IEnumerator TransicaoDeCena()
    {
        fade.Encolher();

        // Espera a duração do fade

        AudioManager.instance.PlaySFX(fim);

        yield return new WaitForSeconds(fade.duracao);

        
        SceneManager.LoadScene(proximaCena);
    }
}
