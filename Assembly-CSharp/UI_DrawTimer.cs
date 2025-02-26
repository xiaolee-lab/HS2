using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000A28 RID: 2600
public class UI_DrawTimer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06004D43 RID: 19779 RVA: 0x001DAA07 File Offset: 0x001D8E07
	public void Setup(float _drawTime, float _fadeTime)
	{
		this.drawTime = _drawTime;
		this.fadeTime = _fadeTime;
		this.timeCnt = this.drawTime;
		this.cgSpace.alpha = 1f;
	}

	// Token: 0x06004D44 RID: 19780 RVA: 0x001DAA33 File Offset: 0x001D8E33
	public void OnPointerEnter(PointerEventData ped)
	{
		this.isOver = true;
	}

	// Token: 0x06004D45 RID: 19781 RVA: 0x001DAA3C File Offset: 0x001D8E3C
	public void OnPointerExit(PointerEventData ped)
	{
		this.isOver = false;
	}

	// Token: 0x06004D46 RID: 19782 RVA: 0x001DAA48 File Offset: 0x001D8E48
	private void Update()
	{
		if (this.isOver)
		{
			this.timeCnt = this.drawTime;
			this.cgSpace.alpha = 1f;
		}
		this.timeCnt = Mathf.Max(0f, this.timeCnt - Time.deltaTime);
		if (this.timeCnt < this.fadeTime)
		{
			float alpha = Mathf.InverseLerp(0f, this.fadeTime, this.timeCnt);
			this.cgSpace.alpha = alpha;
		}
	}

	// Token: 0x040046AC RID: 18092
	[SerializeField]
	private CanvasGroup cgSpace;

	// Token: 0x040046AD RID: 18093
	private bool isOver;

	// Token: 0x040046AE RID: 18094
	private float timeCnt;

	// Token: 0x040046AF RID: 18095
	private float drawTime = 2f;

	// Token: 0x040046B0 RID: 18096
	private float fadeTime = 0.3f;
}
