using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
	public bool fadeOnStart;
	public float fadeDuration = 2f;
	public Color fadeColor;

	private float timer;
	private Renderer rend;
	private Color newColor;
	private Color finalColor;
	private void Start()
	{
		rend = GetComponent<Renderer>();
		if(fadeOnStart ) 
		{
			FadeIn();
		}
	}

	public void FadeIn()
	{
		Fade(1, 0);
	}
	public void FadeOut()
	{
		Fade(0, 1);
	}

	public void Fade(float alphaIn, float alphaOut)
	{
		StartCoroutine(FadeStart(alphaIn, alphaOut));
	}

	public IEnumerator FadeStart(float alphaIn, float alphaOut)
	{
		timer = 0f;
		while (timer < fadeDuration) 
		{
			newColor = fadeColor;
			newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);

			rend.material.SetColor("_Color", newColor);

			timer += Time.deltaTime;
			yield return null;
		}

		finalColor = fadeColor;
		finalColor.a = alphaOut;
		rend.material.SetColor("_Color", finalColor);
	}
}
