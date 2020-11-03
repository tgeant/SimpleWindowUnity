using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciator : MonoBehaviour {

	public GameObject prefabWindow;

	public void CreateWindow(){
		GameObject go = Instantiate(prefabWindow, Vector3.zero, Quaternion.identity, transform);
	}
}
