using UnityEngine;
using Valve.VR;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

/* This class handles opening and closing the keyboard */
/* The majority of this code was copied from the OpenVR github, but it was adapted to work with our project */
/* This script should be attatched to any UI text input field that would need keyboard input */
public class keyboardOverlay : MonoBehaviour, ISelectHandler
{
	private TMP_InputField inputField;
	private bool minimalMode = false;

	static bool keyboardShowing;
	string text = "";
	static keyboardOverlay activeKeyboard = null;

	
	// Use this for initialization
	void Start ()
	{
		inputField = GetComponent<TMP_InputField>();
		if(inputField == null) {
			Debug.LogError("Object needs a Text Mesh Pro input field!");
		}
	}

	void OnEnable()
	{
        SteamVR_Events.System(EVREventType.VREvent_KeyboardCharInput).Listen(OnKeyboard);
        SteamVR_Events.System(EVREventType.VREvent_KeyboardClosed).Listen(OnKeyboardClosed);
    }

    private void OnKeyboard(VREvent_t args)
    {
        if (activeKeyboard != this)
			return;
		VREvent_Keyboard_t keyboard = args.data.keyboard;
		byte[] inputBytes = new byte[] { keyboard.cNewInput0, keyboard.cNewInput1, keyboard.cNewInput2, keyboard.cNewInput3, keyboard.cNewInput4, keyboard.cNewInput5, keyboard.cNewInput6, keyboard.cNewInput7 };
		int len = 0;
		for (; inputBytes[len] != 0 && len < 7; len++) ;
		string input = System.Text.Encoding.UTF8.GetString(inputBytes, 0, len);

		if (minimalMode)
		{
			if (input == "\b")
			{
				if (text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			else if (input == "\x1b")
			{
				// Close the keyboard
				var vr = SteamVR.instance;
				vr.overlay.HideKeyboard();
				keyboardShowing = false;

			}
			else
			{
				text += input;
			}
			inputField.text = text;
		}
		else
		{
			System.Text.StringBuilder textBuilder = new System.Text.StringBuilder(1024);
			uint size = SteamVR.instance.overlay.GetKeyboardText(textBuilder, 1024);
			text = textBuilder.ToString();
            inputField.text = text;
		}
	}

    private void OnKeyboardClosed(VREvent_t args)
    {
		inputField.DeactivateInputField();
        if (activeKeyboard != this)
			return;
		keyboardShowing = false;
		activeKeyboard = null;
    }

	/// <summary>
    /// Opens up the keyboard overlay when the input field is selected
    /// </summary>
	public void OnSelect(BaseEventData eventData)
	{
		StartCoroutine(OpenKeyboard());
 
	}

	IEnumerator OpenKeyboard()
    {
        yield return new WaitForSeconds(.25f);
		if(!keyboardShowing)
		{
			keyboardShowing = true;
			activeKeyboard = this;
            SteamVR.instance.overlay.ShowKeyboard(1, 0, "Description", 256, inputField.text, minimalMode, 0);
		}
    }

}