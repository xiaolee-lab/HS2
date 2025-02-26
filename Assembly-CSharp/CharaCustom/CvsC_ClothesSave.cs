using System;
using System.IO;
using AIProject;
using AIProject.Scene;
using Manager;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009FB RID: 2555
	public class CvsC_ClothesSave : CvsBase
	{
		// Token: 0x06004B7F RID: 19327 RVA: 0x001CD7F9 File Offset: 0x001CBBF9
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = true;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x001CD823 File Offset: 0x001CBC23
		public void UpdateClothesList()
		{
			this.clothesLoadWin.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, true);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x001CD848 File Offset: 0x001CBC48
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsClothesSaveDelete += this.UpdateClothesList;
			this.UpdateClothesList();
			this.clothesLoadWin.btnDisableNotSelect01 = true;
			this.clothesLoadWin.btnDisableNotSelect02 = false;
			this.clothesLoadWin.btnDisableNotSelect03 = true;
			this.clothesLoadWin.onClick01 = delegate(CustomClothesFileInfo info)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "本当に削除しますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					this.clothesLoadWin.SelectInfoClear();
					if (File.Exists(info.FullPath))
					{
						File.Delete(info.FullPath);
					}
					this.customBase.updateCvsClothesSaveDelete = true;
					this.customBase.updateCvsClothesLoad = true;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
			this.clothesLoadWin.onClick02 = delegate(CustomClothesFileInfo info)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				if (null != this.clothesNameInput)
				{
					this.clothesNameInput.SetupInputCoordinateNameWindow(string.Empty);
					this.clothesNameInput.actEntry = delegate(string buf)
					{
						this.createCoordinateFile.CreateCoordinateFile(string.Empty, buf, false);
					};
				}
			};
			this.clothesLoadWin.onClick03 = delegate(CustomClothesFileInfo info)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				ConfirmScene.Sentence = "本当に上書きしますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					if (null != this.clothesNameInput)
					{
						this.clothesNameInput.SetupInputCoordinateNameWindow(info.name);
						this.clothesNameInput.actEntry = delegate(string buf)
						{
							this.createCoordinateFile.CreateCoordinateFile(info.FullPath, buf, true);
						};
					}
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
		}

		// Token: 0x04004567 RID: 17767
		[SerializeField]
		private CustomClothesWindow clothesLoadWin;

		// Token: 0x04004568 RID: 17768
		[SerializeField]
		private CvsC_ClothesInput clothesNameInput;

		// Token: 0x04004569 RID: 17769
		[SerializeField]
		private CvsC_CreateCoordinateFile createCoordinateFile;
	}
}
