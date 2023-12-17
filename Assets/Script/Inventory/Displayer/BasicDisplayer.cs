using UnityEngine;
using UnityEngine.UI;

public class BasicDisplayer<T> : MonoBehaviour {
	[SerializeField] protected T nowData;
	[SerializeField] protected Image iconImage;
	[SerializeField] protected Text nameText;
	[SerializeField] protected Text explanationText;
}