using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{

    public float forcaX;
    public float forcaY;

    public float tempoVida;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 forca = new Vector2(forcaX, forcaY);
        this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        tempoVida -= Time.deltaTime;
        if (tempoVida < 0f) Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
        else if (col.collider.CompareTag("Chao")) Destroy(this.gameObject);
    }
}
