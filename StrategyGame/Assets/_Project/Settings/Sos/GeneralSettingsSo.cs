using UnityEngine;

[CreateAssetMenu(fileName = "GeneralSettingsSo", menuName = "Settings/GeneralSettingsSo", order = 1)]
public class GeneralSettingsSo : ScriptableObject {
	[SerializeField] private string menuSceneName;
	[SerializeField] private string gameSceneName;
	public string MenuSceneName => menuSceneName;
	public string GameSceneName => gameSceneName;
}
