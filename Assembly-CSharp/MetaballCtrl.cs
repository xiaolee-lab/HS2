using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

// Token: 0x02000AEF RID: 2799
public class MetaballCtrl
{
	// Token: 0x060051C0 RID: 20928 RVA: 0x0021B410 File Offset: 0x00219810
	public MetaballCtrl(GameObject _objBase, GameObject _objMale, GameObject _objFemale, ChaControl _customFemale)
	{
		if (_objBase)
		{
			this.ctrlUpdate = _objBase.GetComponentsInChildren<UpdateMeta>();
			foreach (UpdateMeta updateMeta in this.ctrlUpdate)
			{
				foreach (MetaballShoot metaballShoot in updateMeta.lstShoot)
				{
					if (metaballShoot.comment.Contains("中出し1"))
					{
						this.ctrlMetaball[3] = metaballShoot;
					}
					else if (metaballShoot.comment.Contains("外出し1"))
					{
						this.ctrlMetaball[4] = metaballShoot;
						this.ctrlMetaball[4].chaCustom = _customFemale;
					}
					else if (metaballShoot.comment.Contains("外出し2"))
					{
						this.ctrlMetaball[5] = metaballShoot;
						this.ctrlMetaball[5].chaCustom = _customFemale;
					}
					else if (metaballShoot.comment.Contains("吐く2"))
					{
						this.ctrlMetaball[6] = metaballShoot;
						this.ctrlMetaball[6].chaCustom = _customFemale;
					}
					else if (metaballShoot.comment.Contains("中出し"))
					{
						this.ctrlMetaball[0] = metaballShoot;
						this.ctrlMetaball[0].chaCustom = _customFemale;
					}
					else if (metaballShoot.comment.Contains("外出し"))
					{
						this.ctrlMetaball[1] = metaballShoot;
						this.ctrlMetaball[1].chaCustom = _customFemale;
					}
					else if (metaballShoot.comment.Contains("吐く"))
					{
						this.ctrlMetaball[2] = metaballShoot;
						this.ctrlMetaball[2].chaCustom = _customFemale;
					}
				}
			}
		}
		this.aobjBody[0] = _objMale;
		this.aobjBody[1] = _objFemale;
		for (int j = 0; j < 7; j++)
		{
			this.actrlShoot[j] = new MetaballCtrl.ShootCtrl();
			this.aParam[j] = new MetaballCtrl.MetaballParameter();
		}
	}

	// Token: 0x060051C1 RID: 20929 RVA: 0x0021B698 File Offset: 0x00219A98
	public void SetParticle(HParticleCtrl _particle)
	{
		this.particle = _particle;
	}

