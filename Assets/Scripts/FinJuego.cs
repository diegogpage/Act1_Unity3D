using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJuego : MonoBehaviour
{

    private float timer = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
