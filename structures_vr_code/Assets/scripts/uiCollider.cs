using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiCollider : MonoBehaviour {
	private Button button;

	void Start() {
		button = GetComponent<Button>();
	}

	public void uiHit() {
		button.onClick.Invoke();
    }

}
