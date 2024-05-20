using StrategyGame.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class MenuController : MonoBehaviour
{
    private GeneralSettingsSo _generalSettings;

    public void StartClicked() {
		SceneManager.LoadSceneAsync(_generalSettings.GameSceneName);
	}
	public void ExitClicked() {
		Application.Quit();
	}



	[Inject]
	void Construct(GeneralSettingsSo generalSettings) {
		this._generalSettings = generalSettings;
	}
}
