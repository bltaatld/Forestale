using UnityEngine;

public enum SeedType {
	Passive,
	Active
}

[System.Serializable]
public class SeedData : BasicData {
	[Space(10.0f)]
	public SeedType Type;
	public int CostRequired;
}