using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookCoverHelper : MonoBehaviour {

	public RectTransform target;
	private RectTransform rTr;
	private Text text;
	private Image image;
	private Text targetText;
	private Image targetImage;

	// Use this for initialization
	void Start () {
		rTr = GetComponent<RectTransform> ();
		text = GetComponent<Text> ();
		image = GetComponent<Image> ();
		targetText = target.GetComponent<Text> ();
		targetImage = target.GetComponent<Image> ();

		target.position = rTr.position;
		target.localPosition = rTr.localPosition;
		target.rotation = rTr.rotation;
		target.localRotation = rTr.localRotation;
		target.localScale = rTr.localScale;
		target.anchoredPosition = rTr.anchoredPosition;
		target.anchoredPosition3D = rTr.anchoredPosition3D;
		target.anchorMax = rTr.anchorMax;
		target.anchorMin = rTr.anchorMin;
		target.offsetMax = rTr.offsetMax;
		target.offsetMin = rTr.offsetMin;
		target.pivot = rTr.pivot;
		target.sizeDelta = rTr.sizeDelta;

		if(text != null && targetText != null) {
			targetText.alignment = text.alignment;
			targetText.color = text.color;
			targetText.font = text.font;
			targetText.fontSize = text.fontSize;
			targetText.fontStyle = text.fontStyle;
			targetText.horizontalOverflow = text.horizontalOverflow;
			targetText.lineSpacing = text.lineSpacing;
			targetText.material = text.material;
			targetText.raycastTarget = text.raycastTarget;
			targetText.resizeTextForBestFit = text.resizeTextForBestFit;
			targetText.resizeTextMaxSize = text.resizeTextMaxSize;
			targetText.resizeTextMinSize = text.resizeTextMinSize;
			targetText.supportRichText = text.supportRichText;
			targetText.text = text.text;
			targetText.verticalOverflow = text.verticalOverflow;
		}

		if(image != null && targetImage != null) {
			targetImage.sprite = image.sprite;
			targetImage.color = image.color;
			targetImage.material = image.material;
			targetImage.raycastTarget = image.raycastTarget;
		}
	}
}
