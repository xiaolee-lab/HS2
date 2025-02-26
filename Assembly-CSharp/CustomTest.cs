using System;
using System.Collections.Generic;
using AIChara;
using Manager;

// Token: 0x02001364 RID: 4964
public class CustomTest : BaseLoader
{
	// Token: 0x0600A65D RID: 42589 RVA: 0x0043A300 File Offset: 0x00438700
	private void Start()
	{
		this.chaCtrl = Singleton<Character>.Instance.CreateChara(1, null, 0, null);
		this.chaCtrl.releaseCustomInputTexture = false;
		this.chaCtrl.fileHair.parts[1].id = 1;
		this.chaCtrl.Load(false);
		this.ChangeAnimation(1);
	}

	// Token: 0x0600A65E RID: 42590 RVA: 0x0043A35C File Offset: 0x0043875C
	public bool ChangeAnimation(int pose)
	{
		if (null == this.chaCtrl)
		{
			return false;
		}
		if (this.nowPose == pose)
		{
			return false;
		}
		ChaListDefine.CategoryNo type = (this.chaCtrl.sex != 0) ? ChaListDefine.CategoryNo.custom_pose_f : ChaListDefine.CategoryNo.custom_pose_m;
		Dictionary<int, ListInfoBase> categoryInfo = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(type);
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string stateName = string.Empty;
		ListInfoBase listInfoBase;
		if (categoryInfo.TryGetValue(pose, out listInfoBase))
		{
			text = listInfoBase.GetInfo(ChaListDefine.KeyType.MainManifest);
			text2 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainAB);
			text3 = listInfoBase.GetInfo(ChaListDefine.KeyType.MainData);
			stateName = listInfoBase.GetInfo(ChaListDefine.KeyType.Clip);
			bool flag = true;
			ListInfoBase listInfoBase2;
			if (0 <= this.nowPose && categoryInfo.TryGetValue(this.nowPose, out listInfoBase2) && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainManifest) == text && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainAB) == text2 && listInfoBase2.GetInfo(ChaListDefine.KeyType.MainData) == text3)
			{
				flag = false;
			}
			if (flag)
			{
				this.chaCtrl.LoadAnimation(text2, text3, text);
			}
			this.chaCtrl.AnimPlay(stateName);
			return true;
		}
		return false;
	}

	// Token: 0x0600A65F RID: 42591 RVA: 0x0043A49B File Offset: 0x0043889B
	private void Update()
	{
	}

	// Token: 0x040082AD RID: 33453
	private ChaControl chaCtrl;

	// Token: 0x040082AE RID: 33454
	private int nowPose = -1;
}
