using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200135A RID: 4954
	[RequireComponent(typeof(TextSlideEffect))]
	public class TextSlideEffectCtrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x0600A610 RID: 42512 RVA: 0x004395D8 File Offset: 0x004379D8
		private float preferredWidth
		{
			get
			{
				return (!this.text) ? ((!this.textMesh) ? 0f : this.textMesh.preferredWidth) : this.text.preferredWidth;
			}
		}

		// Token: 0x0600A611 RID: 42513 RVA: 0x0043962C File Offset: 0x00437A2C
		private void MoveText()
		{
			float x = this.transBase.sizeDelta.x;
			float preferredWidth = this.preferredWidth;
			if (x >= preferredWidth)
			{
				return;
			}
			if (this.text)
			{
				float num = this.textSlideEffect.subPos;
				if (num > preferredWidth)
				{
					num -= preferredWidth + x;
				}
				num += this.speed * Time.deltaTime;
				this.textSlideEffect.subPos = num;
			}
			else if (this.textMesh)
			{
				Vector4 margin = this.textMesh.margin;
				if (Mathf.Abs(margin.x) > preferredWidth)
				{
					margin.x += preferredWidth + x;
				}
				margin.x -= this.speed * Time.deltaTime;
				this.textMesh.margin = margin;
			}
		}

		// Token: 0x0600A612 RID: 42514 RVA: 0x0043970C File Offset: 0x00437B0C
		private void Check()
		{
			float x = this.transBase.sizeDelta.x;
			float preferredWidth = this.preferredWidth;
			if (x >= preferredWidth)
			{
				ObservableLateUpdateTrigger component = base.GetComponent<ObservableLateUpdateTrigger>();
				if (component != null)
				{
					UnityEngine.Object.Destroy(component);
				}
				UnityEngine.Object.Destroy(this);
				UnityEngine.Object.Destroy(this.textSlideEffect);
				return;
			}
			if (this.text)
			{
				this.text.alignment = TextAnchor.MiddleLeft;
				this.text.horizontalOverflow = HorizontalWrapMode.Overflow;
				this.text.raycastTarget = true;
			}
			else if (this.textMesh)
			{
				this.textMesh.alignment = TextAlignmentOptions.MidlineLeft;
				this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
				this.textMesh.enableWordWrapping = false;
			}
			this.AddFunc();
		}

		// Token: 0x0600A613 RID: 42515 RVA: 0x004397DD File Offset: 0x00437BDD
		private void AddFunc()
		{
			(from _ in this.UpdateAsObservable()
			where this.isPlay
			select _).Subscribe(delegate(Unit _)
			{
				this.MoveText();
			}).AddTo(this);
		}

		// Token: 0x0600A614 RID: 42516 RVA: 0x0043980E File Offset: 0x00437C0E
		private void Start()
		{
			if (this.assist)
			{
				this.LateUpdateAsObservable().First<Unit>().Subscribe(delegate(Unit _)
				{
					this.Check();
				}).AddTo(this);
			}
			else
			{
				this.AddFunc();
			}
		}

		// Token: 0x0600A615 RID: 42517 RVA: 0x00439849 File Offset: 0x00437C49
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isPlay = true;
			if (this.textMesh)
			{
				this.textMesh.overflowMode = TextOverflowModes.Overflow;
			}
		}

		// Token: 0x0600A616 RID: 42518 RVA: 0x00439870 File Offset: 0x00437C70
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isPlay = false;
			this.textSlideEffect.subPos = 0f;
			if (this.textMesh)
			{
				this.textMesh.margin = Vector4.zero;
				this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
			}
		}

		// Token: 0x0400827A RID: 33402
		[SerializeField]
		private TextSlideEffect textSlideEffect;

		// Token: 0x0400827B RID: 33403
		[SerializeField]
		private RectTransform transBase;

		// Token: 0x0400827C RID: 33404
		[SerializeField]
		private Text text;

		// Token: 0x0400827D RID: 33405
		[SerializeField]
		private TextMeshProUGUI textMesh;

		// Token: 0x0400827E RID: 33406
		public float speed = 50f;

		// Token: 0x0400827F RID: 33407
		public bool assist;

		// Token: 0x04008280 RID: 33408
		private bool isPlay;
	}
}
