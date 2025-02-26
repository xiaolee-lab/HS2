using System;
using AIChara;
using AIProject;
using Manager;
using MessagePack;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009FA RID: 2554
	public class CvsC_ClothesLoad : CvsBase
	{
		// Token: 0x06004B78 RID: 19320 RVA: 0x001CD521 File Offset: 0x001CB921
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = true;
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x001CD54B File Offset: 0x001CB94B
		public void UpdateClothesList()
		{
			this.clothesLoadWin.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, false);
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x001CD570 File Offset: 0x001CB970
		protected override void Start()
		{
			base.customBase.actUpdateCvsClothesLoad += this.UpdateClothesList;
			this.UpdateClothesList();
			this.clothesLoadWin.btnDisableNotSelect01 = true;
			this.clothesLoadWin.btnDisableNotSelect02 = true;
			this.clothesLoadWin.btnDisableNotSelect03 = true;
			this.clothesLoadWin.onClick01 = delegate(CustomClothesFileInfo info)
			{
				byte[] bytes = MessagePackSerializer.Serialize<ChaFileAccessory>(base.chaCtrl.nowCoordinate.accessory);
				string fullPath = info.FullPath;
				base.chaCtrl.nowCoordinate.LoadFile(fullPath);
				base.chaCtrl.nowCoordinate.accessory = MessagePackSerializer.Deserialize<ChaFileAccessory>(bytes);
				Singleton<Character>.Instance.customLoadGCClear = false;
				base.chaCtrl.Reload(false, true, true, true, true);
				Singleton<Character>.Instance.customLoadGCClear = true;
				base.customBase.updateCustomUI = true;
				base.chaCtrl.AssignCoordinate();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Load);
			};
			this.clothesLoadWin.onClick02 = delegate(CustomClothesFileInfo info)
			{
				byte[] bytes = MessagePackSerializer.Serialize<ChaFileClothes>(base.chaCtrl.nowCoordinate.clothes);
				string fullPath = info.FullPath;
				base.chaCtrl.nowCoordinate.LoadFile(fullPath);
				base.chaCtrl.nowCoordinate.clothes = MessagePackSerializer.Deserialize<ChaFileClothes>(bytes);
				Singleton<Character>.Instance.customLoadGCClear = false;
				base.chaCtrl.Reload(false, true, true, true, true);
				Singleton<Character>.Instance.customLoadGCClear = true;
				base.customBase.updateCustomUI = true;
				base.customBase.ChangeAcsSlotName(-1);
				base.customBase.forceUpdateAcsList = true;
				base.chaCtrl.AssignCoordinate();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Load);
			};
			this.clothesLoadWin.onClick03 = delegate(CustomClothesFileInfo info)
			{
				string fullPath = info.FullPath;
				base.chaCtrl.nowCoordinate.LoadFile(fullPath);
				Singleton<Character>.Instance.customLoadGCClear = false;
				base.chaCtrl.Reload(false, true, true, true, true);
				Singleton<Character>.Instance.customLoadGCClear = true;
				base.customBase.updateCustomUI = true;
				base.customBase.ChangeAcsSlotName(-1);
				base.customBase.forceUpdateAcsList = true;
				base.chaCtrl.AssignCoordinate();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Load);
			};
		}

		// Token: 0x04004566 RID: 17766
		[SerializeField]
		private CustomClothesWindow clothesLoadWin;
	}
}
