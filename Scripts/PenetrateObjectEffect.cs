//-*-java-*-
using UnityEngine;

/*
Instantiate a stuck bolt whenenver a hit occurs
But use another collider at first 
 */

public class PenetrateObjectEffect : AbObjectEffect{   
    public int knockback; //220
    public float damage; //100 

    //enable these when collision occurs 
    public GameObject hitPiece; 

    private bool spawned = false; 

    //pos = position of collision 
    //dir = direction of projectile 
    override public void EffectGO(GameObject gObj, Vector2 pos, Vector2 dir){

	if(gObj.tag == "Player"){
	    PlayerNetwork pNet = gObj.GetComponent<PlayerNetwork>(); 

		pNet.photonView.RPC("ApplyForceDamage",PhotonTargets.All,
				    new object[]{dir*knockback,damage}); 
		
	}
	//check lerp - check platform is not moving - supporting those collision
	//would involve tracking objects over network 

	else if(gObj.tag == "DestEnviro" && !spawned && gObj.GetComponent<MoveBetween>()==null && gObj.GetComponent<PrefabID>()==null){
	    this.transform.parent = gObj.transform; 

	    SpawnHitPiece(pos,dir); 
	    spawned = true;
	}

    }

    //NOTE: DOES NOT IDENTIFY GO as PARENT, so 
    //if the terrain was destructible and physicalized
    //this system would not work! - issue of identifying
    //a gameobject between clients on network 
    //[RPC]
    public void SpawnHitPiece(Vector2 pos, Vector2 dir){
	GameObject controlScripts = GameObject.Find("ControlScripts"); 
	SynchedObjectCreator creator = controlScripts.GetComponent<SynchedObjectCreator>(); 

	creator.photonView.RPC("Create",PhotonTargets.All,
	       new object[]{pos,
	       Quaternion.Euler(0,0,Mathf.Rad2Deg*Mathf.Atan2(dir.y,dir.x)),
	       hitPiece.GetComponent<PrefabID>().GetPrefabID()}); 
    }
    /*
    override public void ExplosionAt(Vector2 position, float rotation){
	Quaternion quaternion = Quaternion.Euler(0f, 0f, rotation); 
	Instantiate(explosion, position, quaternion); 
    }
    */
}
