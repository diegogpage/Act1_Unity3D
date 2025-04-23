using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float factorGravedad;
    [SerializeField] private float alturaDeSalto;
    [SerializeField] private Transform camara;
    [SerializeField] private InputManagerSO inputManager;
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private TextMeshProUGUI tiempo;
    [SerializeField] private float time;


    [Header("Detección suelo")]
    [SerializeField] private Transform pies;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private LayerMask queEsSuelo;

    private CharacterController controller;
    private Animator anim;
    private Vector3 direccionMovimiento;
    private Vector3 direccionInput;
    private Vector3 velocidadVertical;
    

    private bool gravedadInvertida = false;

    private void OnEnable()
    {
        inputManager.OnSaltar += Saltar;
        inputManager.OnMover += Mover;
    }

    private void Mover(Vector2 ctx)
    {
        direccionInput = new Vector3(ctx.x, 0, ctx.y);
    }

    private void Saltar()
    {
        Debug.Log("Intento saltar");

        if (EstoyEnSuelo())
        {
            Debug.Log("Saltar");
            velocidadVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaDeSalto);
            //y = sqrt(-2 * g * h)
            anim.SetTrigger("jump");
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Para el ratón
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Para que se mueva hacia delante o detrás en función de donde apunte la cámara me creo un vector de direccion en función de ella
        direccionMovimiento = camara.forward * direccionInput.z + camara.right * direccionInput.x;
        direccionMovimiento.y = 0; //Para evitar que se levante del suelo

        //El controller.Move funciona igual que el transform.translate
        controller.Move(direccionMovimiento * speed * Time.deltaTime);

        anim.SetFloat("velocidad", controller.velocity.magnitude); //Para saber la velocidad en ese instante
        
        if(direccionMovimiento.sqrMagnitude > 0)
        {
            RotarHaciaObjetivo();
        }

        if (EstoyEnSuelo() && velocidadVertical.y < 0) 
        {
            //Si he aterrizado reseteo la velocidad vertical
            velocidadVertical.y = 0;
            anim.ResetTrigger("jump"); //Para que no se acumulen triggers
        }
        AplicarGravedad();

        BajarTiempo();
    }

    private void AplicarGravedad()
    {
        //Cambia según si la gravedad está invertida o no
        if (!gravedadInvertida)
        {
            velocidadVertical.y += factorGravedad * Time.deltaTime;
        }
        else
        {
            velocidadVertical.y -= factorGravedad * Time.deltaTime;
        }

        controller.Move(velocidadVertical * Time.deltaTime);
        //Multiplico ambas veces por Time.deltaTime porque es una aceleración (s^2)
    }

    private bool EstoyEnSuelo()
    {
        return Physics.CheckSphere(pies.position, radioDeteccion, queEsSuelo);
        //Devuelve un bool para saber si estoy en el suelo o en el aire
    }

    private void RotarHaciaObjetivo()
    {
        //Roto al jugador hacia el objetivo al que se va a dirigir
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
        transform.rotation = rotacionObjetivo;
    }


    private void OnControllerColliderHit(ControllerColliderHit elOtro)
    {
        if (elOtro.gameObject.CompareTag("ParedMov"))
        {
            Debug.Log("choco");
            Respawn();
            ActivadorParedes activador = FindAnyObjectByType<ActivadorParedes>();
            activador.Recolocar();
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawneo");
        controller.enabled = false;
        transform.position = respawnPosition.position;
        controller.enabled = true;
    }

    public void CambioGravedad()
    {
        gravedadInvertida = !gravedadInvertida; //Cambio el estado de la gravedad
        velocidadVertical = Vector3.zero;

        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.y = -nuevaEscala.y; //Invierto la escala en Y

        //Aplico el cambio de visual y roto
        transform.localScale = nuevaEscala; 
        transform.Rotate(0, 180, 0);
    }

    private void BajarTiempo()
    {
        time -= Time.deltaTime;

        tiempo.text = "Time: " + (int)time;

        if (time <= 0)
        {
            Derrota();
        }
    }

    private void Derrota()
    {
        SceneManager.LoadScene("Derrota");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }
}
