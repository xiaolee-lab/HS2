using System;
using Illusion.Game;
using UniRx;

namespace UploaderSystem
{
	// Token: 0x02000FF5 RID: 4085
	public class UploadScene : BaseLoader
	{
		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06008976 RID: 35190 RVA: 0x00393B15 File Offset: 0x00391F15
		private NetworkInfo netInfo
		{
			get
			{
				return Singleton<NetworkInfo>.Instance;
			}
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06008977 RID: 35191 RVA: 0x00393B1C File Offset: 0x00391F1C
		private NetCacheControl cacheCtrl
		{
			get
			{
				return (!Singleton<NetworkInfo>.IsInstance()) ? null : this.netInfo.cacheCtrl;
			}
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x00393B3C File Offset: 0x00391F3C
		private void Start()
		{
			Observable.FromCoroutine(() => this.phpCtrl.GetBaseInfo(true), false).Subscribe(delegate(Unit __)
			{
			}, delegate(Exception err)
			{
			}, delegate()
			{
				this.uiCtrl.UpdateProfile();
			});
			Illusion.Game.Utils.Sound.Play(new Illusion.Game.Utils.Sound.SettingBGM
			{
				assetBundleName = "sound/data/bgm/00.unity3d",
				assetName = "ai_bgm_10"
			});
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x00393BCA File Offset: 0x00391FCA
		private void Update()
		{
		}

		// Token: 0x04006F6A RID: 28522
		public UpPhpControl phpCtrl;

		// Token: 0x04006F6B RID: 28523
		public UpUIControl uiCtrl;
	}
}
