using System;
using AIProject;
using Manager;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A01 RID: 2561
	public class CvsO_CharaLoad : CvsBase
	{
		// Token: 0x06004BE4 RID: 19428 RVA: 0x001D1BE0 File Offset: 0x001CFFE0
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = true;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x001D1C0A File Offset: 0x001D000A
		public void UpdateCharasList()
		{
			this.charaLoadWin.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, false, null);
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x001D1C30 File Offset: 0x001D0030
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsCharaLoad += this.UpdateCharasList;
			this.UpdateCharasList();
			this.charaLoadWin.btnDisableNotSelect01 = true;
			this.charaLoadWin.btnDisableNotSelect02 = true;
			this.charaLoadWin.btnDisableNotSelect03 = true;
			this.charaLoadWin.onClick03 = delegate(CustomCharaFileInfo info, int flags)
			{
				bool flag = 0 != (flags & 1);
				bool flag2 = 0 != (flags & 2);
				bool flag3 = 0 != (flags & 4);
				bool flag4 = 0 != (flags & 8);
				bool parameter = (flags & 16) != 0 && base.customBase.modeNew;
				string fullPath = info.FullPath;
				base.chaCtrl.chaFile.LoadFileLimited(fullPath, base.chaCtrl.sex, flag, flag2, flag3, parameter, flag4);
				base.chaCtrl.ChangeNowCoordinate(false, true);
				Singleton<Character>.Instance.customLoadGCClear = false;
				base.chaCtrl.Reload(!flag4, !flag, !flag3, !flag2, true);
				Singleton<Character>.Instance.customLoadGCClear = true;
				base.customBase.updateCustomUI = true;
				for (int i = 0; i < 20; i++)
				{
					base.customBase.ChangeAcsSlotName(i);
				}
				base.customBase.SetUpdateToggleSetting();
				base.customBase.forceUpdateAcsList = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Load);
			};
		}

		// Token: 0x040045B9 RID: 17849
		[SerializeField]
		private CustomCharaWindow charaLoadWin;
	}
}
