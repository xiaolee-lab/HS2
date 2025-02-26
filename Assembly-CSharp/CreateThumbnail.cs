using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Manager;
using UnityEngine;

// Token: 0x0200098F RID: 2447
public class CreateThumbnail : BaseLoader
{
	// Token: 0x0600464D RID: 17997 RVA: 0x001AE34C File Offset: 0x001AC74C
	private void Start()
	{
		this.ReloadChara(1);
		ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
		Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.facepaint_layout);
		this.dictFacePaintLayout = (from dict in categoryInfo
		select new CreateThumbnail.FacePaintLayout
		{
			index = dict.Value.Id,
			x = dict.Value.GetInfoFloat(ChaListDefine.KeyType.PosX),
			y = dict.Value.GetInfoFloat(ChaListDefine.KeyType.PosY),
			s = dict.Value.GetInfoFloat(ChaListDefine.KeyType.Scale)
		}).ToDictionary((CreateThumbnail.FacePaintLayout v) => v.index, (CreateThumbnail.FacePaintLayout v) => v);
		categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mole_layout);
		this.dictMoleLayout = (from dict in categoryInfo
		select new CreateThumbnail.MoleLayout
		{
			index = dict.Value.Id,
			x = dict.Value.GetInfoFloat(ChaListDefine.KeyType.PosX),
			y = dict.Value.GetInfoFloat(ChaListDefine.KeyType.PosY),
			s = dict.Value.GetInfoFloat(ChaListDefine.KeyType.Scale)
		}).ToDictionary((CreateThumbnail.MoleLayout v) => v.index, (CreateThumbnail.MoleLayout v) => v);
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x001AE44C File Offset: 0x001AC84C
	public void ReloadChara(int sex)
	{
		if (this.chaCtrl)
		{
			Singleton<Character>.Instance.DeleteChara(this.chaCtrl, false);
		}
		this.chaCtrl = Singleton<Character>.Instance.CreateChara((byte)sex, base.gameObject, 0, null);
		int num = Enum.GetNames(typeof(ChaFileDefine.ClothesKind)).Length;
		for (int i = 0; i < num; i++)
		{
			this.chaCtrl.nowCoordinate.clothes.parts[i].id = 0;
		}
		this.chaCtrl.releaseCustomInputTexture = false;
		this.chaCtrl.Load(false);
		this.chaCtrl.hideMoz = true;
		this.chaCtrl.loadWithDefaultColorAndPtn = true;
		this.chaCtrl.ChangeEyesOpenMax(1f);
		this.chaCtrl.ChangeEyesBlinkFlag(false);
		string assetBundleName = ChaABDefine.CustomAnimAssetBundle(sex);
		string assetName = ChaABDefine.CustomAnimAsset(sex);
		this.chaCtrl.LoadAnimation(assetBundleName, assetName, string.Empty);
		this.chaCtrl.AnimPlay("mannequin");
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x001AE554 File Offset: 0x001AC954
	private void Update()
	{
		if (QualitySettings.shadowDistance != 80f)
		{
			QualitySettings.shadowDistance = 80f;
		}
	}

	// Token: 0x04004155 RID: 16725
	public Dictionary<int, CreateThumbnail.FacePaintLayout> dictFacePaintLayout;

	// Token: 0x04004156 RID: 16726
	public Dictionary<int, CreateThumbnail.MoleLayout> dictMoleLayout;

	// Token: 0x04004157 RID: 16727
	public CameraControl_Ver2 camCtrl;

	// Token: 0x04004158 RID: 16728
	public Camera camMain;

	// Token: 0x04004159 RID: 16729
	public Camera camBack;

	// Token: 0x0400415A RID: 16730
	public Camera camFront;

	// Token: 0x0400415B RID: 16731
	public GameObject objImgBack;

	// Token: 0x0400415C RID: 16732
	public GameObject objImgFront;

	// Token: 0x0400415D RID: 16733
	public ChaControl chaCtrl;

	// Token: 0x02000990 RID: 2448
	public class FacePaintLayout
	{
		// Token: 0x04004164 RID: 16740
		public int index = -1;

		// Token: 0x04004165 RID: 16741
		public float x;

		// Token: 0x04004166 RID: 16742
		public float y;

		// Token: 0x04004167 RID: 16743
		public float s;
	}

	// Token: 0x02000991 RID: 2449
	public class MoleLayout
	{
		// Token: 0x04004168 RID: 16744
		public int index = -1;

		// Token: 0x04004169 RID: 16745
		public float x;

		// Token: 0x0400416A RID: 16746
		public float y;

		// Token: 0x0400416B RID: 16747
		public float s;
	}
}
