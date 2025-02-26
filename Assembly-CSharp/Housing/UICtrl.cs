using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace Housing
{
	// Token: 0x020008BC RID: 2236
	public class UICtrl : MonoBehaviour
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x001561F8 File Offset: 0x001545F8
		public SystemUICtrl SystemUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.systemUICtrl;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x00156200 File Offset: 0x00154600
		public ListUICtrl ListUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.listUICtrl;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x00156208 File Offset: 0x00154608
		public AddUICtrl AddUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.addUICtrl;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x00156210 File Offset: 0x00154610
		public InfoUICtrl InfoUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.infoUICtrl;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x00156218 File Offset: 0x00154618
		public SettingUICtrl SettingUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.settingUICtrl;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x00156220 File Offset: 0x00154620
		public SaveLoadUICtrl SaveLoadUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.saveLoadUICtrl;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x00156228 File Offset: 0x00154628
		public ManipulateUICtrl ManipulateUICtrl
		{
			[CompilerGenerated]
			get
			{
				return this.manipulateUICtrl;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x00156230 File Offset: 0x00154630
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x00156238 File Offset: 0x00154638
		public bool IsInit { get; private set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x00156241 File Offset: 0x00154641
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x00156249 File Offset: 0x00154649
		public CraftCamera CraftCamera { get; private set; }

		// Token: 0x06003A67 RID: 14951 RVA: 0x00156254 File Offset: 0x00154654
		public IEnumerator Init(CraftCamera _craftCamera, bool _tutorial)
		{
			this.CraftCamera = _craftCamera;
			yield return new WaitWhile(() => !Singleton<Housing>.IsInstance() || !Singleton<Housing>.Instance.IsLoadList);
			this.deriveds = new UIDerived[]
			{
				this.systemUICtrl,
				this.listUICtrl,
				this.addUICtrl,
				this.infoUICtrl,
				this.settingUICtrl,
				this.saveLoadUICtrl,
				this.manipulateUICtrl
			};
			foreach (UIDerived uiderived in this.deriveds)
			{
				uiderived.Init(this, _tutorial);
			}
			this.IsInit = true;
			yield break;
		}

		// Token: 0x040039A7 RID: 14759
		[SerializeField]
		private SystemUICtrl systemUICtrl = new SystemUICtrl();

		// Token: 0x040039A8 RID: 14760
		[SerializeField]
		private ListUICtrl listUICtrl = new ListUICtrl();

		// Token: 0x040039A9 RID: 14761
		[SerializeField]
		private AddUICtrl addUICtrl = new AddUICtrl();

		// Token: 0x040039AA RID: 14762
		[SerializeField]
		private InfoUICtrl infoUICtrl = new InfoUICtrl();

		// Token: 0x040039AB RID: 14763
		[SerializeField]
		private SettingUICtrl settingUICtrl = new SettingUICtrl();

		// Token: 0x040039AC RID: 14764
		[SerializeField]
		private SaveLoadUICtrl saveLoadUICtrl = new SaveLoadUICtrl();

		// Token: 0x040039AD RID: 14765
		[SerializeField]
		private ManipulateUICtrl manipulateUICtrl = new ManipulateUICtrl();

		// Token: 0x040039B0 RID: 14768
		private UIDerived[] deriveds;
	}
}
