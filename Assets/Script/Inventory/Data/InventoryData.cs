using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData : ISerializableData {
	public ItemInventoryData Item;
	public SkillInventoryData Skill;
	public SeedInventoryData Seed;

	public string Serialize() => JsonUtility.ToJson(this, true);
	public void Deserialize(string serialized) {
		InventoryData inventory = JsonUtility.FromJson<InventoryData>(serialized);

		Item = inventory.Item;
		Skill = inventory.Skill;
		Seed = inventory.Seed;
	}
}