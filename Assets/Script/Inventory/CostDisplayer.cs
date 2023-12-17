using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CostDisplayer {
	[SerializeField] private Image costObject;
	[SerializeField] private int costDistance;
	private Image[] costImages;

	public int CostCount => costCount;
	public int MaxCostCount => costImages.Length;
	[SerializeField] private int costCount = 0;

	private void DestroyCostImages() {
		if (costImages != null) {
			foreach (var image in costImages) {
				GameObject.Destroy(image.gameObject);
			}
		}
	}
	private void CostImagesFillClear() {
		if (costImages != null) {
			foreach (var image in costImages) {
				image.fillAmount = 0.0f;
			}
		}
	}

	public void SetCost(int cost) {
		costCount = cost;

		CostImagesFillClear();
		for (int i = 0; i < costImages.Length; i++) {
			if (cost <= 0) {
				break;
			}

			costImages[i].fillAmount = cost;
			cost--;
		}
	}
	public void SetMaxCost(int maxCost, Transform parent) {
		DestroyCostImages();

		costImages = new Image[maxCost];

		var halfCost = maxCost / 2.0f;
		var halfWidth = (costObject.rectTransform.rect.width / 2.0f);

		var mid = (costDistance * halfCost) - halfWidth;

		for (int i = 0; i < maxCost; i++) {
			var pos = (costDistance * i) - mid;

			var image = GameObject.Instantiate(costObject, parent);
			var position = Vector2.right * pos;

			image.rectTransform.localPosition = position;
			image.fillAmount = 0.0f;

			costImages[i] = image;
		}
	}
}