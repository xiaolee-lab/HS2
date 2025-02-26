using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio.Anime
{
	// Token: 0x020012E7 RID: 4839
	public class ListNode : PointerAction
	{
		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x0600A178 RID: 41336 RVA: 0x004248FA File Offset: 0x00422CFA
		public TextMeshProUGUI TextMeshUGUI
		{
			[CompilerGenerated]
			get
			{
				return this.textMesh;
			}
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x0600A179 RID: 41337 RVA: 0x00424904 File Offset: 0x00422D04
		private Image ImageButton
		{
			[CompilerGenerated]
			get
			{
				Image result;
				if ((result = this.imageButton) == null)
				{
					result = (this.imageButton = this.button.image);
				}
				return result;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x0600A17A RID: 41338 RVA: 0x00424932 File Offset: 0x00422D32
		// (set) Token: 0x0600A17B RID: 41339 RVA: 0x00424958 File Offset: 0x00422D58
		public bool Interactable
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

		// Token: 0x17002205 RID: 8709
		// (set) Token: 0x0600A17C RID: 41340 RVA: 0x0042498C File Offset: 0x00422D8C
		public bool Select
		{
			set
			{
				this.ImageButton.SafeProc(delegate(Image _i)
				{
					_i.color = ((!value) ? Color.white : Color.green);
				});
			}
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x0600A17D RID: 41341 RVA: 0x004249BE File Offset: 0x00422DBE
		// (set) Token: 0x0600A17E RID: 41342 RVA: 0x004249E8 File Offset: 0x00422DE8
		public string Text
		{
			get
			{
				return (!(this.textMesh != null)) ? string.Empty : this.textMesh.text;
			}
			set
			{
				this.textMesh.SafeProc(delegate(TextMeshProUGUI _t)
				{
					_t.text = value;
					if (this.UseSlide)
					{
						if (this.slideEffect != null)
						{
							this.slideEffect.OnChangedText();
						}
					}
				});
			}
		}

		// Token: 0x17002207 RID: 8711
		// (set) Token: 0x0600A17F RID: 41343 RVA: 0x00424A24 File Offset: 0x00422E24
		public Color TextColor
		{
			set
			{
				this.textMesh.SafeProc(delegate(TextMeshProUGUI _t)
				{
					_t.color = value;
				});
			}
		}

		// Token: 0x17002208 RID: 8712
		// (set) Token: 0x0600A180 RID: 41344 RVA: 0x00424A56 File Offset: 0x00422E56
		public Material TextMeshMaterial
		{
			set
			{
				this.textMesh.fontSharedMaterial = value;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x0600A181 RID: 41345 RVA: 0x00424A64 File Offset: 0x00422E64
		// (set) Token: 0x0600A182 RID: 41346 RVA: 0x00424A6C File Offset: 0x00422E6C
		public bool UseSlide { get; set; } = true;

		// Token: 0x0600A183 RID: 41347 RVA: 0x00424A78 File Offset: 0x00422E78
		public void SetButtonAction(UnityAction _action)
		{
			if (this.button == null)
			{
				return;
			}
			this.button.onClick.RemoveAllListeners();
			this.button.onClick.AddListener(_action);
			this.button.onClick.AddListener(delegate()
			{
				if (this.UseSlide && Studio.optionSystem.autoHide)
				{
					if (this.slideEffect != null)
					{
						this.slideEffect.Stop();
					}
				}
			});
		}

		// Token: 0x04007FA7 RID: 32679
		[SerializeField]
		private Button button;

		// Token: 0x04007FA8 RID: 32680
		[SerializeField]
		private TextMeshProUGUI textMesh;

		// Token: 0x04007FA9 RID: 32681
		[SerializeField]
		private TextMeshSlideEffect slideEffect;

		// Token: 0x04007FAA RID: 32682
		private Image imageButton;
	}
}
