using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Velocidade
    public float velocidade = 0.05f;

    // Andar
    private bool andandoDireita;
    private bool andandoEsquerda;

    // Posição inicial
    private Vector3 posicaoInicial;

    private GameObject personagem;

    // Morte
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        this.andandoDireita = false;
        this.andandoEsquerda = false;
        this.posicaoInicial = this.transform.position;
        this.personagem = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        VerificaAndar();
        VerificaMorte();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.collider.CompareTag("Player")){
            this.isDead = true;
        } 
    }

    public void VerificaAndar(){
        if(personagem.transform.position.x > this.transform.position.x){
            this.andandoDireita= true;
            this.andandoEsquerda= false;
        }
        else{
            this.andandoDireita= false;
            this.andandoEsquerda= true;
        }
        if (andandoDireita) AndarDireita();
        else if (andandoEsquerda) AndarEsquerda();
    }

    public void AndarDireita(){
        Vector3 posicao = this.transform.position;
        posicao.x += velocidade;
        this.transform.position = posicao;
        // Animação
        this.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void AndarEsquerda(){
        Vector3 posicao = this.transform.position;
        posicao.x -= velocidade;
        this.transform.position = posicao;
        // Animação
        this.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void VerificaMorte(){
        Vector3 posicaoAtual = this.transform.position;
        if(posicaoAtual.y < -10f) this.isDead = true;
        if(this.isDead){
            this.transform.position = this.posicaoInicial;
            this.isDead = false;
        }
    }
}
