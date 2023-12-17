using System.IO;

[System.Serializable]
public class ItemInventoryData : BasicInventoryData<ItemData, ItemDataInformation> {
	public override string GetDirectory() => Path.Combine($"ItemData");
}