using Unity.VisualScripting;
using UnityEngine;

public class ActivadorParedes : MonoBehaviour
{
    [SerializeField] private ParedMovible pared1;
    [SerializeField] private ParedMovible pared2;
    [SerializeField] private AudioSource paredMoviendo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pare que se reproduzca a la mitad de velocidad
        paredMoviendo.pitch = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            pared1.MoverPared();
            pared2.MoverPared();
            paredMoviendo.Play();
        }
    }

    public void Recolocar()
    {
        pared1.Recolocar();
        pared2.Recolocar();
        paredMoviendo.Stop();
    }
}
