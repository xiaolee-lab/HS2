using System;
using Studio.SceneAssist;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200135B RID: 4955
	public class ThumbnailNode : PointerAction
	{
		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x0600A61B RID: 42523 RVA: 0x004398E0 File Offset: 0x00437CE0
		public Button button
		{
			get
			{
				return this.m_Button;
			}
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x0600A61C RID: 42524 RVA: 0x004398E8 File Offset: 0x00437CE8
		public RawImage image
		{
			get
			{
				return this.m_Image;
			}
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x0600A61E RID: 42526 RVA: 0x004398FE File Offset: 0x00437CFE
		// (set) Token: 0x0600A61D RID: 42525 RVA: 0x004398F0 File Offset: 0x00437CF0
		public Texture texture
		{
			get
			{
				return this.m_Image.texture;
			}
			set
			{
				this.m_Image.texture = value;
			}
		}

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x0600A61F RID: 42527 RVA: 0x0043990B File Offset: 0x00437D0B
		// (set) Token: 0x0600A620 RID: 42528 RVA: 0x00439918 File Offset: 0x00437D18
		public bool interactable
		{
			get
			{
				return this.m_Button.interactable;
			}
			set
			{
				this.m_Button.interactable = value;
				this.m_Image.color = ((!value) ? Color.clear : Color.white);
			}
		}

		// Token: 0x170022BC RID: 8892
		// (set) Token: 0x0600A621 RID: 42529 RVA: 0x00439946 File Offset: 0x00437D46
		public UnityAction addOnClick
		{
			set
			{
				this.m_Button.onClick.AddListener(value);
			}
		}

		// Token: 0x0600A622 RID: 42530 RVA: 0x00439959 File Offset: 0x00437D59
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.interactable)
			{
				return;
			}
			base.OnPointerEnter(eventData);
		}

		// Token: 0x0600A623 RID: 42531 RVA: 0x00439970 File Offset: 0x00437D70
		public virtual void Awake()
		{
			ThumbnailNode.ClickSound clickSound = this.clickSound;
			if (clickSound == ThumbnailNode.ClickSound.OK)
			{
				this.addOnClick = delegate()
				{
					Assist.PlayDecisionSE();
				};
			}
		}

		// Token: 0x04008281 RID: 33409
		[SerializeField]
		protected Button m_Button;

		// Token: 0x04008282 RID: 33410
		[SerializeField]
		private RawImage m_Image;

		// Token: 0x04008283 RID: 33411
		[SerializeField]
		protected ThumbnailNode.ClickSound clickSound;

		// Token: 0x0200135C RID: 4956
		protected enum ClickSound
		{
			// Token: 0x04008286 RID: 33414
			NoSound,
			// Token: 0x04008287 RID: 33415
			OK
		}
	}
}
