using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using MessagePack;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A03 RID: 2563
	public class CvsO_Fusion : CvsBase
	{
		// Token: 0x06004BF2 RID: 19442 RVA: 0x001D2134 File Offset: 0x001D0534
		public void UpdateCharasList()
		{
			List<CustomCharaFileInfo> lst = CustomCharaFileInfoAssist.CreateCharaFileInfoList(0 == base.chaCtrl.sex, 1 == base.chaCtrl.sex, true, true, false, false);
			this.charaLoadWinA.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, false, lst);
			this.charaLoadWinB.UpdateWindow(base.customBase.modeNew, (int)base.customBase.modeSex, false, lst);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x001D21AD File Offset: 0x001D05AD
		public int RandomIntWhich(int a, int b)
		{
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				return a;
			}
			return b;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x001D21C0 File Offset: 0x001D05C0
		public Color ColorBlend(Color a, Color b, float rate)
		{
			return new Color(Mathf.Lerp(a.r, b.r, rate), Mathf.Lerp(a.g, b.g, rate), Mathf.Lerp(a.b, b.b, rate), Mathf.Lerp(a.a, b.a, rate));
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x001D2224 File Offset: 0x001D0624
		public void FusionProc(string pathA, string pathB)
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			chaFileControl.LoadCharaFile(pathA, base.customBase.modeSex, true, true);
			ChaFileControl chaFileControl2 = new ChaFileControl();
			chaFileControl2.LoadCharaFile(pathB, base.customBase.modeSex, true, true);
			ChaFileFace face = chaFileControl.custom.face;
			ChaFileFace face2 = chaFileControl2.custom.face;
			float t = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			float num = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			float num2 = 0.5f + UnityEngine.Random.Range(-0.2f, 0.2f);
			for (int i = 0; i < base.face.shapeValueFace.Length; i++)
			{
				base.face.shapeValueFace[i] = Mathf.Lerp(face.shapeValueFace[i], face2.shapeValueFace[i], num2);
			}
			base.face.headId = this.RandomIntWhich(face.headId, face2.headId);
			base.face.skinId = this.RandomIntWhich(face.skinId, face2.skinId);
			base.face.detailId = this.RandomIntWhich(face.detailId, face2.detailId);
			base.face.detailPower = Mathf.Lerp(face.detailPower, face2.detailPower, t);
			base.face.eyebrowId = this.RandomIntWhich(face.eyebrowId, face2.eyebrowId);
			base.face.eyebrowColor = this.ColorBlend(face.eyebrowColor, face2.eyebrowColor, num);
			base.face.eyebrowLayout = ((UnityEngine.Random.Range(0, 2) != 0) ? face2.eyebrowLayout : face.eyebrowLayout);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.face.eyebrowTilt = Mathf.Lerp(face.eyebrowTilt, face2.eyebrowTilt, num2);
			bool flag = (UnityEngine.Random.Range(0, 2) != 0) ? face2.pupilSameSetting : face.pupilSameSetting;
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int j = 0; j < 2; j++)
			{
				if (flag && j == 1)
				{
					base.face.pupil[j].whiteColor = base.face.pupil[0].whiteColor;
				}
				else
				{
					base.face.pupil[j].whiteColor = this.ColorBlend(face.pupil[j].whiteColor, face2.pupil[j].whiteColor, num2);
				}
			}
			for (int k = 0; k < 2; k++)
			{
				if (flag && k == 1)
				{
					base.face.pupil[k].pupilId = base.face.pupil[0].pupilId;
				}
				else
				{
					base.face.pupil[k].pupilId = this.RandomIntWhich(face.pupil[k].pupilId, face2.pupil[k].pupilId);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int l = 0; l < 2; l++)
			{
				if (flag && l == 1)
				{
					base.face.pupil[l].pupilColor = base.face.pupil[0].pupilColor;
				}
				else
				{
					base.face.pupil[l].pupilColor = this.ColorBlend(face.pupil[l].pupilColor, face2.pupil[l].pupilColor, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int m = 0; m < 2; m++)
			{
				if (flag && m == 1)
				{
					base.face.pupil[m].pupilW = base.face.pupil[0].pupilW;
				}
				else
				{
					base.face.pupil[m].pupilW = Mathf.Lerp(face.pupil[m].pupilW, face2.pupil[m].pupilW, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int n = 0; n < 2; n++)
			{
				if (flag && n == 1)
				{
					base.face.pupil[n].pupilH = base.face.pupil[0].pupilH;
				}
				else
				{
					base.face.pupil[n].pupilH = Mathf.Lerp(face.pupil[n].pupilH, face2.pupil[n].pupilH, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int num3 = 0; num3 < 2; num3++)
			{
				if (flag && num3 == 1)
				{
					base.face.pupil[num3].pupilEmission = base.face.pupil[0].pupilEmission;
				}
				else
				{
					base.face.pupil[num3].pupilEmission = Mathf.Lerp(face.pupil[num3].pupilEmission, face2.pupil[num3].pupilEmission, num2);
				}
			}
			for (int num4 = 0; num4 < 2; num4++)
			{
				if (flag && num4 == 1)
				{
					base.face.pupil[num4].blackId = base.face.pupil[0].blackId;
				}
				else
				{
					base.face.pupil[num4].blackId = this.RandomIntWhich(face.pupil[num4].blackId, face2.pupil[num4].blackId);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int num5 = 0; num5 < 2; num5++)
			{
				if (flag && num5 == 1)
				{
					base.face.pupil[num5].blackColor = base.face.pupil[0].blackColor;
				}
				else
				{
					base.face.pupil[num5].blackColor = this.ColorBlend(face.pupil[num5].blackColor, face2.pupil[num5].blackColor, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int num6 = 0; num6 < 2; num6++)
			{
				if (flag && num6 == 1)
				{
					base.face.pupil[num6].blackW = base.face.pupil[0].blackW;
				}
				else
				{
					base.face.pupil[num6].blackW = Mathf.Lerp(face.pupil[num6].blackW, face2.pupil[num6].blackW, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			for (int num7 = 0; num7 < 2; num7++)
			{
				if (flag && num7 == 1)
				{
					base.face.pupil[num7].blackH = base.face.pupil[0].blackH;
				}
				else
				{
					base.face.pupil[num7].blackH = Mathf.Lerp(face.pupil[num7].blackH, face2.pupil[num7].blackH, num2);
				}
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.face.pupilY = Mathf.Lerp(face.pupilY, face2.pupilY, num2);
			base.face.hlId = this.RandomIntWhich(face.hlId, face2.hlId);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.face.hlColor = this.ColorBlend(face.hlColor, face2.hlColor, num2);
			base.face.hlLayout = ((UnityEngine.Random.Range(0, 2) != 0) ? face2.hlLayout : face.hlLayout);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.face.hlTilt = Mathf.Lerp(face.hlTilt, face2.hlTilt, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.face.whiteShadowScale = Mathf.Lerp(face.whiteShadowScale, face2.whiteShadowScale, num2);
			base.face.eyelashesId = this.RandomIntWhich(face.eyelashesId, face2.eyelashesId);
			base.face.eyelashesColor = this.ColorBlend(face.eyelashesColor, face2.eyelashesColor, num);
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.moleId = face.moleId;
				base.face.moleColor = face.moleColor;
				base.face.moleLayout = face.moleLayout;
			}
			else
			{
				base.face.moleId = face2.moleId;
				base.face.moleColor = face2.moleColor;
				base.face.moleLayout = face2.moleLayout;
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.makeup.eyeshadowId = face.makeup.eyeshadowId;
				base.face.makeup.eyeshadowColor = face.makeup.eyeshadowColor;
				base.face.makeup.eyeshadowGloss = face.makeup.eyeshadowGloss;
			}
			else
			{
				base.face.makeup.eyeshadowId = face2.makeup.eyeshadowId;
				base.face.makeup.eyeshadowColor = face2.makeup.eyeshadowColor;
				base.face.makeup.eyeshadowGloss = face2.makeup.eyeshadowGloss;
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.makeup.cheekId = face.makeup.cheekId;
				base.face.makeup.cheekColor = face.makeup.cheekColor;
				base.face.makeup.cheekGloss = face.makeup.cheekGloss;
			}
			else
			{
				base.face.makeup.cheekId = face2.makeup.cheekId;
				base.face.makeup.cheekColor = face2.makeup.cheekColor;
				base.face.makeup.cheekGloss = face2.makeup.cheekGloss;
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.makeup.lipId = face.makeup.lipId;
				base.face.makeup.lipColor = face.makeup.lipColor;
				base.face.makeup.lipGloss = face.makeup.lipGloss;
			}
			else
			{
				base.face.makeup.lipId = face2.makeup.lipId;
				base.face.makeup.lipColor = face2.makeup.lipColor;
				base.face.makeup.lipGloss = face2.makeup.lipGloss;
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.makeup.paintInfo[0].Copy(face.makeup.paintInfo[0]);
			}
			else
			{
				base.face.makeup.paintInfo[0].Copy(face2.makeup.paintInfo[0]);
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.face.makeup.paintInfo[1].Copy(face.makeup.paintInfo[1]);
			}
			else
			{
				base.face.makeup.paintInfo[1].Copy(face2.makeup.paintInfo[1]);
			}
			if (base.chaCtrl.sex == 0)
			{
				if (UnityEngine.Random.Range(0, 2) == 0)
				{
					base.face.beardId = face.beardId;
					base.face.beardColor = face.beardColor;
				}
				else
				{
					base.face.beardId = face2.beardId;
					base.face.beardColor = face2.beardColor;
				}
			}
			ChaFileBody body = chaFileControl.custom.body;
			ChaFileBody body2 = chaFileControl2.custom.body;
			num2 = 0.5f + UnityEngine.Random.Range(-0.2f, 0.2f);
			for (int num8 = 0; num8 < base.body.shapeValueBody.Length; num8++)
			{
				base.body.shapeValueBody[num8] = Mathf.Lerp(body.shapeValueBody[num8], body2.shapeValueBody[num8], num2);
			}
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.bustSoftness = Mathf.Lerp(body.bustSoftness, body2.bustSoftness, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.bustWeight = Mathf.Lerp(body.bustWeight, body2.bustWeight, num2);
			base.body.skinId = this.RandomIntWhich(body.skinId, body2.skinId);
			base.body.detailId = this.RandomIntWhich(body.detailId, body2.detailId);
			base.body.detailPower = Mathf.Lerp(body.detailPower, body2.detailPower, t);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.skinColor = this.ColorBlend(body.skinColor, body2.skinColor, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.skinGlossPower = Mathf.Lerp(body.skinGlossPower, body2.skinGlossPower, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.skinMetallicPower = Mathf.Lerp(body.skinMetallicPower, body2.skinMetallicPower, num2);
			base.body.sunburnId = this.RandomIntWhich(body.sunburnId, body2.sunburnId);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.sunburnColor = this.ColorBlend(body.sunburnColor, body2.sunburnColor, num2);
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.body.paintInfo[0].Copy(body.paintInfo[0]);
			}
			else
			{
				base.body.paintInfo[0].Copy(body2.paintInfo[0]);
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.body.paintInfo[1].Copy(body.paintInfo[1]);
			}
			else
			{
				base.body.paintInfo[1].Copy(body2.paintInfo[1]);
			}
			base.body.nipId = this.RandomIntWhich(body.nipId, body2.nipId);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.nipColor = this.ColorBlend(body.nipColor, body2.nipColor, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.nipGlossPower = Mathf.Lerp(body.nipGlossPower, body2.nipGlossPower, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.areolaSize = Mathf.Lerp(body.areolaSize, body2.areolaSize, num2);
			base.body.underhairId = this.RandomIntWhich(body.underhairId, body2.underhairId);
			base.body.underhairColor = this.ColorBlend(body.underhairColor, body2.underhairColor, num);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.nailColor = this.ColorBlend(body.nailColor, body2.nailColor, num2);
			num2 = 0.5f + UnityEngine.Random.Range(-0.5f, 0.5f);
			base.body.nailGlossPower = Mathf.Lerp(body.nailGlossPower, body2.nailGlossPower, num2);
			ChaFileHair hair = chaFileControl.custom.hair;
			ChaFileHair hair2 = chaFileControl2.custom.hair;
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				byte[] bytes = MessagePackSerializer.Serialize<ChaFileHair>(hair);
				base.chaCtrl.chaFile.custom.hair = MessagePackSerializer.Deserialize<ChaFileHair>(bytes);
			}
			else
			{
				byte[] bytes2 = MessagePackSerializer.Serialize<ChaFileHair>(hair2);
				base.chaCtrl.chaFile.custom.hair = MessagePackSerializer.Deserialize<ChaFileHair>(bytes2);
			}
			for (int num9 = 0; num9 < base.hair.parts.Length; num9++)
			{
				base.hair.parts[num9].baseColor = this.ColorBlend(hair.parts[num9].baseColor, hair2.parts[num9].baseColor, num);
				base.hair.parts[num9].topColor = this.ColorBlend(hair.parts[num9].topColor, hair2.parts[num9].topColor, num);
				base.hair.parts[num9].underColor = this.ColorBlend(hair.parts[num9].underColor, hair2.parts[num9].underColor, num);
				base.hair.parts[num9].specular = this.ColorBlend(hair.parts[num9].specular, hair2.parts[num9].specular, num);
				base.hair.parts[num9].smoothness = Mathf.Lerp(hair.parts[num9].smoothness, hair2.parts[num9].smoothness, num);
				base.hair.parts[num9].metallic = Mathf.Lerp(hair.parts[num9].metallic, hair2.parts[num9].metallic, num);
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				base.chaCtrl.chaFile.CopyCoordinate(chaFileControl.coordinate);
			}
			else
			{
				base.chaCtrl.chaFile.CopyCoordinate(chaFileControl2.coordinate);
			}
			base.chaCtrl.ChangeNowCoordinate(false, true);
			Singleton<Character>.Instance.customLoadGCClear = false;
			base.chaCtrl.Reload(false, false, false, false, true);
			Singleton<Character>.Instance.customLoadGCClear = true;
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x001D35C8 File Offset: 0x001D19C8
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFusion += this.UpdateCustomUI;
			this.UpdateCharasList();
			if (null != this.btnFusion)
			{
				this.btnFusion.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					CustomCharaScrollController.ScrollData selectInfo = this.charaLoadWinA.GetSelectInfo();
					CustomCharaScrollController.ScrollData selectInfo2 = this.charaLoadWinB.GetSelectInfo();
					this.FusionProc(selectInfo.info.FileName, selectInfo2.info.FileName);
					this.isFusion = true;
				});
				this.btnFusion.UpdateAsObservable().Subscribe(delegate(Unit _)
				{
					CustomCharaScrollController.ScrollData selectInfo = this.charaLoadWinA.GetSelectInfo();
					CustomCharaScrollController.ScrollData selectInfo2 = this.charaLoadWinB.GetSelectInfo();
					this.btnFusion.interactable = (selectInfo != null && null != selectInfo2);
				});
			}
			if (null != this.btnExit)
			{
				this.btnExit.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					base.customBase.customCtrl.showFusionCvs = false;
					base.customBase.customCtrl.showMainCvs = true;
					this.charaLoadWinA.SelectInfoClear();
					this.charaLoadWinB.SelectInfoClear();
					if (this.isFusion)
					{
						base.customBase.updateCustomUI = true;
						for (int i = 0; i < 20; i++)
						{
							base.customBase.ChangeAcsSlotName(i);
						}
						base.customBase.SetUpdateToggleSetting();
						base.customBase.forceUpdateAcsList = true;
					}
					this.isFusion = false;
				});
			}
		}

		// Token: 0x040045BD RID: 17853
		[SerializeField]
		private CustomCharaWindow charaLoadWinA;

		// Token: 0x040045BE RID: 17854
		[SerializeField]
		private CustomCharaWindow charaLoadWinB;

		// Token: 0x040045BF RID: 17855
		[SerializeField]
		private Button btnFusion;

		// Token: 0x040045C0 RID: 17856
		[SerializeField]
		private Button btnExit;

		// Token: 0x040045C1 RID: 17857
		private bool isFusion;
	}
}
