//-*- java -*-
using UnityEngine; 

public class ManaManager : MonoBehaviour{
    public float regenRate; 
    public float maxMana; 
    private float curMana; 
    
    public GameObject manaBarUI; 
    private SpriteRenderer manaBarDisplay; 
    private Vector3 manaBarScale; 
    private Vector3 curScale; 

    public void Initialize(){
	manaBarDisplay = manaBarUI.GetComponent<SpriteRenderer>(); 
	manaBarScale = manaBarUI.transform.localScale;

	curMana = maxMana; 
    }

    public bool CheckManaCost(float manaCost){
	return manaCost <= curMana; 
    }
    public void SubtractCost(float manaCost){
	curMana -= manaCost; 
    }

    public float GetCurMana(){
	return curMana; 
    }
    void FixedUpdate(){
	if(manaBarDisplay!=null){
	    curScale = manaBarScale; 
	    curScale.x = manaBarScale.x * (curMana/maxMana); 

	    manaBarUI.transform.localScale = curScale; 

	if(curMana < maxMana)
	    curMana += regenRate; 
	}
    }

}
