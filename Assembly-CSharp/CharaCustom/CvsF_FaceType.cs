using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Manager;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009DB RID: 2523
	public class CvsF_FaceType : CvsBase
	{
		// Token: 0x06004A02 RID: 18946 RVA: 0x001C35C2 File Offset: 0x001C19C2
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x001C35EC File Offset: 0x001C19EC
		private void CalculateUI()
		{
			this.ssDetailPower.SetSliderValue(base.face.detailPower);
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x001C3604 File Offset: 0x001C1A04
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.UpdateSkinList();
			this.sscFaceType.SetToggleID(base.face.headId);
			this.sscSkinType.SetToggleID(base.face.skinId);
			this.sscDetailType.SetToggleID(base.face.detailId);
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x001C3668 File Offset: 0x001C1A68
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssDetailPower.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.face.detailPower));
			yield break;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x001C3684 File Offset: 0x001C1A84
		public void UpdateSkinList()
		{
			List<CustomSelectInfo> list = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f, ChaListDefine.KeyType.HeadID);
			list = (from x in list
			where x.limitIndex == base.face.headId
			select x).ToList<CustomSelectInfo>();
			this.sscSkinType.CreateList(list);
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x001C36DC File Offset: 0x001C1ADC
		public List<CustomPushInfo> CreateFacePresetList(ChaListDefine.CategoryNo cateNo)
		{
			Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(cateNo);
			int[] array = categoryInfo.Keys.ToArray<int>();
			List<CustomPushInfo> list = new List<CustomPushInfo>();
			for (int i = 0; i < categoryInfo.Count; i++)
			{
				list.Add(new CustomPushInfo
				{
					category = categoryInfo[array[i]].Category,
					id = categoryInfo[array[i]].Id,
					name = categoryInfo[array[i]].Name,
					assetBundle = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.ThumbAB),
					assetName = categoryInfo[array[i]].GetInfo(ChaListDefine.KeyType.Preset)
				});
			}
			return list;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x001C37A0 File Offset: 0x001C1BA0
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsFaceType += this.UpdateCustomUI;
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_head : ChaListDefine.CategoryNo.mo_head, ChaListDefine.KeyType.Unknown);
			this.sscFaceType.CreateList(lst);
			this.sscFaceType.SetToggleID(base.face.headId);
			this.sscFaceType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.headId != info.id)
				{
					base.chaCtrl.ChangeHead(info.id, false);
					this.UpdateSkinList();
					this.sscSkinType.SetToggleID(base.face.skinId);
				}
			};
			List<CustomPushInfo> lst2 = this.CreateFacePresetList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.fo_head : ChaListDefine.CategoryNo.mo_head);
			this.pscFacePreset.CreateList(lst2);
			this.pscFacePreset.onPush = delegate(CustomPushInfo info)
			{
				if (info != null)
				{
					base.face.headId = info.id;
					base.chaCtrl.chaFile.LoadFacePreset();
					Singleton<Character>.Instance.customLoadGCClear = false;
					base.chaCtrl.Reload(true, false, true, true, true);
					Singleton<Character>.Instance.customLoadGCClear = true;
					base.customBase.updateCvsFaceType = true;
					base.customBase.updateCvsFaceShapeWhole = true;
					base.customBase.updateCvsFaceShapeChin = true;
					base.customBase.updateCvsFaceShapeCheek = true;
					base.customBase.updateCvsFaceShapeEyebrow = true;
					base.customBase.updateCvsFaceShapeEyes = true;
					base.customBase.updateCvsFaceShapeNose = true;
					base.customBase.updateCvsFaceShapeMouth = true;
					base.customBase.updateCvsFaceShapeEar = true;
					base.customBase.updateCvsMole = true;
					base.customBase.updateCvsEyeLR = true;
					base.customBase.updateCvsEyeEtc = true;
					base.customBase.updateCvsEyeHL = true;
					base.customBase.updateCvsEyebrow = true;
					base.customBase.updateCvsEyelashes = true;
					base.customBase.updateCvsEyeshadow = true;
					base.customBase.updateCvsCheek = true;
					base.customBase.updateCvsLip = true;
					base.customBase.updateCvsFacePaint = true;
					base.customBase.SetUpdateToggleSetting();
				}
			};
			this.UpdateSkinList();
			this.sscSkinType.SetToggleID(base.face.skinId);
			this.sscSkinType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.skinId != info.id)
				{
					base.face.skinId = info.id;
					base.chaCtrl.AddUpdateCMFaceTexFlags(true, false, false, false, false, false, false);
					base.chaCtrl.CreateFaceTexture();
				}
			};
			lst = CvsBase.CreateSelectList((base.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.ft_detail_f : ChaListDefine.CategoryNo.mt_detail_f, ChaListDefine.KeyType.Unknown);
			this.sscDetailType.CreateList(lst);
			this.sscDetailType.SetToggleID(base.face.detailId);
			this.sscDetailType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.face.detailId != info.id)
				{
					base.face.detailId = info.id;
					base.chaCtrl.ChangeFaceDetailKind();
				}
			};
			this.ssDetailPower.onChange = delegate(float value)
			{
				base.face.detailPower = value;
				base.chaCtrl.ChangeFaceDetailPower();
			};
			this.ssDetailPower.onSetDefaultValue = (() => base.defChaCtrl.custom.face.detailPower);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x0400446F RID: 17519
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscFaceType;

		// Token: 0x04004470 RID: 17520
		[SerializeField]
		private CustomPushScrollController pscFacePreset;

		// Token: 0x04004471 RID: 17521
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscSkinType;

		// Token: 0x04004472 RID: 17522
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomSliderSet ssDetailPower;

		// Token: 0x04004473 RID: 17523
		[SerializeField]
		private CustomSelectScrollController sscDetailType;
	}
}
