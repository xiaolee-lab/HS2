using System;
using UnityEngine;

// Token: 0x02001159 RID: 4441
public class TexAnmPtn : MonoBehaviour
{
	// Token: 0x060092D0 RID: 37584 RVA: 0x003CE37C File Offset: 0x003CC77C
	private void Start()
	{
		this.rendererData = base.GetComponent<Renderer>();
		if (null == this.rendererData)
		{
			base.enabled = false;
		}
		this.ptnNum = this.UvNumX * this.UvNumY;
		this.separateTime = this.ChangeTime / this.ptnNum;
		this.uvSize = new Vector2(1f / (float)this.UvNumX, 1f / (float)this.UvNumY);
	}

	// Token: 0x060092D1 RID: 37585 RVA: 0x003CE3F8 File Offset: 0x003CC7F8
	private void Update()
	{
		this.passTime += (int)(Time.deltaTime * 1000f);
		while (this.passTime >= this.ChangeTime)
		{
			this.passTime -= this.ChangeTime;
		}
		int value = (this.ChangeTime != 0) ? (this.passTime % this.ChangeTime / this.separateTime) : 0;
		int num = Mathf.Clamp(value, 0, this.ptnNum);
		if (num == this.lastIndex)
		{
			return;
		}
		int num2 = num % this.UvNumX;
		int num3 = num / this.UvNumX;
		Vector2 value2 = new Vector2((float)num2 * this.uvSize.x, 1f - (float)num3 * this.uvSize.y);
		this.rendererData.material.SetTextureOffset("_MainTex", value2);
		this.rendererData.material.SetTextureScale("_MainTex", this.uvSize);
		this.lastIndex = num;
	}

	// Token: 0x040076C9 RID: 30409
	public int UvNumX = 1;

	// Token: 0x040076CA RID: 30410
	public int UvNumY = 1;

	// Token: 0x040076CB RID: 30411
	public int ChangeTime = 1000;

	// Token: 0x040076CC RID: 30412
	private int passTime;

	// Token: 0x040076CD RID: 30413
	private int separateTime;

	// Token: 0x040076CE RID: 30414
	private int ptnNum = 1;

	// Token: 0x040076CF RID: 30415
	private Vector2 uvSize;

	// Token: 0x040076D0 RID: 30416
	private Renderer rendererData;

	// Token: 0x040076D1 RID: 30417
	private int lastIndex = -1;
}
