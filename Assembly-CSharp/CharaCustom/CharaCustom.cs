using System;
using Illusion.Game;
using Manager;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000994 RID: 2452
	[DefaultExecutionOrder(-1)]
	public class CharaCustom : BaseLoader
	{
		// Token: 0x06004668 RID: 18024 RVA: 0x001AED31 File Offset: 0x001AD131
		private void ChangeInert(bool h)
		{
			if (null != Singleton<CustomBase>.Instance.chaCtrl)
			{
				Singleton<CustomBase>.Instance.chaCtrl.ChangeBustInert(h);
			}
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x001AED58 File Offset: 0x001AD158
		protected override void Awake()
		{
			base.Awake();
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x001AED60 File Offset: 0x001AD160
		private void Start()
		{
			if (Singleton<Map>.IsInstance())
			{
				Singleton<Map>.Instance.UpdateTexSetting = false;
			}
			this.shadowDistance = QualitySettings.shadowDistance;
			this.backLimit = QualitySettings.masterTextureLimit;
			if (QualitySettings.GetQualityLevel() / 2 == 0)
			{
				QualitySettings.masterTextureLimit = 1;
			}
			else
			{
				QualitySettings.masterTextureLimit = 0;
			}
			Singleton<Character>.Instance.customLoadGCClear = true;
			this.customCtrl.Initialize(CharaCustom.modeSex, CharaCustom.modeNew, CharaCustom.nextScene, CharaCustom.editCharaFileName);
			Illusion.Game.Utils.Sound.Play(new Illusion.Game.Utils.Sound.SettingBGM
			{
				assetBundleName = "sound/data/bgm/00.unity3d",
				assetName = "ai_bgm_10"
			});
			this.cgScene.blocksRaycasts = true;
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x001AEE10 File Offset: 0x001AD210
		private void Update()
		{
			if (QualitySettings.shadowDistance != 120f)
			{
				QualitySettings.shadowDistance = 120f;
			}
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x001AEE2C File Offset: 0x001AD22C
		private void OnDestroy()
		{
			if (Singleton<CustomBase>.IsInstance())
			{
				Singleton<CustomBase>.Instance.customSettingSave.Save();
			}
			if (Singleton<Character>.IsInstance())
			{
				Singleton<Character>.Instance.chaListCtrl.SaveItemID();
				Singleton<Character>.Instance.DeleteCharaAll();
				Singleton<Character>.Instance.EndLoadAssetBundle(false);
				Singleton<Character>.Instance.customLoadGCClear = false;
			}
			QualitySettings.shadowDistance = this.shadowDistance;
			QualitySettings.masterTextureLimit = this.backLimit;
			if (Singleton<Map>.IsInstance())
			{
				Singleton<Map>.Instance.UpdateTexSetting = true;
			}
			CharaCustom.nextScene = string.Empty;
			CharaCustom.editCharaFileName = string.Empty;
			if (CharaCustom.actEixt != null)
			{
				CharaCustom.actEixt();
			}
			CharaCustom.actEixt = null;
		}

		// Token: 0x0400417F RID: 16767
		[Button("ChangeInert", "惰性通常", new object[]
		{
			false
		})]
		public int inert00;

		// Token: 0x04004180 RID: 16768
		[Button("ChangeInert", "惰性強", new object[]
		{
			true
		})]
		public int inert01;

		// Token: 0x04004181 RID: 16769
		public static bool modeNew = true;

		// Token: 0x04004182 RID: 16770
		public static byte modeSex = 1;

		// Token: 0x04004183 RID: 16771
		public static string nextScene = string.Empty;

		// Token: 0x04004184 RID: 16772
		public static string editCharaFileName = string.Empty;

		// Token: 0x04004185 RID: 16773
		public static Action actEixt;

		// Token: 0x04004186 RID: 16774
		[SerializeField]
		private CustomControl customCtrl;

		// Token: 0x04004187 RID: 16775
		[SerializeField]
		private CanvasGroup cgScene;

		// Token: 0x04004188 RID: 16776
		private float shadowDistance = 400f;

		// Token: 0x04004189 RID: 16777
		private int backLimit;
	}
}
