﻿/* Universidade de Sao Paulo - ICMC - Outubro de 2017
   Autores: Klinsmann Hengles, Felipe Torres e Gabriel Carvalho
   Script desenvolvido para o jogo Infinite Dungeon
   Descrição: Script que descreve o comportamento do personagem*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public enum STATE {NOTSELECTED, SELECTED, MOVING, WAITATTACK, ATTACKING, SPECIAL, FROZEN} // possiveis estados do personagem

	private Vector2 currentPosition; // posicao inicial do personagem
	private Vector2 finalPosition; // posicao para a qual o personagem vai se mover
	private STATE state; // determina o estado do personagem

	private float speed; // velocidade da movimentacao do personagem
	private float attackValue = 10f;
	private float life = 100f;
	private float defense = 5f;
	private int level;
	private float secondary = 100f;
	private int actionsOnTurn = 0;

	public GameObject battleUI;			// GameObject that contains all UI Battle components
	public GameObject attackButton;		
	public GameObject specialButton;

	public GameManager gameManager;

	private bool isColliding = false;
	private Collider2D collider;

	public Animator anim;

	public Vector2 FinalPosition {
		get {
			return finalPosition;
		}
		set { 
			finalPosition = value; 
		}
	}

	public STATE State {
		get {
			return state;
		}
		set { 
			state = value; 
		}
	}

	public float Life{
		get{
			return life;
		}
		set{
			life = value;
		}
	}

	public float Defense{
		get{
			return defense;
		}
		set{
			defense = value;
		}
	}

	public bool IsColliding{
		get{
			return isColliding;
		}
		set{
			isColliding = value;
		}
	}

	public Collider2D Collider{
		get{
			return collider;
		}
	}

	// Use this for initialization
	void Start () {
		currentPosition = gameObject.transform.position; // pega a posicao inicial do personagem
		//anim = gameObject.GetComponent<Animator>();
		/*battleUI = GameObject.Find ("UIBattle");
		attackButton = GameObject.Find ("ButtonAttack");
		specialButton = GameObject.Find ("ButtonSpecial");
		*/
		//anim = gameObject.GetComponent<Animator>();
		state = STATE.NOTSELECTED;
	}

	void Update () {
		if (state == STATE.MOVING) {
			print ("moving");
			if (finalPosition.x != transform.position.x || finalPosition.y != transform.position.y) {
				speed = 2f;
				transform.position = Vector3.MoveTowards (transform.position, finalPosition, speed * Time.deltaTime);
			} else {
				state = STATE.FROZEN;
				anim.SetInteger ("State", 0);
			}
		} else
			print ("state = " + state.ToString ());

	}

	public void OnClickAttack(){
		
		state = STATE.WAITATTACK;
		battleUI.SetActive(false);
	}

	public void OnClickSpecial(){
		state = STATE.SPECIAL;
		battleUI.SetActive (false);
	}

	// Funcao que detecta a colisao entre um objeto com colisor e o mouse
	void OnMouseDown() {
		//if (gameManager.Turn == GameManager.TURN.CHARACTER) {	// Se o turno é do character
		if (gameManager.Turn == GameManager.TURN.PLAYERTURN && state == STATE.NOTSELECTED) {
			battleUI.transform.position = gameObject.transform.position;
			battleUI.SetActive (true);
			state = STATE.SELECTED;

			currentPosition = transform.position;
		}
		//}
	}

	public void Attack(EnemyBehaviour enemy){
		state = STATE.ATTACKING;
		anim.SetInteger ("State", 3);
		float attack = enemy.Life - attackValue + enemy.Defense;
		enemy.Life = attack;
		print ("Vida enemy = " + enemy.Life);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "EnemyAttack") {
			isColliding = true;
			collider = coll;
		}
	}

	public void SetNotSelected(){
		state = STATE.FROZEN;
		anim.SetInteger ("State", 0);
		print ("Not selected");
	}
		

}