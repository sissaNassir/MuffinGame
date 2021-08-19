using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float speed=5f;
    public float rotationSpeed = 30f;
    public Animator animator;

	// Use this for initialization
	void Start () {

        Debug.Log("Sono spawnato"+gameObject.name, gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal")!=0f || Input.GetAxis("Vertical")!=0f)
        {
            animator.SetBool("isMoving", true);
            Vector3 spostamento = Vector3.zero;
            Vector3 rotazione = Vector3.zero;

            rotazione.y = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            spostamento = transform.forward.normalized * (speed * Input.GetAxis("Vertical") * Time.deltaTime);

            transform.position += spostamento;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +  rotazione);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
		
	}

}
