using System;
using System.Linq;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A09 RID: 2569
	public class CustomChangeMainMenu : UI_ToggleGroupCtrl
	{
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x001D6E5D File Offset: 0x001D525D
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x001D6E64 File Offset: 0x001D5264
		public override void Start()
		{
			base.Start();
			if (this.items.Any<UI_ToggleGroupCtrl.ItemInfo>())
			{
				(from item in this.items.Select((UI_ToggleGroupCtrl.ItemInfo val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null && item.val.tglItem != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.tglItem.OnValueChangedAsObservable().Skip(1)
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
						this.ChangeWindowSetting(item.idx);
					});
				});
			}
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x001D6EE7 File Offset: 0x001D52E7
		public bool IsSelectAccessory()
		{
			return this.items[4].tglItem.isOn;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x001D6EFC File Offset: 0x001D52FC
		public void ChangeWindowSetting(int no)
		{
			switch (no)
			{
			case 0:
				this.customBase.ChangeClothesStateAuto(0);
				this.customBase.customCtrl.showColorCvs = false;
				this.customBase.customCtrl.showFileList = false;
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showAcsControllerAll = false;
				this.customBase.showHairController = false;
				break;
			case 1:
				this.customBase.ChangeClothesStateAuto(2);
				this.customBase.customCtrl.showColorCvs = false;
				this.customBase.customCtrl.showFileList = false;
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showAcsControllerAll = false;
				this.customBase.showHairController = false;
				break;
			case 2:
				this.customBase.ChangeClothesStateAuto(0);
				this.customBase.customCtrl.showColorCvs = false;
				this.customBase.customCtrl.showFileList = false;
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showAcsControllerAll = false;
				if (this.cvsH_Hair && this.customBase.chaCtrl)
				{
					this.customBase.showHairController = (null != this.customBase.chaCtrl.cmpHair[this.cvsH_Hair.SNo]);
				}
				break;
			case 3:
				this.customBase.ChangeClothesStateAuto(0);
				this.customBase.customCtrl.showColorCvs = false;
				if (this.cvgClothesSave.alpha == 1f || this.cvgClothesLoad.alpha == 1f)
				{
					this.customBase.customCtrl.showFileList = true;
				}
				else
				{
					this.customBase.customCtrl.showFileList = false;
				}
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showAcsControllerAll = false;
				this.customBase.showHairController = false;
				break;
			case 4:
				this.customBase.ChangeClothesStateAuto(0);
				this.customBase.customCtrl.showColorCvs = false;
				this.customBase.customCtrl.showFileList = false;
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showHairController = false;
				if (this.cvsA_Slot && this.customBase.chaCtrl)
				{
					this.customBase.showAcsControllerAll = this.customBase.chaCtrl.IsAccessory(this.cvsA_Slot.SNo);
				}
				break;
			case 5:
				this.customBase.ChangeClothesStateAuto(0);
				this.customBase.customCtrl.showColorCvs = false;
				if (this.cvgCharaSave.alpha == 1f || this.cvgCharaLoad.alpha == 1f)
				{
					this.customBase.customCtrl.showFileList = true;
				}
				else
				{
					this.customBase.customCtrl.showFileList = false;
				}
				this.customBase.customCtrl.showPattern = false;
				this.customBase.showAcsControllerAll = false;
				this.customBase.showHairController = false;
				break;
			}
		}

		// Token: 0x04004624 RID: 17956
		[SerializeField]
		private CanvasGroup cvgClothesSave;

		// Token: 0x04004625 RID: 17957
		[SerializeField]
		private CanvasGroup cvgClothesLoad;

		// Token: 0x04004626 RID: 17958
		[SerializeField]
		private CanvasGroup cvgCharaSave;

		// Token: 0x04004627 RID: 17959
		[SerializeField]
		private CanvasGroup cvgCharaLoad;

		// Token: 0x04004628 RID: 17960
		[SerializeField]
		private CvsA_Slot cvsA_Slot;

		// Token: 0x04004629 RID: 17961
		[SerializeField]
		private CvsH_Hair cvsH_Hair;
	}
}
