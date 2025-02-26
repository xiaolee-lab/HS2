using System;
using System.Collections.Generic;
using AIChara;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x0200099A RID: 2458
	public class CustomClothesPatternSelect : MonoBehaviour
	{
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x001B30B7 File Offset: 0x001B14B7
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060046B7 RID: 18103 RVA: 0x001B30BE File Offset: 0x001B14BE
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x001B30CB File Offset: 0x001B14CB
		private ChaFileClothes nowClothes
		{
			get
			{
				return this.chaCtrl.nowCoordinate.clothes;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060046B9 RID: 18105 RVA: 0x001B30DD File Offset: 0x001B14DD
		private ChaFileClothes orgClothes
		{
			get
			{
				return this.chaCtrl.chaFile.coordinate.clothes;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x001B30F4 File Offset: 0x001B14F4
		private ChaFileHair hair
		{
			get
			{
				return this.chaCtrl.fileHair;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x001B3101 File Offset: 0x001B1501
		// (set) Token: 0x060046BC RID: 18108 RVA: 0x001B3109 File Offset: 0x001B1509
		public int type { get; set; } = -1;

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x001B3112 File Offset: 0x001B1512
		// (set) Token: 0x060046BE RID: 18110 RVA: 0x001B311A File Offset: 0x001B151A
		public int parts { get; set; } = -1;

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060046BF RID: 18111 RVA: 0x001B3123 File Offset: 0x001B1523
		// (set) Token: 0x060046C0 RID: 18112 RVA: 0x001B312B File Offset: 0x001B152B
		public int idx { get; set; } = -1;

		// Token: 0x060046C1 RID: 18113 RVA: 0x001B3134 File Offset: 0x001B1534
		public void ChangeLink(int _type, int _parts, int _idx = 0)
		{
			this.type = _type;
			this.parts = _parts;
			this.idx = _idx;
			if (this.type == -1 || this.parts == -1 || this.idx == -1)
			{
				return;
			}
			this.ReCreateList(this.type);
			if (this.type == 0)
			{
				this.sscPattern.SetToggleID(this.nowClothes.parts[this.parts].colorInfo[this.idx].pattern);
			}
			else
			{
				this.sscPattern.SetToggleID(this.hair.parts[this.parts].meshType);
			}
			this.sscPattern.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null)
				{
					if (this.type == 0)
					{
						if (this.nowClothes.parts[this.parts].colorInfo[this.idx].pattern != info.id)
						{
							this.nowClothes.parts[this.parts].colorInfo[this.idx].pattern = info.id;
							this.orgClothes.parts[this.parts].colorInfo[this.idx].pattern = info.id;
							this.chaCtrl.ChangeCustomClothes(this.parts, false, 0 == this.idx, 1 == this.idx, 2 == this.idx);
							if (this.onSelect != null)
							{
								this.onSelect(this.parts, this.idx);
							}
						}
					}
					else if (this.hair.parts[this.parts].meshType != info.id)
					{
						this.hair.parts[this.parts].meshType = info.id;
						this.chaCtrl.ChangeSettingHairMeshType(this.parts);
						if (this.onSelect != null)
						{
							this.onSelect(this.parts, this.idx);
						}
					}
				}
			};
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x001B3200 File Offset: 0x001B1600
		public void ReCreateList(int type)
		{
			List<CustomSelectInfo> lst;
			if (type == 0)
			{
				lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_pattern, ChaListDefine.KeyType.Unknown);
			}
			else
			{
				lst = CvsBase.CreateSelectList(ChaListDefine.CategoryNo.st_hairmeshptn, ChaListDefine.KeyType.Unknown);
			}
			this.sscPattern.CreateList(lst);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x001B323E File Offset: 0x001B163E
		private void Awake()
		{
			this.canvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x001B324C File Offset: 0x001B164C
		private void Start()
		{
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.customBase.customCtrl.showPattern = false;
				});
			}
		}

		// Token: 0x040041DA RID: 16858
		[SerializeField]
		private CustomSelectScrollController sscPattern;

		// Token: 0x040041DB RID: 16859
		[SerializeField]
		private Button btnClose;

		// Token: 0x040041DC RID: 16860
		private CanvasGroup canvasGroup;

		// Token: 0x040041E0 RID: 16864
		public Action<int, int> onSelect;
	}
}
