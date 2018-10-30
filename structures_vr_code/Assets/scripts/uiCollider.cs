using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiCollider : MonoBehaviour {
	private Button button;
	private Toggle toggle;
	bool toggleBool = false;

	void Start() {
		button = GetComponent<Button>();
		toggle = GetComponent<Toggle>();
		if(toggle != null) {
			toggle.isOn = toggleBool;
		}

	}

	public void uiHit() {
		if(button != null) {
			button.onClick.Invoke();
		} else if(toggle != null) {
			if(toggleBool) {
				toggleBool = false;
			} else {
				toggleBool = true;
			}
			//toggle.onValueChanged.Invoke(toggleBool);
			toggle.isOn = toggleBool;
		}
    }
}
