using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject menuPerdiste;
    public static PlayerMove instance;
    public GameObject bulletPrefab; //Bala que el player va a disparar
    public float velocidad = 20; //Velocidad de movimiento
    private Rigidbody2D player; //Fisicas del player
    private Vector2 movimiento; //Movimiento que hace el player
    private float lastShoot; //Regular la cantidad de disparos
    public int puntuacion = 0; //Puntos del player
    public TextMeshProUGUI puntuacionText; //Texto para mostrar el puntaje en vivo
    public TextMeshProUGUI puntuacionFinalText; //Texto para mostrar el puntaje final
    private int vidaPlayer = 5; //Vidas del player
    private float timerVida = 0f; //contiene el tiempo desde la ultima vez que le bajo la vida al player
    private float intervalo = 1f; //contiene el tiempo que debe de esperar para bajar la vida al player
    public GameObject[] vidas; //Contiene las imagenes de las vidas del player
    private Animator animator;
    public GameObject damage;
    private bool atacado = false;
    private float tiempoDamage = 0f;
    private float intervaloDamage = 1f;
    public AudioSource audioSource;
    public AudioSource audioShoot;
    public AudioSource audioQuejido;
    public AudioSource audioBanana;
    public AudioSource audioCereza;
    public AudioSource audioPera;
    public AudioSource audioNaranja;
    public AudioSource audioFresa;
    public AudioSource audioSandia;
    public AudioSource audioPiña;
    public AudioSource audioCoco;
    public AudioSource audioManzana;

    private void OnCollisionStay2D(Collision2D collision)//Verifica si hubo un choque con objetos con fisicas
    {
        if (collision.gameObject.CompareTag("Enemy"))//Verifica si choco con un enemigo 
        {
            if (timerVida > intervalo)
            {
                audioQuejido.Play();
                atacado = true;
                vidas[vidaPlayer-1].SetActive(false);
                vidaPlayer--;
                timerVida = 0f;
            }
            timerVida += Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioQuejido.Play();
            atacado = true;
            vidas[vidaPlayer - 1].SetActive(false);
            vidaPlayer--;
        }
        if (collision.gameObject.CompareTag("banana"))
        {
            audioBanana.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("cereza"))
        {
            audioCereza.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("pera"))
        {
            audioPera.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("naranja"))
        {
            audioNaranja.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("fresa"))
        {
            audioFresa.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("sandia"))
        {
            audioSandia.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("piña"))
        {
            audioPiña.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("coco"))
        {
            audioCoco.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
        if (collision.gameObject.CompareTag("manzana"))
        {
            audioManzana.Play();
            Destroy(collision.gameObject);
            puntuacion = puntuacion + 10;
        }
    }

    void Start()
    {
        audioSource.Play();
        animator = GetComponent<Animator>();
        instance = this;
        player = GetComponent<Rigidbody2D>(); //Obtiene las fisicas del player
    }
    void Update()
    {
        animator.SetBool("shoot", false);
        puntuacionText.text = puntuacion.ToString();
        float movimientoX = Input.GetAxisRaw("Horizontal"); //Obtiene valores dependiendo de las teclas (A, D, LEFT, RIGHT)
        float movimientoY = Input.GetAxisRaw("Vertical"); //Obtiene valores dependiendo de las teclas (W, S, TOP, DOWN)
        movimiento = new Vector2(movimientoX, movimientoY).normalized;

        animator.SetBool("left", movimientoX == -1.0f && movimientoY == 0.0f);
        animator.SetBool("right", movimientoX == 1.0f && movimientoY == 0.0f);
        animator.SetBool("up", movimientoY == 1.0f && movimientoX == 0.0f);
        animator.SetBool("down", movimientoY == -1.0f && movimientoX == 0.0f);
        animator.SetBool("leftDown", movimientoX == -1.0f && movimientoY == -1.0f);
        animator.SetBool("leftUp", movimientoX == -1.0f && movimientoY == 1.0f);
        animator.SetBool("rightDown", movimientoX == 1.0f && movimientoY == -1.0f);
        animator.SetBool("rightUp", movimientoX == 1.0f && movimientoY == 1.0f);

        if (!IsMouseOverUI() || IsMouseOverBloodImage())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > lastShoot + 0.10f) //Verifica que se presione el boton izquierdo del mouse
            {
                Vector3 mousePosicion = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Obtiene la posicion del mouse
                shoot(mousePosicion);
                lastShoot = Time.time;
            }
        }

        if ( vidaPlayer == 0)
        {
            Time.timeScale = 0f;
            menuPerdiste.SetActive(true);   
            puntuacionFinalText.text = puntuacion.ToString();
            vidaPlayer = 5;
        }

        if (puntuacion == 100)
        {
            ControladorEnemigos.instance.tiempoEnemigos  = 1;
        }

        if(puntuacion == 200)
        {
            ControladorEnemigos.instance.tiempoEnemigos = 0.5f;
        }

        if (puntuacion == 300)
        {
            ControladorEnemigos.instance.tiempoEnemigos = 0.1f;
        }
        if (atacado)
        {
            tiempoDamage += Time.deltaTime;
            damage.SetActive(true);
            
            if (tiempoDamage > intervaloDamage)
            {
                atacado = false;
                damage.SetActive(false);
                tiempoDamage = 0.0f;
            }
        }
        
   }
    private void shoot(Vector3 mousePosicion)
    {
        audioShoot.Play();
        animator.SetBool("shoot", true);
        Vector3 direccion = (mousePosicion - transform.position).normalized; //direccion hacia la posicion del mouse
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (direccion * 1.5f), Quaternion.identity); //Duplica el bulletPrefab en la posicion y hacia la direccion del mouse
        bullet.GetComponent<BulletMove>().setDireccion(direccion);
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); // Utiliza EventSystem para verificar si el mouse está sobre elementos de la interfaz de usuario
    }
    private void FixedUpdate()
    {
        player.MovePosition(player.position + movimiento * velocidad * Time.fixedDeltaTime); //Movimiento del player
    }
    private bool IsMouseOverBloodImage()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.CompareTag("DamageImage"))
            {
                return true;
            }
        }
        return false;
    }
}
