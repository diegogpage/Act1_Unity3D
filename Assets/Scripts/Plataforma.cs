using UnityEngine;
using System.Collections;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private float retardo;
    private float tiempoAntesDeDesaparecer = 4f;
    private float tiempoParaReaparecer = 2f;

    private Collider col;
    private MeshRenderer rend;

    private void Start()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();
        StartCoroutine(DesaparecerYReaparecer());
    }

    public IEnumerator DesaparecerYReaparecer()
    {
        while (true)
        {
            yield return new WaitForSeconds(retardo);

            yield return new WaitForSeconds(tiempoAntesDeDesaparecer);

            col.enabled = false;
            rend.enabled = false;


            yield return new WaitForSeconds(tiempoParaReaparecer);

            col.enabled = true;
            rend.enabled = true;
        }
        
    }
}
