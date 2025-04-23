using UnityEngine;
using UnityEngine.SceneManagement;

public class ComenzarJuego : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Comenzar()
    {
        SceneManager.LoadScene("Juego");
    }
}
