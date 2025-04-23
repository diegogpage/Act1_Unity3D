using UnityEngine;
using UnityEngine.SceneManagement;

public class CondicionVictoria : MonoBehaviour
{
    private void OnTriggerEnter(Collider elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
