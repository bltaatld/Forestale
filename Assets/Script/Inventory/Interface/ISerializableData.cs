// Temporary Storage Interface
public interface ISerializableData {
	public string Serialize();
	public void Deserialize(string serialized);
}