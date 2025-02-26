using System;
using Illusion.Game;
using UniRx;

namespace UploaderSystem
{
	// Token: 0x02000FFC RID: 4092
	public class DownloadScene : BaseLoader
	{
		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x060089A0 RID: 35232 RVA: 0x00399A01 File Offset: 0x00397E01
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x060089A1 RID: 35233 RVA: 0x00399A08 File Offset: 0x00397E08
		private NetCacheControl cacheCtrl
		{
			get
			{
				return (!Singleton<NetworkInfo>.IsInstance()) ? null : this.netInfo.cacheCtrl;
			}
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x00399A28 File Offset: 0x00397E28
		private void Start()
		{
			Observable.FromCoroutine(() => this.phpCtrl.GetBaseInfo(false), false).Subscribe(delegate(Unit __)
			{
			}, delegate(Exception err)
			{
			}, delegate()
			{
				this.uiCtrl.changeSearchSetting = true;
			});
			Illusion.Game.Utils.Sound.Play(new Illusion.Game.Utils.Sound.SettingBGM
			{
				assetBundleName = "sound/data/bgm/00.unity3d",
				assetName = "ai_bgm_10"
			});
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x00399AB6 File Offset: 0x00397EB6
		private void Update()
		{
		}

		// Token: 0x04006FB1 RID: 28593
		public DownPhpControl phpCtrl;

		// Token: 0x04006FB2 RID: 28594
		public DownUIControl uiCtrl;
	}
}
