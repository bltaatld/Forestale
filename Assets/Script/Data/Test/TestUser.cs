using UnityEngine;

public class TestUser : MonoBehaviour {
	private void Awake() {
		GameDataManager.Instance.OnGameDataSaved += DataSaved;
		GameDataManager.Instance.OnGameDataLoaded += DataLoaded;

		GameDataManager.Instance.Load();
		m_data = GameDataManager.Instance.Data.TestData;
	}
	[SerializeField] private TestData m_data;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			GameDataManager.Instance.Save();
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			GameDataManager.Instance.Load();
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			m_data.test--;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			m_data.test++;
		}
 	}

	private void VisualUpdate() {
		Debug.Log($"{GameDataManager.Instance.Data.TestData.test}");
	}

	private void DataLoaded() {
		Debug.Log("Load!");

		VisualUpdate();
	}

	private void DataSaved() {
		Debug.Log("Save!");
	}
}