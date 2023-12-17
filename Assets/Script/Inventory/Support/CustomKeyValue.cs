[System.Serializable]
public struct CustomKeyValue<K, V> {
	public K Key;
	public V Value;

	public CustomKeyValue(K key, V value) {
		Key = key;
		Value = value;
	}
}