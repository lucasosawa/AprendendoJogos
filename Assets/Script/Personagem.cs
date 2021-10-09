using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personagem : MonoBehaviour
{
    // Velocidade
    public float velocidade;
    // Pulo
    private int qtdPulos;
    const int MAX_PULOS = 3;
    public float forcaPulo;

    // Texto
    public GameObject textoPowerup;
    private int qtdPowerup;

    // Sons
    public AudioClip somPegarPowerup;

    // Posição inicial
    private Vector3 posicaoInicial;

    // Andar
    private bool andandoDireita;
    private bool andandoEsquerda;

    // Morte
    private bool isDead;

    //tiro
    public GameObject tiro;
    private GameObject tiroAtual;

    // Start is called before the first frame update
    void Start()
    {
        // this.velocidade = 0.5f;
        this.qtdPulos = MAX_PULOS;
        this.qtdPowerup = 0;
        this.andandoDireita = false;
        this.andandoEsquerda = false;
        this.posicaoInicial = this.transform.position;
        AtualizarHUD();
        
    }

    // Update is called once per frame
    void Update()
    {
        VerificaAndar();
        VerificaPular();
        VerificaMorte();
        VerificaAtirar();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Chao")){
            this.qtdPulos = MAX_PULOS;
            this.GetComponent<Animator>().SetBool("isJumping", false);
        }
        if(col.collider.CompareTag("Enemy")) this.isDead = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Powerup")){
            Destroy(col.gameObject);
            this.qtdPowerup++;
            AtualizarHUD();

            this.GetComponent<AudioSource>().PlayOneShot(somPegarPowerup);
        }
    }

    public void AtualizarHUD(){
        textoPowerup.GetComponent<Text>().text = qtdPowerup.ToString();
    }

    public void VerificaAndar(){
        if (Input.GetKey(KeyCode.D)||andandoDireita) AndarDireita();
        else if (Input.GetKey(KeyCode.A)||andandoEsquerda) AndarEsquerda();
        else this.GetComponent<Animator>().SetBool("isRunnin", false);
    }
    
    public void AndarDireitaEnter(){
        this.andandoDireita = true;
    }
    public void AndarDireitaExit(){
        this.andandoDireita = false;
    }
    public void AndarEsquerdaEnter(){
        this.andandoEsquerda = true;
    }
    public void AndarEsquerdaExit(){
        this.andandoEsquerda = false;
    }


    public void AndarDireita(){
        Vector3 posicao = this.transform.position;
        posicao.x += velocidade;
        this.transform.position = posicao;
        // Animação
        this.GetComponent<Animator>().SetBool("isRunnin", true);
        this.GetComponent<SpriteRenderer>().flipX = false;
    }
    public void AndarEsquerda(){
        Vector3 posicao = this.transform.position;
        posicao.x -= velocidade;
        this.transform.position = posicao;
        // Animação
        this.GetComponent<Animator>().SetBool("isRunnin", true);
        this.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void VerificaPular(){
        if (Input.GetKey(KeyCode.W)) Pular();
    }

    public void Pular(){
        if(this.qtdPulos>0){
            this.qtdPulos--;
            Vector2 forca = new Vector2(0f, this.forcaPulo);
            this.GetComponent<Rigidbody2D>().AddForce(forca, ForceMode2D.Impulse);
            this.GetComponent<Animator>().SetBool("isJumping", true);
        } 
        
    }

    public void VerificaMorte(){
        Vector3 posicaoAtual = this.transform.position;
        if(posicaoAtual.y < -10f) this.isDead = true;
        if(this.isDead){
            this.transform.position = this.posicaoInicial;
            this.isDead = false;
        }
    }

    public void VerificaAtirar(){
        if (Input.GetKey(KeyCode.Space) && this.tiroAtual == null)
        {
            Vector3 posicaoTiro = this.transform.position;
            if (this.GetComponent<SpriteRenderer>().flipX) posicaoTiro.x -= 1f;
            else posicaoTiro.x += 1f;
            this.tiroAtual = Instantiate(this.tiro, posicaoTiro, this.transform.rotation);
            if (this.GetComponent<SpriteRenderer>().flipX) 
                this.tiroAtual.GetComponent<Tiro>().forcaX *= -1;

        }
    }

}
