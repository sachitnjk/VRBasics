using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
	public FadeScreen fadeScreen;
	private bool transitionTriggered;

	private void Start()
	{
		transitionTriggered = false;
	}

	private void Update()
	{
		if(GameManager.instance.empDischarged  && !transitionTriggered) 
		{
			transitionTriggered = true;
			GoToScene(2);
		}
	}
	public void GoToScene(int sceneIndex)
	{
		StartCoroutine(StartGoToScene(sceneIndex));
	}

	private IEnumerator StartGoToScene(int sceneIndex) 
	{
		fadeScreen.FadeOut();
		yield return new WaitForSeconds(fadeScreen.fadeDuration);

		SceneManager.LoadScene(sceneIndex);

	}
}
