using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008B3 RID: 2227
	[Serializable]
	public class SettingUICtrl : UIDerived
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x00154A90 File Offset: 0x00152E90
		// (set) Token: 0x06003A0A RID: 14858 RVA: 0x00154AA7 File Offset: 0x00152EA7
		private bool Visible
		{
			get
			{
				return this.cgRoot.alpha != 0f;
			}
			set
			{
				this.cgRoot.Enable(value, false);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x00154AB6 File Offset: 0x00152EB6
		private OCItem OCItem
		{
			[CompilerGenerated]
			get
			{
				return this.objectCtrl as OCItem;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x00154AC3 File Offset: 0x00152EC3
		private OIItem OIItem
		{
			[CompilerGenerated]
			get
			{
				OCItem ocitem = this.OCItem;
				return (ocitem != null) ? ocitem.OIItem : null;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x00154ADA File Offset: 0x00152EDA
		private OCFolder OCFolder
		{
			[CompilerGenerated]
			get
			{
				return this.objectCtrl as OCFolder;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x00154AE7 File Offset: 0x00152EE7
		private OIFolder OIFolder
		{
			[CompilerGenerated]
			get
			{
				OCFolder ocfolder = this.OCFolder;
				return (ocfolder != null) ? ocfolder.OIFolder : null;
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x00154B00 File Offset: 0x00152F00
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			this.itemUI.color1.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.itemUI.colorPanel.isOpen && this.itemUI.color1.IsOpen)
				{
					this.itemUI.Close(true);
				}
				else
				{
					this.itemUI.SetOpen(0);
					this.itemUI.colorPanel.Setup(this.OIItem.Color1, delegate(Color _c)
					{
						this.itemUI.color1.Color = _c;
						this.OCItem.Color1 = _c;
					}, false);
				}
			});
			this.itemUI.color2.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.itemUI.colorPanel.isOpen && this.itemUI.color2.IsOpen)
				{
					this.itemUI.Close(true);
				}
				else
				{
					this.itemUI.SetOpen(1);
					this.itemUI.colorPanel.Setup(this.OIItem.Color2, delegate(Color _c)
					{
						this.itemUI.color2.Color = _c;
						this.OCItem.Color2 = _c;
					}, false);
				}
			});
			this.itemUI.color3.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.itemUI.colorPanel.isOpen && this.itemUI.color3.IsOpen)
				{
					this.itemUI.Close(true);
				}
				else
				{
					this.itemUI.SetOpen(2);
					this.itemUI.colorPanel.Setup(this.OIItem.Color3, delegate(Color _c)
					{
						this.itemUI.color3.Color = _c;
						this.OCItem.Color3 = _c;
					}, false);
				}
			});
			this.itemUI.emissionColor.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.itemUI.colorPanel.isOpen && this.itemUI.emissionColor.IsOpen)
				{
					this.itemUI.Close(true);
				}
				else
				{
					float exposure = 0f;
					Color32 c;
					SettingUICtrl.DecomposeHdrColor(this.OIItem.EmissionColor, out c, out exposure);
					c.a = byte.MaxValue;
					this.itemUI.SetOpen(3);
					this.itemUI.colorPanel.Setup(c, delegate(Color _c)
					{
						_c *= Mathf.Pow(2f, exposure);
						this.itemUI.emissionColor.Color = _c;
						this.OCItem.EmissionColor = _c;
					}, false);
				}
			});
			this.itemUI.option.toggle.OnValueChangedAsObservable().Subscribe(delegate(bool _b)
			{
				if (this.OCItem != null)
				{
					this.OCItem.VisibleOption = _b;
				}
			});
			this.itemUI.init.buttonInit.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				if (this.OCItem == null)
				{
					return;
				}
				this.OCItem.ResetColor();
				this.itemUI.Close(true);
				this.itemUI.color1.Color = this.OCItem.Color1;
				this.itemUI.color2.Color = this.OCItem.Color2;
				this.itemUI.color3.Color = this.OCItem.Color3;
				this.itemUI.emissionColor.Color = this.OCItem.EmissionColor;
			});
			ColorPanel colorPanel = this.itemUI.colorPanel;
			colorPanel.onClose = (Action)Delegate.Combine(colorPanel.onClose, new Action(delegate()
			{
				this.itemUI.Close(false);
			}));
			this.folderUI.inputName.OnEndEditAsObservable().Subscribe(delegate(string _s)
			{
				this.OIFolder.Name = _s;
				base.UICtrl.ListUICtrl.RefreshList();
			});
			Selection instance = Singleton<Selection>.Instance;
			instance.onSelectFunc = (Action<ObjectCtrl[]>)Delegate.Combine(instance.onSelectFunc, new Action<ObjectCtrl[]>(this.OnSelect));
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00154C73 File Offset: 0x00153073
		public override void UpdateUI()
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x00154C78 File Offset: 0x00153078
		private void OnSelect(ObjectCtrl[] _objectCtrls)
		{
			if (_objectCtrls.IsNullOrEmpty<ObjectCtrl>())
			{
				this.objectCtrl = null;
				this.Visible = false;
				return;
			}
			this.objectCtrl = _objectCtrls.SafeGet(0);
			int kind = this.objectCtrl.Kind;
			if (kind != 0)
			{
				if (kind == 1)
				{
					this.Visible = true;
					this.itemUI.Active = false;
					this.folderUI.Active = true;
					this.textTitle.text = "フォルダー設定";
					this.folderUI.Name = this.OIFolder.Name;
				}
			}
			else
			{
				this.itemUI.SetActive(new bool[]
				{
					this.OCItem.IsColor1,
					this.OCItem.IsColor2,
					this.OCItem.IsColor3,
					this.OCItem.IsEmissionColor,
					this.OCItem.IsOption,
					this.OCItem.IsColor
				});
				this.folderUI.Active = false;
				this.textTitle.text = "アイテム設定";
				this.itemUI.color1.Color = this.OIItem.Color1;
				this.itemUI.color2.Color = this.OIItem.Color2;
				this.itemUI.color3.Color = this.OIItem.Color3;
				this.itemUI.emissionColor.Color = this.OIItem.EmissionColor;
				this.itemUI.option.IsOn = this.OIItem.VisibleOption;
				this.Visible = this.itemUI.Active;
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00154E34 File Offset: 0x00153234
		internal static void DecomposeHdrColor(Color linearColorHdr, out Color32 baseLinearColor, out float exposure)
		{
			baseLinearColor = linearColorHdr;
			float maxColorComponent = linearColorHdr.maxColorComponent;
			byte b = 191;
			if (maxColorComponent == 0f || (maxColorComponent <= 1f && maxColorComponent >= 0.003921569f))
			{
				exposure = 0f;
				baseLinearColor.r = (byte)Mathf.RoundToInt(linearColorHdr.r * 255f);
				baseLinearColor.g = (byte)Mathf.RoundToInt(linearColorHdr.g * 255f);
				baseLinearColor.b = (byte)Mathf.RoundToInt(linearColorHdr.b * 255f);
			}
			else
			{
				float num = (float)b / maxColorComponent;
				exposure = Mathf.Log(255f / num) / Mathf.Log(2f);
				baseLinearColor.r = (byte)Mathf.Min((int)b, (int)((byte)Mathf.CeilToInt(num * linearColorHdr.r)));
				baseLinearColor.g = (byte)Mathf.Min((int)b, (int)((byte)Mathf.CeilToInt(num * linearColorHdr.g)));
				baseLinearColor.b = (byte)Mathf.Min((int)b, (int)((byte)Mathf.CeilToInt(num * linearColorHdr.b)));
			}
		}

		// Token: 0x0400397D RID: 14717
		[SerializeField]
		private CanvasGroup cgRoot;

		// Token: 0x0400397E RID: 14718
		[SerializeField]
		private Text textTitle;

		// Token: 0x0400397F RID: 14719
		[SerializeField]
		[Header("アイテム設定")]
		private SettingUICtrl.ItemUI itemUI = new SettingUICtrl.ItemUI();

		// Token: 0x04003980 RID: 14720
		[SerializeField]
		[Header("フォルダー設定")]
		private SettingUICtrl.FolderUI folderUI = new SettingUICtrl.FolderUI();

		// Token: 0x04003981 RID: 14721
		private ObjectCtrl objectCtrl;

		// Token: 0x020008B4 RID: 2228
		private class BaseClass
		{
			// Token: 0x17000A76 RID: 2678
			// (get) Token: 0x06003A20 RID: 14880 RVA: 0x001552C4 File Offset: 0x001536C4
			// (set) Token: 0x06003A1F RID: 14879 RVA: 0x001552B5 File Offset: 0x001536B5
			public virtual bool Active
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x04003982 RID: 14722
			public GameObject objRoot;
		}

		// Token: 0x020008B5 RID: 2229
		[Serializable]
		private class ColorUI : SettingUICtrl.BaseClass
		{
			// Token: 0x17000A77 RID: 2679
			// (set) Token: 0x06003A22 RID: 14882 RVA: 0x001552DC File Offset: 0x001536DC
			public virtual Color Color
			{
				set
				{
					Image image;
					if ((image = this.m_image) == null)
					{
						image = (this.m_image = this.buttonColor.image);
					}
					image.color = value;
				}
			}

			// Token: 0x17000A78 RID: 2680
			// (get) Token: 0x06003A23 RID: 14883 RVA: 0x00155310 File Offset: 0x00153710
			// (set) Token: 0x06003A24 RID: 14884 RVA: 0x00155318 File Offset: 0x00153718
			public bool IsOpen { get; set; }

			// Token: 0x04003983 RID: 14723
			public Button buttonColor;

			// Token: 0x04003984 RID: 14724
			protected Image m_image;
		}

		// Token: 0x020008B6 RID: 2230
		[Serializable]
		private class EmissionColorUI : SettingUICtrl.ColorUI
		{
			// Token: 0x17000A79 RID: 2681
			// (set) Token: 0x06003A26 RID: 14886 RVA: 0x0015532C File Offset: 0x0015372C
			public override Color Color
			{
				set
				{
					value.a = 1f;
					Image image;
					if ((image = this.m_image) == null)
					{
						image = (this.m_image = this.buttonColor.image);
					}
					image.color = value;
				}
			}
		}

		// Token: 0x020008B7 RID: 2231
		[Serializable]
		private class ToggleUI : SettingUICtrl.BaseClass
		{
			// Token: 0x17000A7A RID: 2682
			// (set) Token: 0x06003A28 RID: 14888 RVA: 0x00155374 File Offset: 0x00153774
			public bool IsOn
			{
				set
				{
					this.toggle.isOn = value;
				}
			}

			// Token: 0x04003986 RID: 14726
			public Toggle toggle;
		}

		// Token: 0x020008B8 RID: 2232
		[Serializable]
		private class InitColorUI : SettingUICtrl.BaseClass
		{
			// Token: 0x04003987 RID: 14727
			public Button buttonInit;
		}

		// Token: 0x020008B9 RID: 2233
		[Serializable]
		private class ItemUI : SettingUICtrl.BaseClass
		{
			// Token: 0x17000A7B RID: 2683
			// (set) Token: 0x06003A2B RID: 14891 RVA: 0x001553E1 File Offset: 0x001537E1
			public override bool Active
			{
				set
				{
					base.Active = value;
					this.Close(true);
				}
			}

			// Token: 0x06003A2C RID: 14892 RVA: 0x001553F4 File Offset: 0x001537F4
			public void SetActive(params bool[] _params)
			{
				SettingUICtrl.ItemUI.<SetActive>c__AnonStorey0 <SetActive>c__AnonStorey = new SettingUICtrl.ItemUI.<SetActive>c__AnonStorey0();
				<SetActive>c__AnonStorey.array = new SettingUICtrl.BaseClass[]
				{
					this.color1,
					this.color2,
					this.color3,
					this.emissionColor,
					this.option,
					this.init
				};
				int i;
				for (i = 0; i < <SetActive>c__AnonStorey.array.Length; i++)
				{
					if (!_params.SafeProc(i, delegate(bool _b)
					{
						<SetActive>c__AnonStorey.array[i].Active = _b;
					}))
					{
						<SetActive>c__AnonStorey.array[i].Active = false;
					}
				}
				this.Active = _params.Any((bool v) => v);
			}

			// Token: 0x06003A2D RID: 14893 RVA: 0x001554DC File Offset: 0x001538DC
			public void SetOpen(int _idx)
			{
				SettingUICtrl.ColorUI[] array = new SettingUICtrl.ColorUI[]
				{
					this.color1,
					this.color2,
					this.color3,
					this.emissionColor
				};
				for (int i = 0; i < array.Length; i++)
				{
					array[i].IsOpen = (i == _idx);
				}
			}

			// Token: 0x06003A2E RID: 14894 RVA: 0x00155534 File Offset: 0x00153934
			public void Close(bool _panel = true)
			{
				this.color1.IsOpen = false;
				this.color2.IsOpen = false;
				this.color3.IsOpen = false;
				this.emissionColor.IsOpen = false;
				if (_panel)
				{
					this.colorPanel.Close();
				}
			}

			// Token: 0x04003988 RID: 14728
			public SettingUICtrl.ColorUI color1 = new SettingUICtrl.ColorUI();

			// Token: 0x04003989 RID: 14729
			public SettingUICtrl.ColorUI color2 = new SettingUICtrl.ColorUI();

			// Token: 0x0400398A RID: 14730
			public SettingUICtrl.ColorUI color3 = new SettingUICtrl.ColorUI();

			// Token: 0x0400398B RID: 14731
			public SettingUICtrl.EmissionColorUI emissionColor = new SettingUICtrl.EmissionColorUI();

			// Token: 0x0400398C RID: 14732
			public SettingUICtrl.ToggleUI option = new SettingUICtrl.ToggleUI();

			// Token: 0x0400398D RID: 14733
			public SettingUICtrl.InitColorUI init = new SettingUICtrl.InitColorUI();

			// Token: 0x0400398E RID: 14734
			[Header("カラーピッカー")]
			public ColorPanel colorPanel;
		}

		// Token: 0x020008BA RID: 2234
		[Serializable]
		private class FolderUI : SettingUICtrl.BaseClass
		{
			// Token: 0x17000A7C RID: 2684
			// (set) Token: 0x06003A31 RID: 14897 RVA: 0x001555B7 File Offset: 0x001539B7
			public string Name
			{
				set
				{
					this.inputName.text = value;
				}
			}

			// Token: 0x04003990 RID: 14736
			public InputField inputName;
		}
	}
}
