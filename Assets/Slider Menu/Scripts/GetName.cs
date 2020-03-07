using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour {
	private string cardName;
	private string cartaActiva;
	private string textoNombre;
	private Transform parent;

	private int fontSize = 35;
	private int alto = 100;
	private int ancho = 550;
	private int posX = 45;
	private int posY = -290;
	private int posZ = 0;

	Color colorTexto;

	private Font fuente;


	SliderMenu sldrMenu;

	// Use this for initialization
	void Start () {
		fuente = (Font)Resources.Load("EnchLand", typeof(Font));
		colorTexto = new Color32(210,210,210,255); 
		parent = this.transform.parent;
		sldrMenu = (SliderMenu)GameObject.FindObjectOfType (typeof(SliderMenu));
		//nombre del parent
		cardName = parent.name;
		//Debug.Log ("name: " + cardName);
		Values vals = parent.gameObject.GetComponent<Values> ();
		textoNombre = vals.nombre;
		this.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().font = fuente;
		this.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().fontSize = fontSize;
		this.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().color = colorTexto;
		this.transform.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(ancho,alto);
		this.transform.gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (posX,posY,posZ);
		this.transform.gameObject.GetComponent<Text> ().text = textoNombre;
	}
	
	// Update is called once per frame
	void Update () {
		cartaActiva = sldrMenu.cartaActiva;
		//Debug.Log ("activa: " + cartaActiva + ", this: " + cardName);
		//Comparar el nombre de la carta activa con la del parent
		if (cardName == cartaActiva) {
			if(this.transform.gameObject.GetComponent<Text> ().text == ""){
				this.transform.gameObject.GetComponent<Text> ().text = textoNombre;
			}
			if(parent.gameObject.GetComponent<Button> ().interactable == false){
				parent.gameObject.GetComponent<Button> ().interactable = true;
			}
		} else {
			//Ocultar
			if(this.transform.gameObject.GetComponent<Text> ().text != ""){
				this.transform.gameObject.GetComponent<Text> ().text = "";
			}
			if(parent.gameObject.GetComponent<Button> ().interactable == true){
				parent.gameObject.GetComponent<Button> ().interactable = false;
			}
		}
	}
}
