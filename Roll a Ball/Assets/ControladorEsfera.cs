using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorEsfera : MonoBehaviour {


	public float speed; //Velocitat a la pilota
	private int count; //comptador de punts

	public Text text; //Text on mostra els punts
	public Text winText; //Text on mostra missatge de victoria

	void Start(){
		count = 0;
		winText.text = "";
		updateCounter();
	}

	//Update is called once per frame
	//Utilitzem Fixed per les Physics
	void FixedUpdate () {

		//Moviments horitzontal i vertical
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		//Creem un nou Vector:
		// # Vector2 = Game 2D
		// # Vector3 = Game 3D
		Vector3 direction = new Vector3(horizontal, 0, vertical);

		//Donar-li els anteriors moviments a la pilota.
		//Depenen del tipus de moviment, cada cop la pilota anira més rapid
		GetComponent<Rigidbody>().AddForce(direction * speed);
	}

	/// <sumary>
	///OnTriggerEnter is called when the Collider other enters the trigger.
	/// </sumary>
	/// <param name="other"> The other collider involved in this collision.</param>

	//Si l'esfera impacta contra un objecte que tingui com etiqueta
	//"pickup", aquest objecte desapareix.
	//Per cada bloc destruit, suma un punt.
	 void OnTriggerEnter (Collider other)
    {
		//PD: Per que aixo funcioni s'ha danar al prefab de Pickup i assegurar-te de que te l'etiqueta Pickup
		//i en Collider esta activada l'opcio isTrigger
        if (other.gameObject.CompareTag("Pickup")){
            other.gameObject.SetActive(false);
            count = count + 1;
            updateCounter();
        }
    }

	//Actualitzar el comptador de punts
	void updateCounter(){
		text.text = "Puntos: " + count;

		//Comprovar nombre actual de Pickups que estroven encara en el joc
		//Quan s'hagin eliminat tots, mostra un missatge de victoria.
		int numPickUps = GameObject.FindGameObjectsWithTag("Pickup").Length;
		if(numPickUps == 1){
			winText.text = "Has guanyat!";
		}
	}
}
