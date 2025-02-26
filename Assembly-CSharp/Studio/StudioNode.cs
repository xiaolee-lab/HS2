using System;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using Studio.SceneAssist;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001345 RID: 4933
	public class StudioNode : PointerAction
	{
		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x0600A516 RID: 42262 RVA: 0x00434E77 File Offset: 0x00433277
		public Button buttonUI
		{
			get
			{
				return this.m_Button;
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x0600A517 RID: 42263 RVA: 0x00434E80 File Offset: 0x00433280
		public Image imageButton
		{
			[CompilerGenerated]
			get
			{
				Image result;
				if ((result = this.m_ImageButton) == null)
				{
					result = (this.m_ImageButton = this.m_Button.image);
				}
				return result;
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x0600A518 RID: 42264 RVA: 0x00434EAE File Offset: 0x004332AE
		public Text textUI
		{
			get
			{
				return this.m_Text;
			}
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x0600A519 RID: 42265 RVA: 0x00434EB6 File Offset: 0x004332B6
		// (set) Token: 0x0600A51A RID: 42266 RVA: 0x00434EC4 File Offset: 0x004332C4
		public string text
		{
			get
			{
				return this.m_Text.text;
			}
			set
			{
				this.m_Text.SafeProc(delegate(Text _t)
				{
					_t.text = value;
				});
				this._textMesh.SafeProc(delegate(TextMeshProUGUI _t)
				{
					_t.text = value;
				});
			}
		}

		// Token: 0x17002291 RID: 8849
		// (set) Token: 0x0600A51B RID: 42267 RVA: 0x00434F10 File Offset: 0x00433310
		public Color TextColor
		{
			set
			{
				this.m_Text.SafeProc(delegate(Text _t)
				{
					_t.color = value;
				});
				this._textMesh.SafeProc(delegate(TextMeshProUGUI _t)
				{
					_t.color = value;
				});
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x0600A51C RID: 42268 RVA: 0x00434F5A File Offset: 0x0043335A
		// (set) Token: 0x0600A51D RID: 42269 RVA: 0x00434F62 File Offset: 0x00433362
		public bool select
		{
			get
			{
				return this.m_Select;
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Select, value))
				{
					this.imageButton.color = ((!this.m_Select) ? Color.white : Color.green);
				}
			}
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x0600A51E RID: 42270 RVA: 0x00434F9A File Offset: 0x0043339A
		// (set) Token: 0x0600A51F RID: 42271 RVA: 0x00434FA7 File Offset: 0x004333A7
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

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x0600A520 RID: 42272 RVA: 0x00434FB5 File Offset: 0x004333B5
		// (set) Token: 0x0600A521 RID: 42273 RVA: 0x00434FC2 File Offset: 0x004333C2
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActiveIfDifferent(value);
			}
		}

		// Token: 0x17002295 RID: 8853
		// (set) Token: 0x0600A522 RID: 42274 RVA: 0x00434FD1 File Offset: 0x004333D1
		public UnityAction addOnClick
		{
			set
			{
				this.m_Button.onClick.AddListener(value);
			}
		}

		// Token: 0x0600A523 RID: 42275 RVA: 0x00434FE4 File Offset: 0x004333E4
		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.interactable)
			{
				return;
			}
			base.OnPointerEnter(eventData);
		}

		// Token: 0x0600A524 RID: 42276 RVA: 0x00434FFC File Offset: 0x004333FC
		public virtual void Awake()
		{
			StudioNode.ClickSound clickSound = this.clickSound;
			if (clickSound == StudioNode.ClickSound.OK)
			{
				this.addOnClick = delegate()
				{
					Assist.PlayDecisionSE();
				};
			}
		}

		// Token: 0x0400820A RID: 33290
		[SerializeField]
		protected Button m_Button;

		// Token: 0x0400820B RID: 33291
		[SerializeField]
		protected Image m_ImageButton;

		// Token: 0x0400820C RID: 33292
		[SerializeField]
		protected Text m_Text;

		// Token: 0x0400820D RID: 33293
		[SerializeField]
		private TextMeshProUGUI _textMesh;

		// Token: 0x0400820E RID: 33294
		[SerializeField]
		protected StudioNode.ClickSound clickSound;

		// Token: 0x0400820F RID: 33295
		protected bool m_Select;

		// Token: 0x02001346 RID: 4934
		protected enum ClickSound
		{
			// Token: 0x04008212 RID: 33298
			NoSound,
			// Token: 0x04008213 RID: 33299
			OK
		}
	}
}
