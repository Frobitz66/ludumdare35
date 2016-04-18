using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonState : MonoBehaviour {

	public Sprite DefaultSprite;
	public Sprite HoverSprite;
	private Button ourButton;

	// Use this for initialization
	void Start () {
		ourButton = GetComponent<Button> ();
		if (DefaultSprite == null) {
			DefaultSprite = ourButton.image.sprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Hover(){
		if (HoverSprite == null)
			return;
		
		ourButton.image.sprite = HoverSprite;
	}

	public void UnHover(){
		ourButton.image.sprite = DefaultSprite;
	}

	public virtual void Click(){
    }
}



