using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    // Esta função limpa o progresso das abas para começar um jogo novo
    public void IniciarNovaPartida()
    {
        PlayerPrefs.DeleteKey("ClicouAlimentacao");
        PlayerPrefs.DeleteKey("ClicouRoda");
        PlayerPrefs.DeleteKey("ClicouMaAlimentacao");
        PlayerPrefs.Save(); // Garante que a memória é limpa imediatamente
        
        Debug.Log("Memória limpa! O Quiz vai começar trancado nesta nova partida.");
    }
}