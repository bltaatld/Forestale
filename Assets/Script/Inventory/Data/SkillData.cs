using UnityEngine;
using UnityEngine.Video;

public enum SkillType {
	CloseRange,
	LongRange,
	Support
}

[System.Serializable]
public class SkillData : BasicData {
	[Space(10.0f)]
	public SkillType Type;
	public int Damage;
	public int SpiritualDamage;
	public int TotalPoint;
	public int Point;
	public float Critical;
	public float CoolTime;
	public VideoClip Clip;
}