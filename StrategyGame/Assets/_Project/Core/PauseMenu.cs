using StrategyGame.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
	private GeneralSettingsSo generalSettings;

	private bool pausePanelEnabled = false;

    public void MainMenu() {
        SceneManager.LoadScene(generalSettings.MenuSceneName);
    }
    public void CloseGame() {
        Application.Quit();
    }
    public void Cancel() {
        SwitchPausePanelState();
	}
   
    void Update()
    {
        if (GameInput.Instance.GetEscButton()) {
            SwitchPausePanelState();
		}
    }
    private void SwitchPausePanelState() {
		pausePanelEnabled = !pausePanelEnabled;
		ChangePausePanelState(pausePanelEnabled);
	}
    private void ChangePausePanelState(bool state) {
		pausePanel.SetActive(state);
	}
	[Inject]
	void Construct(GeneralSettingsSo generalSettings) {
		this.generalSettings = generalSettings;
	}
}
