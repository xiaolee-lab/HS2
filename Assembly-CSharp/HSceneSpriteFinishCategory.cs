using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000B0B RID: 2827
public class HSceneSpriteFinishCategory : HSceneSpriteCategory
{
	// Token: 0x060052FB RID: 21243 RVA: 0x002445B8 File Offset: 0x002429B8
	public void Init()
	{
		if (this.atari.GetComponent<PointerEnterTrigger>() == null)
		{
			UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
			PointerEnterTrigger pointerEnterTrigger = this.atari.AddComponent<PointerEnterTrigger>();
			pointerEnterTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				if (Cursor.visible)
				{
					this.onFinish = true;
				}
			});
		}
		if (this.atari.GetComponent<PointerExitTrigger>() == null)
		{
			UITrigger.TriggerEvent triggerEvent2 = new UITrigger.TriggerEvent();
			PointerExitTrigger pointerExitTrigger = this.atari.AddComponent<PointerExitTrigger>();
			pointerExitTrigger.Triggers.Add(triggerEvent2);
			triggerEvent2.AddListener(delegate(BaseEventData x)
			{
				this.onFinish = false;
			});
		}
		this.rt = this.Finish.GetComponent<RectTransform>();
		this.BeforeObj = this.Finish.gameObject;
		(from active in this.BeforeActive
		where active != this.oldBeforeActive
		select active).Subscribe(delegate(bool active)
		{
			this.oldBeforeActive = active;
			this.atari.SetActive(active);
		});
		base.gameObject.SetActive(true);
	}

	// Token: 0x060052FC RID: 21244 RVA: 0x002446AC File Offset: 0x00242AAC
	private void Update()
	{
		this.BeforeActive.Value = this.BeforeObj.activeSelf;
		if (!this.atari.activeSelf)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		Vector3 v = Vector3.zero;
		float num = 0f;
		v = this.rt.anchoredPosition;
		if (this.onFinish)
		{
			v.y = Mathf.SmoothDamp(v.y, this.MainPosY[0], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
		}
		else
		{
			v.y = Mathf.SmoothDamp(v.y, this.MainPosY[1], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
		}
		this.rt.anchoredPosition = v;
	}

	// Token: 0x04004D75 RID: 19829
	[SerializeField]
	private GameObject atari;

	// Token: 0x04004D76 RID: 19830
	[SerializeField]
	private Button Finish;

	// Token: 0x04004D77 RID: 19831
	private bool onFinish;

	// Token: 0x04004D78 RID: 19832
	[SerializeField]
	private Button[] FinishPattern;

	// Token: 0x04004D79 RID: 19833
	[Space]
	[SerializeField]
	private float[] MainPosY = new float[2];

	// Token: 0x04004D7A RID: 19834
	[SerializeField]
	[Space]
	private float smoothTime;

	// Token: 0x04004D7B RID: 19835
	private GameObject BeforeObj;

	// Token: 0x04004D7C RID: 19836
	private RectTransform rt;

	// Token: 0x04004D7D RID: 19837
	private BoolReactiveProperty BeforeActive = new BoolReactiveProperty(false);

	// Token: 0x04004D7E RID: 19838
	private bool oldBeforeActive;
}
