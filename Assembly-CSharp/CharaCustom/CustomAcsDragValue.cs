using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009FD RID: 2557
	public class CustomAcsDragValue : MonoBehaviour
	{
		// Token: 0x06004B90 RID: 19344 RVA: 0x001CE476 File Offset: 0x001CC876
		private void Awake()
		{
			this.imgCol = base.GetComponent<Image>();
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x001CE484 File Offset: 0x001CC884
		private void Start()
		{
			float backMousePos = 0f;
			ObservableEventTrigger observableEventTrigger = base.gameObject.AddComponent<ObservableEventTrigger>();
			(from _ in this.UpdateAsObservable().SkipUntil(observableEventTrigger.OnPointerDownAsObservable().Do(delegate(PointerEventData _)
			{
				backMousePos = Input.mousePosition.x;
				if (this.imgCol)
				{
					this.imgCol.color = new Color(this.imgCol.color.r, this.imgCol.color.g, this.imgCol.color.b, 1f);
				}
			})).TakeUntil(observableEventTrigger.OnPointerUpAsObservable().Do(delegate(PointerEventData _)
			{
				if (this.imgCol)
				{
					this.imgCol.color = new Color(this.imgCol.color.r, this.imgCol.color.g, this.imgCol.color.b, 0f);
				}
			})).RepeatUntilDestroy(this)
			select Input.mousePosition.x - backMousePos).Subscribe(delegate(float move)
			{
				backMousePos = Input.mousePosition.x;
				if (this.type == 0 && this.xyz == 0)
				{
					move *= -1f;
				}
				this.customAcsCorrectSet.UpdateDragValue(this.type, this.xyz, move);
			}).AddTo(this);
		}

		// Token: 0x04004579 RID: 17785
		[SerializeField]
		private CustomAcsCorrectSet customAcsCorrectSet;

		// Token: 0x0400457A RID: 17786
		[SerializeField]
		private int type;

		// Token: 0x0400457B RID: 17787
		[SerializeField]
		private int xyz;

		// Token: 0x0400457C RID: 17788
		private Image imgCol;
	}
}
