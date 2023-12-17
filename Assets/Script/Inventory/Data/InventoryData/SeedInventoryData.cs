using System.IO;

[System.Serializable]
public class SeedInventoryData : BasicInventoryData<SeedData, SeedDataInformation> {
	public override string GetDirectory() => Path.Combine($"SeedData");
}