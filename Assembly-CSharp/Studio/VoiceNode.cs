using System;
using Studio.SceneAssist;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200135D RID: 4957
	public class VoiceNode : PointerAction
	{
		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x0600A626 RID: 42534 RVA: 0x0042625C File Offset: 0x0042465C
		public Button buttonUI
		{
			get
			{
				return this.m_Button;
			}
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x0600A627 RID: 42535 RVA: 0x00426264 File Offset: 0x00424664
		public TextMeshProUGUI textUI
		{
			get
			{
				return this.m_Text;
			}
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x0600A628 RID: 42536 RVA: 0x0042626C File Offset: 0x0042466C
		// (set) Token: 0x0600A629 RID: 42537 RVA: 0x00426279 File Offset: 0x00424679
		public string text
		{
			get
			{
				return this.m_Text.text;
			}
			set
			{
				this.m_Text.text = value;
			}
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600A62A RID: 42538 RVA: 0x00426287 File Offset: 0x00424687
		// (set) Token: 0x0600A62B RID: 42539 RVA: 0x00426294 File Offset: 0x00424694
		public bool interactable
		{
			get
			{
				return this.m_Button.interactable;
			}
			set
			{
				this.m_Button.interactable = value;
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x0600A62C RID: 42540 RVA: 0x004262A2 File Offset: 0x004246A2
		// (set) Token: 0x0600A62D RID: 42541 RVA: 0x004262AF File Offset: 0x004246AF
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				if (base.gameObject.activeSelf != value)
				{
					base.gameObject.SetActive(value);
				}
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (set) Token: 0x0600A62E RID: 42542 RVA: 0x004262CE File Offset: 0x004246CE
		public UnityAction addOnClick
		{
			set
			{
				this.m_Button.onClick.AddListener(value);
			}
		}

		// Token: 0x0600A62F RID: 42543 RVA: 0x004262E1 File Offset: 0x004246E1
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.interactable)
			{
				return;
			}
			base.OnPointerEnter(eventData);
		}

		// Token: 0x0600A630 RID: 42544 RVA: 0x004262F8 File Offset: 0x004246F8
		public virtual void Awake()
		{
			VoiceNode.ClickSound clickSound = this.clickSound;
			if (clickSound == VoiceNode.ClickSound.OK)
			{
				this.addOnClick = delegate()
				{
					Assist.PlayDecisionSE();
				};
			}
		}

		// Token: 0x04008288 RID: 33416
		[SerializeField]
		protected Button m_Button;

		// Token: 0x04008289 RID: 33417
		[SerializeField]
		protected TextMeshProUGUI m_Text;

		// Token: 0x0400828A RID: 33418
		[SerializeField]
		protected VoiceNode.ClickSound clickSound;

		// Token: 0x0200135E RID: 4958
		protected enum ClickSound
		{
			// Token: 0x0400828D RID: 33421
			NoSound,
			// Token: 0x0400828E RID: 33422
			OK
		}
	}
}
