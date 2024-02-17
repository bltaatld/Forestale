[System.Serializable]
public class GameSaveData {
	/* Enter the data you want to save here */
	/* Test Data */
	public TestData TestData { get; set; }

	public GameSaveData() { 
		TestData = new TestData();
	}
}