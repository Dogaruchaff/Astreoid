using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer sRenderer;
    private Rigidbody2D rb;
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50;
    public float maxLifeTime = 30f;
    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
       sRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

       this.transform.eulerAngles = new Vector3(0.0f,0.0f, Random.value * 360.0f);

       this.transform.localScale = Vector3.one * this.size;

       rb.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(this.gameObject, this.maxLifeTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if (this.size / 2 >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
    }
    
    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, this.transform.position, this.transform.rotation);
        half.size = this.size / 2;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
