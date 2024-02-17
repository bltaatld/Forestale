using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour {
	public static GameDataManager Instance {
		get {
			if (m_instance == null) {
				GameDataManager manager = FindObjectOfType<GameDataManager>(true);
				if (manager == null) {
					var gameObject = new GameObject {
						name = $"Data Manager Instance"
					};

					DontDestroyOnLoad(gameObject);

					manager = gameObject.AddComponent<GameDataManager>();
				}

				m_instance = manager;
			}

			return m_instance;
		}
	}
	private static GameDataManager m_instance;

	public GameSaveData Data => m_data;
	private GameSaveData m_data;

	public event Action OnGameDataLoaded;
	public event Action OnGameDataSaved;

	private string GameDataPath => Path.Combine(Application.persistentDataPath, "GameData.json");

	public void Load() {
		string path = GameDataPath;

		string json = string.Empty;
		if (File.Exists(path)) {
			json = File.ReadAllText(path);
		}

		if (string.IsNullOrEmpty(json)) {
			m_data = new GameSaveData();
		} else {
			try {
				m_data = JsonConvert.DeserializeObject<GameSaveData>(json);
			} catch (Exception e) {
				Debug.LogError(e.Message);

				m_data = new GameSaveData();
			}
		}

		OnGameDataLoaded?.Invoke();
	}
	public void Save() {
		string path = GameDataPath;

		string json = JsonConvert.SerializeObject(m_data, Formatting.Indented);

		File.WriteAllText(path, json);

		this.Load();

		OnGameDataSaved?.Invoke();
	}
}