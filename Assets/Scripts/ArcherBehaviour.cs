﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehaviour : PlayerBehaviour {

	private float specialValue = 16f;

	void Start () {
		stateAttack = 4;
		stateSpecial = 4;
		attackValue = 16f;
		defense = 17f;
	}

	public override void SpecialCommand(List<GameObject> enemies){
		int i;
		enemiesAttacking = enemies;
		State = STATE.ONSPECIAL;
		Special = Special - specialValue;
		battleManager.archerSpecialSlider.value = Special;
		anim.SetInteger ("State", 2);
	}

	public override string Name{
		get{ return "Arqueiro";}
		set{}
	}

	public void FinishAttack(){
		GameObject attackObject;
		int i;
		float attack;
		EnemyBehaviour enemy;
		Vector3 vec = new Vector3 (enemiesAttacking [0].transform.position.x, enemiesAttacking [0].transform.position.y + 0.5f, enemiesAttacking [0].transform.position.z - 1f);
		attackObject = Instantiate (prefabAttack, vec, Quaternion.identity);
		attackObject.GetComponent<Animator> ().SetInteger ("State", stateAttack);
		enemy = enemiesAttacking [0].GetComponent<EnemyBehaviour> ();
		attack = enemy.Life - attackValue + enemy.Defense;
		enemy.Life = attack;
		enemy.IsSelected = false;
		if (enemy.Life <= 0)
			battleManager.DestroyEnemy (enemy);
		else
			print ("Vida enemy = " + enemy.Life);
		enemiesAttacking.Clear ();
	}

	public override void FinishSpecial(){
		int i;
		EnemyBehaviour enemy;
		GameObject attackObject;
		for (i = 0; i < enemiesAttacking.Count; i++) {
			enemy = enemiesAttacking [i].GetComponent<EnemyBehaviour> ();
			//Vector3 vec = new Vector3
			Vector3 vec = new Vector3 (enemiesAttacking [i].transform.position.x, enemiesAttacking [i].transform.position.y + 0.5f, enemiesAttacking [i].transform.position.z - 1f);
			attackObject = Instantiate (prefabAttack, vec, Quaternion.identity);
			attackObject.GetComponent<Animator> ().SetInteger ("State", stateSpecial);
			float attack = enemy.Life - attackValue + enemy.Defense - specialValue;
			enemy.Life = attack;
			if (enemy.Life <= 0)
				battleManager.DestroyEnemy (enemy);
			else
				print ("enemy life special archer = " + enemy.Life);
		}
	}
}
