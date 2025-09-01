using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Nome da Próxima Cena")]
    public string proximaCena;
    public PauseMenu pauseMenu;

    // Função para carregar a cena definida na string
    public void CarregarCena()
    {
        if (!string.IsNullOrEmpty(proximaCena))
        {
            pauseMenu.Retomar();
            SceneManager.LoadScene(proximaCena);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena foi definida em NextScene!");
        }
    }
}
