using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BasicInventoryData<T, TObject> where TObject : BasicDataInformation<T> {
	//											   index, dataID
	[SerializeField] private SerializableDictionary<uint, uint> datas;

	abstract public string GetDirectory();

	public void Add(uint index, uint dataID) {
		datas[index] = dataID;
	}
	public void Remove(uint index) {
		datas.Remove(index);
	}

	public bool HasIndex(uint index) => datas.ContainsKey(index);
	public bool HasData(uint dataID) => datas.ContainsValue(dataID);

	public Dictionary<uint, T> GetDatas() {
		Dictionary<uint, T> result = new();

		TObject[] loadDatas = Resources.LoadAll<TObject>(GetDirectory());

		Dictionary<uint, T> datasList = new();
		foreach (TObject item in loadDatas) {
			var dataID = Convert.ToUInt32(item.name);
			
			datasList.Add(dataID, item.Data);
		}

		foreach (var item in datas) {
			result[item.Key] = datasList[item.Value];
		}

		return result;
	}
}