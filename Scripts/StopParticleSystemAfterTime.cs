//-*- java -*-
using UnityEngine;

public class StopParticleSystemAfterTime : MonoBehaviour{
       public float time; 
       public double startTime; 
       public ParticleSystem pSystem; 

       void Start(){
       startTime = Time.time; 
}
	void Update(){
	if(Time.time > startTime+time){
pSystem.Stop(); 
}
}

}
