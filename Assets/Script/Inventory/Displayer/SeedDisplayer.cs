using UnityEngine;

public class SeedDisplayer : BasicDisplayer<SeedData> {
	[SerializeField] private CostDisplayer costDisplayer;

	private void Start() {
		costDisplayer.SetMaxCost(5, transform);
	}

	[ContextMenu("Cost")]
	public void Cost() {
		costDisplayer.SetCost(3);
	}
}
