using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Referência ao Menu de Pause")]
    public GameObject menuPause;

    private bool jogoPausado = false;

    void Update()
    {
        // Verifica se apertou ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
                Retomar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        menuPause.SetActive(true);   // mostra o menu
        Time.timeScale = 0f;        // pausa o jogo
        jogoPausado = true;
    }

    public void Retomar()
    {
        menuPause.SetActive(false); // esconde o menu
        Time.timeScale = 1f;        // volta ao normal
        jogoPausado = false;
    }
}
