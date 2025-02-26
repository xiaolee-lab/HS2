using System;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000862 RID: 2146
	public class HSetting : BaseSetting
	{
		// Token: 0x060036C4 RID: 14020 RVA: 0x00144938 File Offset: 0x00142D38
		public override void Init()
		{
			HSystem hdata = Config.HData;
			GraphicSystem gdata = Config.GraphicData;
			base.LinkToggleArray(this.drawToggles, delegate(int i)
			{
				hdata.Visible = (i == 0);
			});
			base.LinkToggleArray(this.sonToggles, delegate(int i)
			{
				hdata.Son = (i == 0);
			});
			base.LinkToggleArray(this.clothToggles, delegate(int i)
			{
				hdata.Cloth = (i == 0);
			});
			base.LinkToggleArray(this.accessoryToggles, delegate(int i)
			{
				hdata.Accessory = (i == 0);
			});
			base.LinkToggleArray(this.shoesToggles, delegate(int i)
			{
				hdata.Shoes = (i == 0);
			});
			base.LinkToggleArray(this.silhouetteToggles, delegate(int i)
			{
				gdata.SimpleBody = (i == 0);
			});
			this.silhouetteCololr.actUpdateColor = delegate(Color c)
			{
				gdata.SilhouetteColor = c;
			};
			base.LinkToggleArray(this.siruToggles, delegate(int i)
			{
				hdata.Siru = i;
			});
			base.LinkToggleArray(this.urineToggles, delegate(int i)
			{
				hdata.Urine = (i == 0);
			});
			base.LinkToggleArray(this.sioToggles, delegate(int i)
			{
				hdata.Sio = (i == 0);
			});
			base.LinkToggleArray(this.glossToggles, delegate(int i)
			{
				hdata.Gloss = (i == 0);
			});
			base.LinkToggleArray(this.gaugeToggles, delegate(int i)
			{
				hdata.FeelingGauge = (i == 0);
			});
			base.LinkToggleArray(this.guideToggles, delegate(int i)
			{
				hdata.ActionGuide = (i == 0);
			});
			base.LinkToggleArray(this.muneToggles, delegate(int i)
			{
				hdata.MenuIcon = (i == 0);
			});
			base.LinkToggleArray(this.finishToggles, delegate(int i)
			{
				hdata.FinishButton = (i == 0);
			});
			base.LinkToggleArray(this.initCameraToggles, delegate(int i)
			{
				hdata.InitCamera = (i == 0);
			});
			base.LinkToggleArray(this.eyeDir0Toggles, delegate(int i)
			{
				hdata.EyeDir0 = (i == 0);
			});
			base.LinkToggleArray(this.neckDir0Toggles, delegate(int i)
			{
				hdata.NeckDir0 = (i == 0);
			});
			base.LinkToggleArray(this.eyeDir1Toggles, delegate(int i)
			{
				hdata.EyeDir1 = (i == 0);
			});
			base.LinkToggleArray(this.neckDir1Toggles, delegate(int i)
			{
				hdata.NeckDir1 = (i == 0);
			});
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x00144B40 File Offset: 0x00142F40
		protected override void ValueToUI()
		{
			HSystem hdata = Config.HData;
			GraphicSystem gdata = Config.GraphicData;
			base.SetToggleUIArray(this.drawToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Visible) : hdata.Visible);
			});
			base.SetToggleUIArray(this.sonToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Son) : hdata.Son);
			});
			base.SetToggleUIArray(this.clothToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Cloth) : hdata.Cloth);
			});
			base.SetToggleUIArray(this.accessoryToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Accessory) : hdata.Accessory);
			});
			base.SetToggleUIArray(this.shoesToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Shoes) : hdata.Shoes);
			});
			base.SetToggleUIArray(this.silhouetteToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!gdata.SimpleBody) : gdata.SimpleBody);
			});
			this.silhouetteCololr.SetColor(gdata.SilhouetteColor);
			base.SetToggleUIArray(this.siruToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = (index == hdata.Siru);
			});
			base.SetToggleUIArray(this.urineToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Urine) : hdata.Urine);
			});
			base.SetToggleUIArray(this.sioToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Sio) : hdata.Sio);
			});
			base.SetToggleUIArray(this.glossToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.Gloss) : hdata.Gloss);
			});
			base.SetToggleUIArray(this.gaugeToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.FeelingGauge) : hdata.FeelingGauge);
			});
			base.SetToggleUIArray(this.guideToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.ActionGuide) : hdata.ActionGuide);
			});
			base.SetToggleUIArray(this.muneToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.MenuIcon) : hdata.MenuIcon);
			});
			base.SetToggleUIArray(this.finishToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.FinishButton) : hdata.FinishButton);
			});
			base.SetToggleUIArray(this.initCameraToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.InitCamera) : hdata.InitCamera);
			});
			base.SetToggleUIArray(this.eyeDir0Toggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.EyeDir0) : hdata.EyeDir0);
			});
			base.SetToggleUIArray(this.neckDir0Toggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.NeckDir0) : hdata.NeckDir0);
			});
			base.SetToggleUIArray(this.eyeDir1Toggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.EyeDir1) : hdata.EyeDir1);
			});
			base.SetToggleUIArray(this.neckDir1Toggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!hdata.NeckDir1) : hdata.NeckDir1);
			});
		}

		// Token: 0x0400374D RID: 14157
		[Header("主人公の表示")]
		[SerializeField]
		private Toggle[] drawToggles;

		// Token: 0x0400374E RID: 14158
		[Header("主人公の男根")]
		[SerializeField]
		private Toggle[] sonToggles;

		// Token: 0x0400374F RID: 14159
		[Header("主人公の服")]
		[SerializeField]
		private Toggle[] clothToggles;

		// Token: 0x04003750 RID: 14160
		[Header("主人公のアクセサリー")]
		[SerializeField]
		private Toggle[] accessoryToggles;

		// Token: 0x04003751 RID: 14161
		[Header("主人公の靴")]
		[SerializeField]
		private Toggle[] shoesToggles;

		// Token: 0x04003752 RID: 14162
		[Header("主人公を単色化")]
		[SerializeField]
		private Toggle[] silhouetteToggles;

		// Token: 0x04003753 RID: 14163
		[Header("単色")]
		[SerializeField]
		private UI_SampleColor silhouetteCololr;

		// Token: 0x04003754 RID: 14164
		[Header("汁")]
		[SerializeField]
		private Toggle[] siruToggles;

		// Token: 0x04003755 RID: 14165
		[Header("尿")]
		[SerializeField]
		private Toggle[] urineToggles;

		// Token: 0x04003756 RID: 14166
		[Header("潮吹き")]
		[SerializeField]
		private Toggle[] sioToggles;

		// Token: 0x04003757 RID: 14167
		[Header("艶")]
		[SerializeField]
		private Toggle[] glossToggles;

		// Token: 0x04003758 RID: 14168
		[Header("快感ゲージ")]
		[SerializeField]
		private Toggle[] gaugeToggles;

		// Token: 0x04003759 RID: 14169
		[Header("操作ガイド")]
		[SerializeField]
		private Toggle[] guideToggles;

		// Token: 0x0400375A RID: 14170
		[Header("メニューアイコン")]
		[SerializeField]
		private Toggle[] muneToggles;

		// Token: 0x0400375B RID: 14171
		[Header("フィニッシュボタン")]
		[SerializeField]
		private Toggle[] finishToggles;

		// Token: 0x0400375C RID: 14172
		[Header("カメラ初期化判断")]
		[SerializeField]
		private Toggle[] initCameraToggles;

		// Token: 0x0400375D RID: 14173
		[Header("１人目視線")]
		[SerializeField]
		private Toggle[] eyeDir0Toggles;

		// Token: 0x0400375E RID: 14174
		[Header("１人目首の向き")]
		[SerializeField]
		private Toggle[] neckDir0Toggles;

		// Token: 0x0400375F RID: 14175
		[Header("２人目視線")]
		[SerializeField]
		private Toggle[] eyeDir1Toggles;

		// Token: 0x04003760 RID: 14176
		[Header("２人目首の向き")]
		[SerializeField]
		private Toggle[] neckDir1Toggles;
	}
}
