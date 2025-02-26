using System;
using System.IO;
using AIProject;
using AIProject.Scene;
using Illusion.Extensions;
using Manager;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A02 RID: 2562
	public class CvsO_CharaSave : CvsBase
	{
		// Token: 0x06004BE9 RID: 19433 RVA: 0x001D1DB6 File Offset: 0x001D01B6
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = true;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x001D1DE0 File Offset: 0x001D01E0
		public void UpdateCharasList()
		{
			this.charaLoadWin.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, true, null);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x001D1E08 File Offset: 0x001D0208
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsCharaSaveDelete += this.UpdateCharasList;
			this.UpdateCharasList();
			this.charaLoadWin.btnDisableNotSelect01 = true;
			this.charaLoadWin.btnDisableNotSelect02 = false;
			this.charaLoadWin.btnDisableNotSelect03 = true;
			this.charaLoadWin.onClick01 = delegate(CustomCharaFileInfo info)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				if (info.gameRegistration)
				{
					ConfirmScene.Sentence = "本当に削除しますか？\n" + "このキャラにはパラメータが含まれています。".Coloring("#DE4529FF").Size(24);
				}
				else
				{
					ConfirmScene.Sentence = "本当に削除しますか？";
				}
				ConfirmScene.OnClickedYes = delegate()
				{
					this.charaLoadWin.SelectInfoClear();
					if (File.Exists(info.FullPath))
					{
						File.Delete(info.FullPath);
					}
					this.customBase.updateCvsCharaSaveDelete = true;
					this.customBase.updateCvsCharaLoad = true;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
			this.charaLoadWin.onClick02 = delegate(CustomCharaFileInfo info)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				base.customBase.customCtrl.saveMode = true;
			};
			this.charaLoadWin.onClick03 = delegate(CustomCharaFileInfo info, int flags)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				if (info.gameRegistration)
				{
					ConfirmScene.Sentence = "本当に上書きしますか？\n" + "上書きするとパラメータは初期化されます。".Coloring("#DE4529FF").Size(24);
				}
				else
				{
					ConfirmScene.Sentence = "本当に上書きしますか？";
				}
				ConfirmScene.OnClickedYes = delegate()
				{
					this.customBase.customCtrl.overwriteSavePath = info.FullPath;
					this.customBase.customCtrl.saveMode = true;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			};
		}

		// Token: 0x040045BA RID: 17850
		[SerializeField]
		private CustomCharaWindow charaLoadWin;
	}
}
