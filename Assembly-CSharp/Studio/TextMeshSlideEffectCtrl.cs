using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001358 RID: 4952
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextMeshSlideEffectCtrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x0600A601 RID: 42497 RVA: 0x00439330 File Offset: 0x00437730
		private float preferredWidth
		{
			get
			{
				return (!this.textMesh) ? 0f : this.textMesh.preferredWidth;
			}
		}

		// Token: 0x0600A602 RID: 42498 RVA: 0x00439358 File Offset: 0x00437758
		private void MoveText()
		{
			float x = this.transBase.sizeDelta.x;
			float preferredWidth = this.preferredWidth;
			if (x >= preferredWidth)
			{
				return;
			}
			Vector4 margin = this.textMesh.margin;
			if (Mathf.Abs(margin.x) > preferredWidth)
			{
				margin.x += preferredWidth + x;
			}
			margin.x -= this.speed * Time.deltaTime;
			this.textMesh.margin = margin;
		}

		// Token: 0x0600A603 RID: 42499 RVA: 0x004393E0 File Offset: 0x004377E0
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
				return;
			}
			this.textMesh.alignment = TextAlignmentOptions.MidlineLeft;
			this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
			this.textMesh.enableWordWrapping = false;
			this.AddFunc();
		}

		// Token: 0x0600A604 RID: 42500 RVA: 0x0043945D File Offset: 0x0043785D
		private void AddFunc()
		{
			(from _ in this.UpdateAsObservable()
			where this.isPlay
			select _).Subscribe(delegate(Unit _)
			{
				this.MoveText();
			}).AddTo(this);
		}

		// Token: 0x0600A605 RID: 42501 RVA: 0x0043948E File Offset: 0x0043788E
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

		// Token: 0x0600A606 RID: 42502 RVA: 0x004394C9 File Offset: 0x004378C9
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isPlay = true;
			this.textMesh.overflowMode = TextOverflowModes.Overflow;
		}

		// Token: 0x0600A607 RID: 42503 RVA: 0x004394DE File Offset: 0x004378DE
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isPlay = false;
			this.textMesh.margin = Vector4.zero;
			this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
		}

		// Token: 0x04008274 RID: 33396
		[SerializeField]
		private RectTransform transBase;

		// Token: 0x04008275 RID: 33397
		[SerializeField]
		private TextMeshProUGUI textMesh;

		// Token: 0x04008276 RID: 33398
		public float speed = 50f;

		// Token: 0x04008277 RID: 33399
		public bool assist;

		// Token: 0x04008278 RID: 33400
		private bool isPlay;
	}
}
