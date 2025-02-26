using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012DE RID: 4830
	public class ListNode : PointerAction
	{
		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x0600A110 RID: 41232 RVA: 0x00422664 File Offset: 0x00420A64
		// (set) Token: 0x0600A111 RID: 41233 RVA: 0x00422688 File Offset: 0x00420A88
		public bool interactable
		{
			get
			{
				return this.button != null && this.button.interactable;
			}
			set
			{
				this.button.SafeProc(delegate(Button _b)
				{
					_b.interactable = value;
				});
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x0600A112 RID: 41234 RVA: 0x004226BA File Offset: 0x00420ABA
		// (set) Token: 0x0600A113 RID: 41235 RVA: 0x004226E0 File Offset: 0x00420AE0
		public bool select
		{
			get
			{
				return this.imageSelect != null && this.imageSelect.enabled;
			}
			set
			{
				this.imageSelect.SafeProc(delegate(Image _i)
				{
					_i.enabled = value;
				});
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x0600A114 RID: 41236 RVA: 0x00422712 File Offset: 0x00420B12
		public Image image
		{
			[CompilerGenerated]
			get
			{
				return (!(this.button != null)) ? null : this.button.image;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (set) Token: 0x0600A115 RID: 41237 RVA: 0x00422738 File Offset: 0x00420B38
		public Sprite selectSprite
		{
			set
			{
				this.imageSelect.SafeProc(delegate(Image _i)
				{
					_i.sprite = value;
				});
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x0600A116 RID: 41238 RVA: 0x0042276C File Offset: 0x00420B6C
		// (set) Token: 0x0600A117 RID: 41239 RVA: 0x004227C0 File Offset: 0x00420BC0
		public string text
		{
			get
			{
				return (!(this.content != null)) ? ((!(this.textMesh != null)) ? string.Empty : this.textMesh.text) : this.content.text;
			}
			set
			{
				this.content.SafeProc(delegate(Text _t)
				{
					_t.text = value;
				});
				this.textMesh.SafeProc(delegate(TextMeshProUGUI _t)
				{
					_t.text = value;
				});
			}
		}

		// Token: 0x0600A118 RID: 41240 RVA: 0x0042280A File Offset: 0x00420C0A
		private void SetCoverEnabled(bool _enabled)
		{
			if (this.button != null && !this.button.interactable)
			{
				return;
			}
		}

		// Token: 0x0600A119 RID: 41241 RVA: 0x0042282E File Offset: 0x00420C2E
		private void PlaySelectSE()
		{
			if (this.button != null && !this.button.interactable)
			{
				return;
			}
		}

		// Token: 0x0600A11A RID: 41242 RVA: 0x00422852 File Offset: 0x00420C52
		public void AddActionToButton(UnityAction _action)
		{
			if (this.button != null)
			{
				this.button.onClick.AddListener(_action);
			}
		}

		// Token: 0x0600A11B RID: 41243 RVA: 0x00422872 File Offset: 0x00420C72
		public void SetButtonAction(UnityAction _action)
		{
			if (this.button == null)
			{
				return;
			}
			this.button.onClick.RemoveAllListeners();
			this.button.onClick.AddListener(_action);
		}

		// Token: 0x0600A11C RID: 41244 RVA: 0x004228A8 File Offset: 0x00420CA8
		private void Awake()
		{
			this.listEnterAction.Add(delegate
			{
				this.SetCoverEnabled(true);
			});
			this.listEnterAction.Add(new UnityAction(this.PlaySelectSE));
			this.listExitAction.Add(delegate
			{
				this.SetCoverEnabled(false);
			});
		}

		// Token: 0x04007F42 RID: 32578
		[SerializeField]
		private Button button;

		// Token: 0x04007F43 RID: 32579
		[SerializeField]
		private Image imageSelect;

		// Token: 0x04007F44 RID: 32580
		[SerializeField]
		private Text content;

		// Token: 0x04007F45 RID: 32581
		[SerializeField]
		private TextMeshProUGUI textMesh;
	}
}
