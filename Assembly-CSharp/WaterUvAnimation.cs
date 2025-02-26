using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public class WaterUvAnimation : MonoBehaviour
{
	// Token: 0x06001423 RID: 5155 RVA: 0x0007E196 File Offset: 0x0007C596
	private void Awake()
	{
		this.mat = base.GetComponent<Renderer>().materials[this.MaterialNomber];
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0007E1B0 File Offset: 0x0007C5B0
	private void Update()
	{
		if (this.IsReverse)
		{
			this.offset -= Time.deltaTime * this.Speed;
			if (this.offset < 0f)
			{
				this.offset = 1f;
			}
		}
		else
		{
			this.offset += Time.deltaTime * this.Speed;
			if (this.offset > 1f)
			{
				this.offset = 0f;
			}
		}
		Vector2 value = new Vector2(0f, this.offset);
		this.mat.SetTextureOffset("_BumpMap", value);
		this.mat.SetFloat("_OffsetYHeightMap", this.offset);
	}

	// Token: 0x040016E1 RID: 5857
	public bool IsReverse;

	// Token: 0x040016E2 RID: 5858
	public float Speed = 1f;

	// Token: 0x040016E3 RID: 5859
	public int MaterialNomber;

	// Token: 0x040016E4 RID: 5860
	private Material mat;

	// Token: 0x040016E5 RID: 5861
	private float deltaFps;

	// Token: 0x040016E6 RID: 5862
	private bool isVisible;

	// Token: 0x040016E7 RID: 5863
	private bool isCorutineStarted;

	// Token: 0x040016E8 RID: 5864
	private float offset;

	// Token: 0x040016E9 RID: 5865
	private float delta;
}
