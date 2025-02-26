using System;
using UnityEngine;

// Token: 0x02001178 RID: 4472
public class CrossFadeObject : MonoBehaviour
{
	// Token: 0x060093B6 RID: 37814 RVA: 0x003D1344 File Offset: 0x003CF744
	private float CalcRate()
	{
		if (this.span == 0f)
		{
			return 1f;
		}
		if (this.timer < this.delay)
		{
			return 0f;
		}
		float num = this.timer - this.delay;
		return Mathf.Clamp01(num / this.span);
	}

	// Token: 0x060093B7 RID: 37815 RVA: 0x003D1399 File Offset: 0x003CF799
	private void Awake()
	{
		this.alpha = 1f;
	}

	// Token: 0x060093B8 RID: 37816 RVA: 0x003D13A8 File Offset: 0x003CF7A8
	private void Update()
	{
		float num = this.CalcRate();
		if (this.timer > this.delay && num >= 1f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.alpha = 1f - num;
		this.timer += Time.deltaTime;
	}

	// Token: 0x060093B9 RID: 37817 RVA: 0x003D1403 File Offset: 0x003CF803
	private void OnDestroy()
	{
		if (this.tex)
		{
			this.tex.Release();
			UnityEngine.Object.Destroy(this.tex);
			this.tex = null;
		}
	}

	// Token: 0x060093BA RID: 37818 RVA: 0x003D1434 File Offset: 0x003CF834
	private void OnGUI()
	{
		GUI.depth = this.depth;
		GUI.color = new Color(1f, 1f, 1f, this.alpha);
		if (this.tex == null)
		{
			return;
		}
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.tex, ScaleMode.ScaleToFit, false);
	}

	// Token: 0x04007714 RID: 30484
	public RenderTexture tex;

	// Token: 0x04007715 RID: 30485
	private float span = 1f;

	// Token: 0x04007716 RID: 30486
	private float delay;

	// Token: 0x04007717 RID: 30487
	private float timer;

	// Token: 0x04007718 RID: 30488
	private int depth = 10;

	// Token: 0x04007719 RID: 30489
	private float alpha = 1f;
}