	// Token: 0x060051C2 RID: 20930 RVA: 0x0021B6A4 File Offset: 0x00219AA4
	public bool Load(string _strAssetPath, string _nameFile, GameObject _objMale, GameObject _objMale1, GameObject _objFemale1 = null, ChaControl _customFemale1 = null)
	{
		this.aobjBody[0] = _objMale;
		this.aobjBody[2] = _objMale1;
		if (_objFemale1 != null)
		{
			this.aobjBody[3] = _objFemale1;
			if (this.ctrlMetaball[4] != null)
			{
				this.ctrlMetaball[4].chaCustom = _customFemale1;
			}
		}
		foreach (MetaballCtrl.ShootCtrl shootCtrl in this.actrlShoot)
		{
			shootCtrl.lstShoot.Clear();
		}
		string text = string.Empty;
		if (!_nameFile.ContainsAny(new string[]
		{
			"f2",
			"f1"
		}))
		{
			text = _nameFile.Remove(3, 2);
		}
		else if (_nameFile.Contains("ai3p"))
		{
			text = _nameFile.Remove(4, 3);
		}
		else if (_nameFile.Contains("h2_mf2"))
		{
			text = _nameFile.Remove(6, 3);
		}
		else
		{
			text = _nameFile.Remove(3, 3);
		}
		this.infolist = GlobalMethod.LoadExcelDataAlFindlFile(_strAssetPath, text, 1, 1, 3, 1, null, true);
		if (this.infolist == null)
		{
			GlobalMethod.DebugLog(string.Concat(new string[]
			{
				"excel : [",
				text,
				"]は読み込めなかった\u3000警告無しでここに来たら[",
				_strAssetPath,
				"]自体がない"
			}), 1);
			return false;
		}
		int count = this.infolist.Count;
		if (count < 3)
		{
			GlobalMethod.DebugLog("excel : [" + text + "]は必ず3行必要", 1);
			return false;
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		StringBuilder stringBuilder3 = new StringBuilder();
		string a = string.Empty;
		for (int j = 0; j < count; j++)
		{
			int num = 0;
			if (this.ctrlMetaball[j] != null)
			{
				this.ctrlMetaball[j].isEnable = (this.infolist[j].list[num++] == "1");
				if (this.ctrlMetaball[j].isEnable)
				{
					int intTryParse = GlobalMethod.GetIntTryParse(this.infolist[j].list[num++], 0);
					this.ctrlMetaball[j].ShootAxis = ((!this.aobjBody[intTryParse]) ? null : this.aobjBody[intTryParse].transform.FindLoop(this.infolist[j].list[num++]));
					if (this.aobjBody[intTryParse] == null)
					{
						num++;
					}
					intTryParse = GlobalMethod.GetIntTryParse(this.infolist[j].list[num++], 0);
					GameObject gameObject = (!this.aobjBody[intTryParse]) ? null : this.aobjBody[intTryParse].transform.FindLoop(this.infolist[j].list[num++]);
					this.ctrlMetaball[j].parentTransform = ((!gameObject) ? null : gameObject.transform);
					this.aParam[j].param[0].objParent = gameObject;
					if (this.aobjBody[intTryParse] == null)
					{
						num++;
					}
					intTryParse = GlobalMethod.GetIntTryParse(this.infolist[j].list[num++], 0);
					gameObject = ((!this.aobjBody[intTryParse]) ? null : this.aobjBody[intTryParse].transform.FindLoop(this.infolist[j].list[num++]));
					this.aParam[j].param[1].objParent = gameObject;
					if (this.aobjBody[intTryParse] == null)
					{
						num++;
					}
					stringBuilder.Clear();
					stringBuilder.Append(this.infolist[j].list[num++]);
					stringBuilder2.Clear();
					stringBuilder2.Append(this.infolist[j].list[num++]);
					stringBuilder3.Clear();
					stringBuilder3.Append(this.infolist[j].list[num++]);
					this.aParam[j].param[0].objShoot = this.LoadSiruObject(stringBuilder.ToString(), stringBuilder2.ToString(), stringBuilder3.ToString());
					this.ctrlMetaball[j].ShootObj = this.aParam[j].param[0].objShoot;
					AssetBundleManager.UnloadAssetBundle(stringBuilder.ToString(), false, stringBuilder3.ToString(), false);
					stringBuilder.Clear();
					stringBuilder.Append(this.infolist[j].list[num++]);
					stringBuilder2.Clear();
					stringBuilder2.Append(this.infolist[j].list[num++]);
					stringBuilder3.Clear();
					stringBuilder3.Append(this.infolist[j].list[num++]);
					this.aParam[j].param[1].objShoot = this.LoadSiruObject(stringBuilder.ToString(), stringBuilder2.ToString(), stringBuilder3.ToString());
					AssetBundleManager.UnloadAssetBundle(stringBuilder.ToString(), false, stringBuilder3.ToString(), false);
					this.aParam[j].param[0].speedS = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[0].rotationS = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[0].speedM = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[0].rotationM = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[0].speedL = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[0].rotationL = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.ctrlMetaball[j].speedSMin = this.aParam[j].param[0].speedS.x;
					this.ctrlMetaball[j].speedSMax = this.aParam[j].param[0].speedS.y;
					this.ctrlMetaball[j].randomRotationS = this.aParam[j].param[0].rotationS;
					this.ctrlMetaball[j].speedMMin = this.aParam[j].param[0].speedM.x;
					this.ctrlMetaball[j].speedMMax = this.aParam[j].param[0].speedM.y;
					this.ctrlMetaball[j].randomRotationM = this.aParam[j].param[0].rotationM;
					this.ctrlMetaball[j].speedLMin = this.aParam[j].param[0].speedL.x;
					this.ctrlMetaball[j].speedLMax = this.aParam[j].param[0].speedL.y;
					this.ctrlMetaball[j].randomRotationL = this.aParam[j].param[0].rotationL;
					this.aParam[j].param[1].speedS = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[1].rotationS = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[1].speedM = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[1].rotationM = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[1].speedL = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					this.aParam[j].param[1].rotationL = new Vector2(float.Parse(this.infolist[j].list[num++]), float.Parse(this.infolist[j].list[num++]));
					while (this.infolist[j].list.Count > num)
					{
						a = this.infolist[j].list[num++];
						if (a == string.Empty)
						{
							break;
						}
						this.info = default(MetaballCtrl.ShootInfo);
						this.info.isInside = (a == "1");
						this.info.nameAnimation = this.infolist[j].list[num++];
						float timeShoot;
						if (!float.TryParse(this.infolist[j].list[num++], out timeShoot))
						{
							timeShoot = 0f;
						}
						this.info.timeShoot = timeShoot;
						this.info.timeOld = 0f;
						this.actrlShoot[j].lstShoot.Add(this.info);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060051C3 RID: 20931 RVA: 0x0021C37C File Offset: 0x0021A77C
	public bool SetParameterFromState(int _state)
	{
		if (!GlobalMethod.RangeOn<int>(_state, 0, 1))
		{
			return false;
		}
		for (int i = 0; i < this.ctrlMetaball.Length; i++)
		{
			if (this.ctrlMetaball[i] != null)
			{
				this.ctrlMetaball[i].speedSMin = this.aParam[i].param[_state].speedS.x;
				this.ctrlMetaball[i].speedSMax = this.aParam[i].param[_state].speedS.y;
				this.ctrlMetaball[i].randomRotationS = this.aParam[i].param[_state].rotationS;
				this.ctrlMetaball[i].speedMMin = this.aParam[i].param[_state].speedM.x;
				this.ctrlMetaball[i].speedMMax = this.aParam[i].param[_state].speedM.y;
				this.ctrlMetaball[i].randomRotationM = this.aParam[i].param[_state].rotationM;
				this.ctrlMetaball[i].speedLMin = this.aParam[i].param[_state].speedL.x;
				this.ctrlMetaball[i].speedLMax = this.aParam[i].param[_state].speedL.y;
				this.ctrlMetaball[i].randomRotationL = this.aParam[i].param[_state].rotationL;
				this.ctrlMetaball[i].ShootObj = this.aParam[i].param[_state].objShoot;
				this.ctrlMetaball[i].parentTransform = ((!this.aParam[i].param[_state].objParent) ? null : this.aParam[i].param[_state].objParent.transform);
			}
		}
		return true;
	}

	// Token: 0x060051C4 RID: 20932 RVA: 0x0021C578 File Offset: 0x0021A978
	public bool Proc(AnimatorStateInfo _stateInfo, bool _isInside = false)
	{
		if (Singleton<HSceneFlagCtrl>.Instance.semenType == 0)
		{
			return true;
		}
		for (int i = 0; i < this.actrlShoot.Length; i++)
		{
			if (Singleton<HSceneFlagCtrl>.Instance.semenType != 2 || i == 1 || i == 4 || i == 5)
			{
				if (this.actrlShoot[i].IsAnimation(_isInside, _stateInfo, this.info))
				{
					if (Singleton<HSceneFlagCtrl>.Instance.semenType == 2)
					{
						if (this.particle != null)
						{
							if (i == 1)
							{
								this.particle.Play(3);
								this.particle.Play(11);
							}
							if (i == 4)
							{
								this.particle.Play(7);
								this.particle.Play(12);
							}
							if (i == 5)
							{
								this.particle.Play(13);
							}
						}
					}
					else if (this.ctrlMetaball[i] != null)
					{
						this.ctrlMetaball[i].ShootMetaBallStart();
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060051C5 RID: 20933 RVA: 0x0021C688 File Offset: 0x0021AA88
	public bool Clear()
	{
		foreach (UpdateMeta updateMeta in this.ctrlUpdate)
		{
			updateMeta.MetaBallClear(false);
		}
		return true;
	}

	// Token: 0x060051C6 RID: 20934 RVA: 0x0021C6BC File Offset: 0x0021AABC
	private GameObject LoadSiruObject(string _pathAsset, string _nameFile, string _manifest)
	{
		if (_nameFile == string.Empty)
		{
			return null;
		}
		GameObject result = CommonLib.LoadAsset<GameObject>(_pathAsset, _nameFile, false, _manifest);
		Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(_pathAsset);
		return result;
	}

	// Token: 0x04004C3E RID: 19518
	private List<ExcelData.Param> infolist = new List<ExcelData.Param>();

	// Token: 0x04004C3F RID: 19519
	private MetaballCtrl.ShootInfo info = default(MetaballCtrl.ShootInfo);

	// Token: 0x04004C40 RID: 19520
	private const int cMetaBallNum = 7;

	// Token: 0x04004C41 RID: 19521
	public MetaballShoot[] ctrlMetaball = new MetaballShoot[7];

	// Token: 0x04004C42 RID: 19522
	private UpdateMeta[] ctrlUpdate;

	// Token: 0x04004C43 RID: 19523
	private MetaballCtrl.ShootCtrl[] actrlShoot = new MetaballCtrl.ShootCtrl[7];

	// Token: 0x04004C44 RID: 19524
	private GameObject[] aobjBody = new GameObject[4];

	// Token: 0x04004C45 RID: 19525
	private MetaballCtrl.MetaballParameter[] aParam = new MetaballCtrl.MetaballParameter[7];

	// Token: 0x04004C46 RID: 19526
	private HParticleCtrl particle;

	// Token: 0x02000AF0 RID: 2800
	public struct ShootInfo
	{
		// Token: 0x04004C47 RID: 19527
		public bool isInside;

		// Token: 0x04004C48 RID: 19528
		public string nameAnimation;

		// Token: 0x04004C49 RID: 19529
		public float timeShoot;

		// Token: 0x04004C4A RID: 19530
		public float timeOld;
	}

	// Token: 0x02000AF1 RID: 2801
	public class ShootCtrl
	{
		// Token: 0x060051C8 RID: 20936 RVA: 0x0021C710 File Offset: 0x0021AB10
		public bool IsAnimation(bool _isInside, AnimatorStateInfo _stateInfo, MetaballCtrl.ShootInfo info)
		{
			for (int i = 0; i < this.lstShoot.Count; i++)
			{
				info = this.lstShoot[i];
				if (!_stateInfo.IsName(info.nameAnimation))
				{
					info.timeOld = 0f;
					this.lstShoot[i] = info;
				}
				else if (!info.isInside || _isInside)
				{
					if (info.timeOld <= info.timeShoot && info.timeShoot < _stateInfo.normalizedTime)
					{
						info.timeOld = _stateInfo.normalizedTime;
						this.lstShoot[i] = info;
						return true;
					}
					info.timeOld = _stateInfo.normalizedTime;
					this.lstShoot[i] = info;
				}
			}
			return false;
		}

		// Token: 0x04004C4B RID: 19531
		public List<MetaballCtrl.ShootInfo> lstShoot = new List<MetaballCtrl.ShootInfo>();
	}

	// Token: 0x02000AF2 RID: 2802
	public class MetaballParameterInfo
	{
		// Token: 0x04004C4C RID: 19532
		public Vector2 speedS;

		// Token: 0x04004C4D RID: 19533
		public Vector2 speedM;

		// Token: 0x04004C4E RID: 19534
		public Vector2 speedL;

		// Token: 0x04004C4F RID: 19535
		public Vector2 rotationS;

		// Token: 0x04004C50 RID: 19536
		public Vector2 rotationM;

		// Token: 0x04004C51 RID: 19537
		public Vector2 rotationL;

		// Token: 0x04004C52 RID: 19538
		public GameObject objShoot;

		// Token: 0x04004C53 RID: 19539
		public GameObject objParent;
	}

	// Token: 0x02000AF3 RID: 2803
	public class MetaballParameter
	{
		// Token: 0x060051CA RID: 20938 RVA: 0x0021C7FB File Offset: 0x0021ABFB
		public MetaballParameter()
		{
			this.param[0] = new MetaballCtrl.MetaballParameterInfo();
			this.param[1] = new MetaballCtrl.MetaballParameterInfo();
		}

		// Token: 0x04004C54 RID: 19540
		public MetaballCtrl.MetaballParameterInfo[] param = new MetaballCtrl.MetaballParameterInfo[2];
	}
}
