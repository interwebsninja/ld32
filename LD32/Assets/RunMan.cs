﻿using UnityEngine;
using System.Collections;

public class RunMan : MonoBehaviour {
	public float walkingspeed = 5;
	public float runningspeed = 11;
	public float triggerDistance = 10;
	public float damageValue = 20;

	public bool stunned;
	public float stunTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (stunTime <= 0f) {
			stunned = false;
		} else {
			stunTime -= Time.deltaTime;
		}

		if (GetComponent<NavMeshAgent> ().remainingDistance < triggerDistance) {
			GetComponent<NavMeshAgent> ().speed = runningspeed;
		} else {
			GetComponent<NavMeshAgent> ().speed = walkingspeed;
		}

		if (stunned) {
			GetComponent<NavMeshAgent> ().speed = 0f;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			other.GetComponent<Health>().changeHealth(-damageValue);
			Vector3 forceDir = other.transform.position - transform.position;
			forceDir.Normalize();
			forceDir = 100 * forceDir;
			other.GetComponent<Player>().knockback += new Vector3(forceDir.x, 0.5f, forceDir.z);
		}
	}
}
