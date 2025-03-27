using UnityEngine;
public class BulletMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D; //Contiene las fisicas de la bala
    private Vector2 direccion; //Contiene hacia donde tiene que ir la bala
    public float speed = 10; //Contiene la velocidad de la bala
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();//Obtiene el rigidBody del objeto relacionado al script
    }

    void FixedUpdate()
    {
        //El metodo velocity aplica fuerza para desplazar el objeto con dichas fisicas
        //La velocidad va a ser la direccion hacia la que se dirige por la velocidad que le ingresemos
        //Es decir el resultado sera un vector escalado a la velocidad
        rigidbody2D.velocity = direccion.normalized * speed; 
    }

    public void setDireccion(Vector2 direccion)
    {
        this.direccion = direccion;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bordes"))// Verifica si la colisión es con un enemigo o con los bordes
        {
            Destroy(gameObject);// Destruye la bala al colisionar
        }
    }
}