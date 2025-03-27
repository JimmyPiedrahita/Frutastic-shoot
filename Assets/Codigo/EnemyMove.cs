using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform player; //Obtiene la posicion del player para ir hacia esa direccion
    public float speed = 3f; //Velocidad
    private int health = 3; //Vida
    public AudioClip audioDead;
    public AudioSource enemyDead;

    public int Health { get => health; set => health = value; }

    void Start()
    {
        player = FindAnyObjectByType<PlayerMove>().gameObject.transform;
    }

    void FixedUpdate()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision) //Verifica si hubo un choque con un objeto con fisicas
    {
        if (collision.gameObject.CompareTag("Bullet")) //Verifica si el choque fue contra una bala
        {
            Health = Health-1; //Resta la vida en cada choque con una bala
            PlayerMove.instance.puntuacion += 1; //Aumentar la puntuacion
            if (Health <= 0) //Verifica que ya no tenga vida
            {
                enemyDead.PlayOneShot(audioDead);
                Destroy(gameObject, 0.2f); //Destruye el objeto es decir el enemigo
            }
        }
    }
}