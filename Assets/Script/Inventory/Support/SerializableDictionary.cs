using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<Key, Value> : Dictionary<Key, Value>, ISerializationCallbackReceiver {
	[SerializeField] private List<CustomKeyValue<Key, Value>> datas;
	public void OnBeforeSerialize() {
		datas.Clear();

		foreach(KeyValuePair<Key, Value> kvp in this) {
			datas.Add(new CustomKeyValue<Key, Value>(kvp.Key, kvp.Value));
		}

	}

	public void OnAfterDeserialize() {
		this.Clear();

		var count = datas.Count;
		for (int i = 0; i < count; i++) {
			this.Add(datas[i].Key, datas[i].Value);
		}
	}
}