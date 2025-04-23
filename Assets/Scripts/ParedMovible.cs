using UnityEngine;

public class ParedMovible : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform posicionFinal;
    private Vector3 posicionInicial;
    private bool meMuevo = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (meMuevo)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionFinal.position, speed*Time.deltaTime);
        }
    }

    public void MoverPared()
    {
        Debug.Log("Me muevo");
        meMuevo = true;
    }

    public void Recolocar()
    {
        Debug.Log("Doy");
        meMuevo = false;
        transform.position = posicionInicial;
    }
}
