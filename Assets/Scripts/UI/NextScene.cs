using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [Header("Nome da Próxima Cena")]
    public string proximaCena;

    // Função para carregar a cena definida na string
    public void CarregarCena()
    {
        if (!string.IsNullOrEmpty(proximaCena))
        {
            SceneManager.LoadScene(proximaCena);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena foi definida em NextScene!");
        }
    }
}
