using StrategyGame.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class SplashController : MonoBehaviour {
	private SplashSettingsSo splashSettings;
	private GeneralSettingsSo generalSettings;
	void Start()
    {
		StartCoroutine(NavigateToMenu());
    }
	IEnumerator NavigateToMenu() {
		yield return new WaitForSeconds(splashSettings.SplashDuration);
		SceneManager.LoadSceneAsync(generalSettings.MenuSceneName);
	}

	[Inject]
	void Construct(SplashSettingsSo splashSettings, GeneralSettingsSo generalSettings) {
		this.splashSettings = splashSettings;
		this.generalSettings = generalSettings;
	}
}
