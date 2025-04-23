using UnityEngine;

public class InvertirGravedad : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            player.CambioGravedad(); 
        }
    }

    private void OnTriggerExit(Collider elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            player.CambioGravedad();     
        }
    }

}
