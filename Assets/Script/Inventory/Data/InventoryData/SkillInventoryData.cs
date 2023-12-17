using System.IO;

[System.Serializable]
public class SkillInventoryData : BasicInventoryData<SkillData, SkillDataInformation> {
	public override string GetDirectory() => Path.Combine($"SkillData");
}