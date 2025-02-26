using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000640 RID: 1600
public class TestSwitchBar : MonoBehaviour
{
	// Token: 0x06002602 RID: 9730 RVA: 0x000D87DF File Offset: 0x000D6BDF
	private void Start()
	{
		this.InstantiateBar(this.currentPrefab);
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x000D87ED File Offset: 0x000D6BED
	public void SetRandomBar()
	{
		this.InstantiateBar(UnityEngine.Random.Range(0, this.progressBarPrefabs.Length));
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x000D8804 File Offset: 0x000D6C04
	public void ShiftBar(int shift)
	{
		int num = this.currentPrefab + shift;
		if (num >= this.progressBarPrefabs.Length)
		{
			this.InstantiateBar(0);
		}
		if (num < 0)
		{
			this.InstantiateBar(this.progressBarPrefabs.Length - 1);
		}
		else
		{
			this.InstantiateBar(num);
		}
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000D8854 File Offset: 0x000D6C54
	private void InstantiateBar(int num)
	{
		if (num < 0 || num >= this.progressBarPrefabs.Length)
		{
			return;
		}
		this.currentPrefab = num;
		if (this.bar != null)
		{
			UnityEngine.Object.Destroy(this.bar.gameObject);
		}
		this.bar = UnityEngine.Object.Instantiate<ProgressBarPro>(this.progressBarPrefabs[num], this.prefabParent);
		this.bar.gameObject.SetActive(false);
		this.bar.transform.localScale = Vector3.one;
		this.bar.SetValue(this.currentValue, false);
		this.bar.gameObject.SetActive(true);
		this.prefabName.text = this.progressBarPrefabs[this.currentPrefab].gameObject.name;
		base.Invoke("EnableBar", 0.01f);
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000D8933 File Offset: 0x000D6D33
	private void EnableBar()
	{
		if (this.bar != null)
		{
			this.bar.gameObject.SetActive(true);
		}
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x000D8957 File Offset: 0x000D6D57
	public void SetValue(float value)
	{
		this.currentValue = value;
		if (this.bar != null)
		{
			this.bar.SetValue(value, false);
		}
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x000D897E File Offset: 0x000D6D7E
	public void SetBarColor(Color color)
	{
		if (this.bar != null)
		{
			this.bar.SetBarColor(color);
		}
	}

	// Token: 0x040025B7 RID: 9655
	[SerializeField]
	private ProgressBarPro[] progressBarPrefabs;

	// Token: 0x040025B8 RID: 9656
	[SerializeField]
	private Transform prefabParent;

	// Token: 0x040025B9 RID: 9657
	[SerializeField]
	private int currentPrefab;

	// Token: 0x040025BA RID: 9658
	[SerializeField]
	private Text prefabName;

	// Token: 0x040025BB RID: 9659
	private ProgressBarPro bar;

	// Token: 0x040025BC RID: 9660
	private float currentValue = 1f;
}
