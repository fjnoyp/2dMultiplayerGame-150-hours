//-*-java-*-
using UnityEngine;

public class ExplosionObjectEffect : AbObjectEffect{   
    public int knockback; //220
    public float damage; //100 

    public PolygonCollider2D ourPolygon; 

    public AbObjectVisualEffect objectVisualEffect; 
    
    //pos = position of collision 
    //dir = direction of projectile 
    override public void EffectGO(GameObject gObj, Vector2 pos, Vector2 dir){
	if(gObj.tag == "Player"){
	    PlayerNetwork pNet = gObj.GetComponent<PlayerNetwork>(); 

	    /*
	    //Only inflict collisioneffects if the player the bullet hits 
	    //isn't ours 
	    if( pNet.photonView.isMine && 
		!projectileManager.photonView.isMine){
	    */
		pNet.photonView.RPC("ApplyForceDamage",PhotonTargets.All,
				    new object[]{dir*knockback,damage}); 
	}
	else if(gObj.tag == "DestEnviro"){
	    gObj.GetComponent<SDestructable>().ExplosionCut(ourPolygon.points,(Vector2)this.transform.position); 
	    objectVisualEffect.StartEffect(gObj,pos,dir); 
	}

	//this.ExplosionAt(pos,Mathf.Tan(dir.y/dir.x)); 

    }
}

//the client where the projectile exploded will manage all explosion details
//the other players who receive the explosion rpc notification will only check 
//if the explosion managed to effect their respective players 

// - this will hopefully avoid any synchronization issues 
