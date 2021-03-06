﻿//-*-java-*-
using UnityEngine;
using System.Collections;

/*
Manages health of user
Manages synchronzation of damage inflicted on player 
 */

public class PlayerHealth : MonoBehaviour{
    //External References : 


    public float health; 
    public float regenRate; 
    [HideInInspector]
    public float origHealth; 

    public PlayerNetwork pNet; 

    [HideInInspector] 
    public GameObject hurtUI; 
    private SpriteRenderer redHurtDisplay; 
    private Color baseHurtColor; 
    private float baseHurtAlpha = 0; 
    private float time; 
    private static float TwoPi = Mathf.PI * 2;

    private bool isDead = false; 



    /*
    public SpriteRenderer healthBar; 
    private Vector3 healthScale; // The local scale of the health bar initially (with full health).
    */

    public void Initialize(){
	//hurtUI.SetActive(true); 
	redHurtDisplay = hurtUI.GetComponent<SpriteRenderer>();
	baseHurtColor = redHurtDisplay.color; 
	origHealth = health; 

	/*
	Color color = redHurtDisplay.color; 
	color += new Color(0,0,0,1); 
	redHurtDisplay.color = color; 
	*/
    }

    public void ResetHealth(){
        health = origHealth;
        baseHurtAlpha = 0; 
	isDead = false; 
    }

    public void DecreaseHealth(float damage){
 	health -= damage; 
	baseHurtAlpha = (origHealth-health)/origHealth; 

	if(health <= 0 && !isDead){
	    pNet.SynchDeath(); 
	    isDead = true; 
	}
    }

    public void FixedUpdate(){
	if(health<origHealth){
	    health += regenRate; 
	    baseHurtAlpha = (origHealth-health)/origHealth; 
	}
	if(redHurtDisplay!=null){ //wasteful 
	    baseHurtColor.a = baseHurtAlpha + 
		(baseHurtAlpha/3.0f) * Mathf.Abs(Mathf.Sin(time)); 
	    redHurtDisplay.color = baseHurtColor; 
	}
	time += .035f; 
	time = time%TwoPi; 
    }
    /*
    public void Update(){
	if(redHurtDisplay!=null){ //wasteful 
	    baseHurtColor.a = baseHurtAlpha + 
		(baseHurtAlpha/3.0f) * Mathf.Abs(Mathf.Sin(time)); 
	    redHurtDisplay.color = baseHurtColor; 
	}
	time += .035f; 
	time = time%TwoPi; 
    }
    */
}
