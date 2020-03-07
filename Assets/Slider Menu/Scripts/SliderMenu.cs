using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour {
	public string cartaActiva;
	//Canvas Settings--------------------------------------------------------------------------------------------
	public Canvas YourCanvas;
	//ScrollBar Settings-----------------------------------------------------------------------------------------
	public Scrollbar HorizontalScrollBar;
	//Content Settings-------------------------------------------------------------------------------------------
	public RectTransform ScrollContent;
	public List<GameObject> Cards;
	public GameObject[] cardsResp;
	//Slides Settings--------------------------------------------------------------------------------------------
	public float Element_Width;
	public float Element_Height;
	public float Element_Margin;
	public float Element_Scale;
	//Slides Settings--------------------------------------------------------------------------------------------
	public float Transition_In;
	public float Transition_Out;
	//Other Variables--------------------------------------------------------------------------------------------
	private float n;
	private float ScrollSteps;
	//array para cambiar type
	private string[] arrayTipos = {"Bronce","Silver","Gold","Leader"};
	private int tipoActivo = 0;
	//array para cambiar type
	private string[] arrayFaccion = {"Neutral","N. Realms","Scoiatael","Monsters","Skellige","Nilfgaard"};
	private int factionActiva = 0;
	//array para cambiar rareza
	private string[] arrayRareza = {"Common","Rare","Epic","Legendary"};
	private int rarezaActiva = 0;
	//textos de filtros
	private GameObject[] txtTipo;
	private GameObject[] txtFaccion;
	private GameObject[] txtRareza;

	void Start () {
		setCardsInList ();
		SetSizeAndPosition ();
		txtTipo = GameObject.FindGameObjectsWithTag("txtType");
		txtFaccion = GameObject.FindGameObjectsWithTag("txtFaction");
		txtRareza = GameObject.FindGameObjectsWithTag("txtRareza");
		ChangeFaction (0);
		ChangeType (2);
		ChangeRarity (2);
	}

	void Update () {
		//Slides Scale, Slides Transition And Slides Color Transition-------------------------------
		for (int s=0; s < Cards.Count; s++) {
			for (int t=0; t < Cards.Count; t++) {
				if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
					if (t != s) {
						Cards [t].transform.localScale = Vector2.Lerp (Cards [t].transform.localScale, new Vector2 (1, 1), Transition_Out);
					}
					if (t == s) {
						Cards [s].transform.localScale = Vector2.Lerp (Cards [s].transform.localScale, new Vector2 (Element_Scale, Element_Scale), Transition_In);
						Cards [s].gameObject.transform.SetAsLastSibling ();
						cartaActiva = Cards [s].gameObject.name;
						//Debug.Log ("Card activa : " + Cards [s].gameObject.name);
						//Cards [s].GetComponent<Image>().color = new Color32(225,225,225,225);
					}
					/*else if(t<s){
						Cards [s].GetComponent<Image>().color = new Color32(225,225,225,225);
					}else if(t>s){
						Cards [s].GetComponent<Image>().color = new Color32(197,197,197,225);
					}*/
				} else if(Cards.Count == 1){
					cartaActiva = Cards [s].gameObject.name;
					Cards [s].transform.localScale = Vector2.Lerp (Cards [s].transform.localScale, new Vector2 (Element_Scale, Element_Scale), Transition_In);
					Cards [s].gameObject.transform.SetAsLastSibling ();
				}
			}
		}
	}

	public GameObject GetActiveCard(){
		if (Cards.Count > 0) {
			for (int z = 0; z < Cards.Count; z++) {
				if (Cards [z].name == cartaActiva) {
					return Cards [z];
				}
			}
			return null;
		} else {
			return null;
		}
	}

	public void ChangeFaction(int inc){
		factionActiva = factionActiva + inc;
		if(factionActiva > 5){
			factionActiva = 0;
		}
		if(factionActiva < 0){
			factionActiva = 5;
		}
		ChangeTextFaction ();
		FilterCards();
	}

	public void ChangeType(int inc){
		tipoActivo = tipoActivo + inc;
		if(tipoActivo > 3){
			tipoActivo = 0;
		}
		if(tipoActivo < 0){
			tipoActivo = 3;
		}
		ChangeTextTipo ();
		FilterCards();
	}

	public void ChangeRarity(int inc){
		rarezaActiva = rarezaActiva + inc;
		if(rarezaActiva > 3){
			rarezaActiva = 0;
		}
		if(rarezaActiva < 0){
			rarezaActiva = 3;
		}
		ChangeTextRareza ();
		FilterCards();
	}

	private void ChangeTextFaction(){
		txtFaccion[0].gameObject.GetComponent<Text>().text = arrayFaccion[factionActiva];
	}

	private void ChangeTextTipo(){
		txtTipo[0].gameObject.GetComponent<Text>().text = arrayTipos[tipoActivo];
	}

	private void ChangeTextRareza(){
		txtRareza[0].gameObject.GetComponent<Text>().text = arrayRareza[rarezaActiva];
	}

	public void FilterCards(){
		if (verifyAllFilters() == true) {
			//Vacio la lista de cartas
			Cards.Clear ();
			//insactivo las cartas respaldo
			foreach (GameObject card in cardsResp) {
				card.gameObject.SetActive (false);
			}
			//Busco los filtros activos y los agrego
			AddCardByFilters();
			SetSizeAndPosition ();
		}
	}

	private bool verifyAllFilters(){
		if (arrayTipos[tipoActivo] == "Type" & arrayFaccion[factionActiva] == "Faction" & arrayRareza[rarezaActiva] == "Rarity") { //Agregar todos faction rarity
			//Debug.Log ("Se resetea TODO");
			return false;
		} else {
			return true;
		}
	}

	public void AddCardByFilters(){
		foreach (GameObject card in cardsResp) {
			Values vals = card.gameObject.GetComponent<Values> ();
			if (vals.faction == arrayFaccion [factionActiva] & vals.tipo == arrayTipos [tipoActivo] & vals.rarity == arrayRareza [rarezaActiva]) {
				if (CheckCardInLIst (vals.idCard) == false) {
					card.gameObject.SetActive (true);
					Cards.Add (card); //Hay que chekear si la carta ya existe en la lista, mediante ID
				}
			}
		}
	}

	void SetSizeAndPosition(){
		//Auto Find Cards And Auto Set Size And Position Of Slides
		for (int b=0; b<Cards.Count; b++) {
			Cards[b].GetComponent<RectTransform>().sizeDelta = new Vector2(Element_Width,Element_Height);
			Cards[b].GetComponent<RectTransform>().localPosition = new Vector3((2*b+3)*(Element_Width)/2+(2*b+3)*Element_Margin,0,10);
		}
		//Set Size Of ScrollContent (Auto Set)
		ScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2((Cards.Count+2)*(Element_Width+2*Element_Margin),Element_Height);
		//Calculate ScrollSteps Value
		n = Cards.Count - 1;
		ScrollSteps = 1 / n;
	}

	void setCardsInList(){
		cardsResp = GameObject.FindGameObjectsWithTag("Card");
		foreach (GameObject card in cardsResp) {
			Cards.Add (card);
		}
	}

	public bool CheckCardInLIst(int idCarta){ //Verifica si ya existe la carta en la lista
		bool isExist = false;
		for (int y=0; y < Cards.Count; y++) {
			Values valorCard = Cards [y].gameObject.GetComponent<Values> ();
			if(valorCard.idCard == idCarta){
				isExist = true;
			}
		}
		return isExist;
	}

	/* Se ocupaba en el boton reset 
	public void SetActiveAllCards(){
		Cards.Clear ();
		foreach (GameObject card in cardsResp) {
			card.gameObject.SetActive (true);
			Cards.Add (card);
			SetSizeAndPosition ();
		}
	}
	*/

	/*
	public void SetActiveTF(bool isActive, int idCard){
		foreach (GameObject card in cardsResp) {
			Values vals = card.gameObject.GetComponent<Values>();
			if(vals.idCard == idCard){
				card.gameObject.SetActive (isActive);
			}
			SetSizeAndPosition ();
		}
	}

	public void RemoveCardById(int idcard){// REMOVER CARTA POR ID, LUEGO SETACTIVE FALSE AL OBJETO
		for (int y=0; y < Cards.Count; y++) {
			Values valorCard = Cards [y].gameObject.GetComponent<Values> ();
			if(valorCard.idCard == idcard){
				Cards.Remove (Cards [y]);
				SetActiveTF (false, idcard);
			}
		}
	}

	public void ChangeFaccion(string newfacc){
		canSearch = false;
		for (int s=0; s<Cards.Count; s++) {
			Cards [s].transform.localScale = new Vector2 (1, 1);
		}
		for (int s=0; s<Cards.Count; s++) {
			Values valorCard = Cards [s].gameObject.GetComponent<Values> ();
			if(valorCard.faction != newfacc){
				Cards.Remove (Cards [s]);
			}
		}
		canSearch = true;
		SetSizeAndPosition ();
	}

	public void ChangeType(string newtype){
		//Cambiar el color del boton presionado
		SetBtnColorType(true, newtype);

		canSearch = false;
		Values valorCard;
		List<int> idsRem = new List<int>();
		for (int y=0; y < Cards.Count; y++) {
			valorCard = Cards [y].gameObject.GetComponent<Values> ();
			if(valorCard.tipo != newtype){//Almacenar los id para luego removerlos todos
				idsRem.Add(valorCard.idCard);
			}
		}
		if (idsRem.Count > 0) { // Hay cartas para remover de la lista
			for (int x = 0; x < idsRem.Count; x++) {
				RemoveCardById (idsRem [x]);
			}
		}
		canSearch = true;
		SetSizeAndPosition ();
	}
	*/
}
