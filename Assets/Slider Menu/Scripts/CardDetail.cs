using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDetail : MonoBehaviour {
	public GameObject window;
	private GameObject cartaActiva;

	SliderMenu sldrMenu;

	//textos a modificar
	public GameObject txtNombre;
	public GameObject txtType;
	public GameObject txtFaction;
	public GameObject txtScrapCost;
	//public GameObject txtPower;
	public GameObject txtRarity;
	public GameObject txtLane;
	public GameObject txtMillCost;
	public GameObject txtEfecto;

	//Titulos a ocultar en tabs
	public GameObject titScrapCost;
	public GameObject titName;
	public GameObject titType;
	public GameObject titFaction;
	public GameObject titRarity;
	public GameObject titLane;
	//public GameObject titPower;
	public GameObject titMillcost;

	public GameObject btnInfo;

	//Sub card
	public GameObject btnSubCard;
	public GameObject image1;
	public GameObject imageOnly;
	public GameObject image2;

	public GameObject txtNomCard1;
	public GameObject txtAbiCard1;
	public GameObject txtNomCard2;
	public GameObject txtAbiCard2;
	public GameObject txtNomCardOnly;
	public GameObject txtAbiCardOnly;

	Values vals;

	// Use this for initialization
	void Start () {
		sldrMenu = (SliderMenu)GameObject.FindObjectOfType (typeof(SliderMenu));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowWindow(){
		if(window.gameObject.activeSelf == false){
			window.gameObject.SetActive (true);
			cartaActiva = sldrMenu.GetActiveCard();
			vals = cartaActiva.gameObject.GetComponent<Values> ();
			SetValues (vals);
			if (vals.subcards.Length > 0) {
				btnSubCard.gameObject.SetActive (true);
			}
		}
	}

	public void ActivarSubCartas(){
		HideShowInfo (false);
		if(vals.subcards.Length == 1){
			imageOnly.gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = vals.subcards[0];
			imageOnly.gameObject.SetActive (true);
			//Seteo textos nombre y abilidad
			txtNomCardOnly.SetActive(true);
			txtNomCardOnly.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.nomSubcard1;
			txtAbiCardOnly.SetActive (true);
			txtAbiCardOnly.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.abilitySubcard1;
		}else if(vals.subcards.Length == 2){
			image1.gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = vals.subcards[0];
			image1.gameObject.SetActive (true);
			txtNomCard1.SetActive(true);
			txtNomCard1.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.nomSubcard1;
			txtAbiCard1.SetActive(true);
			txtAbiCard1.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.abilitySubcard1;
			image2.gameObject.GetComponent<UnityEngine.UI.Image> ().sprite = vals.subcards[1];
			image2.gameObject.SetActive (true);
			txtNomCard2.SetActive(true);
			txtNomCard2.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.nomSubcard2;
			txtAbiCard2.SetActive(true);
			txtAbiCard2.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = vals.abilitySubcard2;
		}
	}

	public void OcultarSubCartas(){
		HideShowInfo (true);
		image1.gameObject.SetActive (false);
		image2.gameObject.SetActive (false);
		imageOnly.gameObject.SetActive (false);
		txtNomCard1.SetActive (false);
		txtAbiCard1.SetActive (false);
		txtNomCard2.SetActive (false);
		txtAbiCard2.SetActive (false);
		txtNomCardOnly.SetActive (false);
		txtAbiCardOnly.SetActive (false);
	}

	public void HideWindow(){
		if(window.gameObject.activeSelf == true){
			HideShowInfo (true);
			btnSubCard.gameObject.SetActive (false);
			window.gameObject.SetActive (false);
			image1.gameObject.SetActive (false);
			image2.gameObject.SetActive (false);
			imageOnly.gameObject.SetActive (false);
			txtNomCard1.SetActive (false);
			txtAbiCard1.SetActive (false);
			txtNomCard2.SetActive (false);
			txtAbiCard2.SetActive (false);
			txtNomCardOnly.SetActive (false);
			txtAbiCardOnly.SetActive (false);
		}
	}

	private void SetValues(Values valores){
		txtNombre.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.nombre;
		txtType.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.tipo;
		txtFaction.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.faction;
		txtScrapCost.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.scrapCost.ToString();
		//txtPower.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.fuerza.ToString();
		txtRarity.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.rarity;
		txtLane.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.lane;
		txtMillCost.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.millCost.ToString();
		txtEfecto.transform.gameObject.GetComponent<UnityEngine.UI.Text> ().text = valores.efecto;
	}

	private void HideShowInfo(bool isShow){
		txtNombre.gameObject.SetActive (isShow);
		txtType.gameObject.SetActive (isShow);
		txtFaction.gameObject.SetActive (isShow);
		txtScrapCost.gameObject.SetActive (isShow);
		//txtPower.gameObject.SetActive (isShow);
		txtRarity.gameObject.SetActive (isShow);
		txtLane.gameObject.SetActive (isShow);
		txtMillCost.gameObject.SetActive (isShow);
		txtEfecto.gameObject.SetActive (isShow);
		titScrapCost.gameObject.SetActive (isShow);
		titName.gameObject.SetActive (isShow);
		titType.gameObject.SetActive (isShow);
		titFaction.gameObject.SetActive (isShow);
		titRarity.gameObject.SetActive (isShow);
		titLane.gameObject.SetActive (isShow);
		//titPower.gameObject.SetActive (isShow);
		titMillcost.gameObject.SetActive (isShow);
	}
}
