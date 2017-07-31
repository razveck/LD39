using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject instructionsPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Instructions() {
		mainPanel.SetActive(false);
		instructionsPanel.SetActive(true);
	}

	public void StartGame() {
		SceneManager.LoadScene(1);
	}
}
