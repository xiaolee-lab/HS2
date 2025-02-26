using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using AIProject;
using Illusion.Game;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

// Token: 0x02000AB2 RID: 2738
public class HSeCtrl
{
	// Token: 0x0600506A RID: 20586 RVA: 0x001F5344 File Offset: 0x001F3744
	public HSeCtrl()
	{
		this.hFlag = Singleton<Manager.Resources>.Instance.HSceneTable.HSceneSet.GetComponentInChildren<HSceneFlagCtrl>(true);
	}

	// Token: 0x0600506B RID: 20587 RVA: 0x001F53B8 File Offset: 0x001F37B8
	public bool Load(string _strAssetFolder, string _file, GameObject _objBoneMale, params GameObject[] _objBoneFemale)
	{
		this.lstInfo.Clear();
		this.dicLoop.Clear();
		this.oldnameHash = 0;
		this.oldNormalizeTime = 0f;
		this.excelData = null;
		Singleton<Sound>.Instance.Stop(Sound.Type.GameSE3D);
		if (_file == string.Empty)
		{
			return false;
		}
		GameObject[] array;
		if (_objBoneFemale.Length > 1)
		{
			array = new GameObject[]
			{
				_objBoneMale,
				_objBoneFemale[0],
				_objBoneFemale[1]
			};
		}
		else
		{
			array = new GameObject[]
			{
				_objBoneMale,
				_objBoneFemale[0]
			};
		}
		List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_strAssetFolder, false);
		assetBundleNameListFromPath.Sort();
		this.assetName = _file;
		for (int m = 0; m < assetBundleNameListFromPath.Count; m++)
		{
			this.abName = assetBundleNameListFromPath[m];
			if (GlobalMethod.AssetFileExist(this.abName, this.assetName, string.Empty))
			{
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.abName, this.assetName, false, string.Empty);
				Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.abName);
				if (!(this.excelData == null))
				{
					int j = 1;
					while (j < this.excelData.MaxCell)
					{
						this.row = this.excelData.list[j++].list;
						int index = 0;
						this.nameAnim = this.row.GetElement(index++);
						int num = int.Parse(this.row.GetElement(index++));
						this.infoKey.objParentName = this.row.GetElement(index++);
						array[num].SafeProc(delegate(GameObject o)
						{
							if (o != null)
							{
								this.infoKey.objParent = o.transform.FindLoop(this.infoKey.objParentName);
							}
						});
						this.infoKey.key = float.Parse(this.row.GetElement(index++));
						this.infoKey.isLoop = (this.row.GetElement(index++) == "1");
						this.infoKey.loopSwitch = ((!(this.row.GetElement(index++) == "1")) ? 0 : 1);
						this.infoKey.bChangeVol = (this.row.GetElement(index++) == "1");
						this.infoKey.assetPath = this.row.GetElement(index++);
						string element = this.row.GetElement(index++);
						if (element != string.Empty)
						{
							string[] array2 = element.Split(new char[]
							{
								'/'
							});
							this.infoKey.nameSE = new List<string>();
							for (int k = 0; k < array2.Length; k++)
							{
								this.infoKey.nameSE.Add(array2[k]);
							}
						}
						this.infoKey.isShorts = (this.row.GetElement(index++) == "1");
						element = this.row.GetElement(index++);
						if (element != string.Empty)
						{
							string[] array2 = element.Split(new char[]
							{
								'/'
							});
							this.infoKey.nameShortsSE = new List<string>();
							for (int l = 0; l < array2.Length; l++)
							{
								this.infoKey.nameShortsSE.Add(array2[l]);
							}
						}
						this.infoKey.nFemale = 0;
						if (this.row.Count > 11)
						{
							this.infoKey.nFemale = ((!(this.row.GetElement(index) == string.Empty)) ? GlobalMethod.GetIntTryParse(this.row.GetElement(index++), 0) : 0);
						}
						this.info = this.lstInfo.Find((HSeCtrl.Info i) => i.nameAnimation == this.nameAnim);
						if (this.info == null)
						{
							this.info = new HSeCtrl.Info();
							this.info.nameAnimation = this.nameAnim;
							this.info.key.Add(this.infoKey);
							this.lstInfo.Add(this.info);
						}
						else
						{
							this.info.key.Add(this.infoKey);
						}
					}
				}
			}
		}
		return true;
	}

	// Token: 0x0600506C RID: 20588 RVA: 0x001F5850 File Offset: 0x001F3C50
	public bool Proc(AnimatorStateInfo _ai, ChaControl[] _females)
	{
		if (_females == null)
		{
			return false;
		}
		if (this.oldnameHash != _ai.shortNameHash)
		{
			this.oldNormalizeTime = 0f;
		}
		float now = _ai.normalizedTime % 1f;
		this.procNameSE = StringBuilderPool.Get();
		string key = string.Empty;
		string key2 = string.Empty;
		for (int i = 0; i < this.lstInfo.Count; i++)
		{
			if (_ai.IsName(this.lstInfo[i].nameAnimation))
			{
				List<HSeCtrl.KeyInfo> list = this.lstInfo[i].IsKey(this.oldNormalizeTime, now);
				for (int j = 0; j < list.Count; j++)
				{
					this.procNameSE.Clear();
					this.procNameSE.Append(this.GetSEName(list[j].nameSE));
					if (list[j].isLoop)
					{
						if (list[j].loopSwitch == 0)
						{
							bool flag = false;
							for (int k = 0; k < list[j].nameSE.Count; k++)
							{
								int index = k;
								if (this.dicLoop.ContainsKey(list[j].nameSE[index]))
								{
									key = list[j].nameSE[index];
									if (this.dicLoop[key].ContainsKey(list[j].objParentName))
									{
										flag = true;
										key2 = list[j].objParentName;
										break;
									}
								}
							}
							if (flag)
							{
								Singleton<Sound>.Instance.Stop(this.dicLoop[key][key2]);
								this.dicLoop[key].Remove(key2);
							}
						}
						else
						{
							this.dicloopContainKey[0] = this.dicLoop.ContainsKey(this.procNameSE.ToString());
							this.dicloopContainKey[1] = (this.dicloopContainKey[0] && this.dicLoop[this.procNameSE.ToString()].ContainsKey(list[j].objParentName));
							if (!this.dicloopContainKey[0] || !this.dicloopContainKey[1])
							{
								Illusion.Game.Utils.Sound.Setting s = new Illusion.Game.Utils.Sound.Setting
								{
									type = Sound.Type.GameSE3D,
									assetBundleName = list[j].assetPath,
									assetName = this.procNameSE.ToString()
								};
								Transform trans = Illusion.Game.Utils.Sound.Play(s);
								trans.SafeProcObject(delegate(Transform _)
								{
									_.GetComponent<AudioSource>().SafeProcObject(delegate(AudioSource a)
									{
										a.loop = true;
									});
								});
								AudioSource component = trans.GetComponent<AudioSource>();
								component.rolloffMode = AudioRolloffMode.Linear;
								GameObject parent = list[j].objParent;
								Observable.EveryLateUpdate().Subscribe(delegate(long _)
								{
									Vector3 pos = (!parent) ? Vector3.zero : parent.transform.position;
									Quaternion rot = (!parent) ? Quaternion.identity : parent.transform.rotation;
									trans.SafeProcObject(delegate(Transform o)
									{
										o.SetPositionAndRotation(pos, rot);
									});
								}).AddTo(trans);
								if (!this.dicloopContainKey[0])
								{
									this.dicLoop.Add(this.procNameSE.ToString(), new Dictionary<string, Transform>());
								}
								if (!this.dicloopContainKey[1])
								{
									this.dicLoop[this.procNameSE.ToString()].Add(list[j].objParentName, null);
								}
								this.dicLoop[this.procNameSE.ToString()][list[j].objParentName] = trans;
							}
						}
					}
					else
					{
						Illusion.Game.Utils.Sound.Setting setting = new Illusion.Game.Utils.Sound.Setting
						{
							type = Sound.Type.GameSE3D,
							assetBundleName = list[j].assetPath,
							assetName = this.procNameSE.ToString()
						};
						if (list[j].isShorts && list[j].nameShortsSE.Count > 0 && _females[list[j].nFemale].IsKokanHide())
						{
							setting.assetName = this.GetSEName(list[j].nameShortsSE);
						}
						Transform trans = Illusion.Game.Utils.Sound.Play(setting);
						AudioSource component2 = trans.GetComponent<AudioSource>();
						component2.rolloffMode = AudioRolloffMode.Linear;
						GameObject parent = list[j].objParent;
						Observable.EveryLateUpdate().Subscribe(delegate(long _)
						{
							Vector3 pos = (!parent) ? Vector3.zero : parent.transform.position;
							Quaternion rot = (!parent) ? Quaternion.identity : parent.transform.rotation;
							trans.SafeProcObject(delegate(Transform o)
							{
								o.SetPositionAndRotation(pos, rot);
							});
						}).AddTo(trans);
					}
				}
				break;
			}
		}
		this.oldNormalizeTime = now;
		this.oldnameHash = _ai.shortNameHash;
		StringBuilderPool.Release(this.procNameSE);
		return true;
	}

	// Token: 0x0600506D RID: 20589 RVA: 0x001F5D98 File Offset: 0x001F4198
	public bool Proc(AnimatorStateInfo _ai, ChaControl _female, int main = 0)
	{
		if (_female == null)
		{
			return false;
		}
		if (this.oldnameHash != _ai.shortNameHash)
		{
			this.oldNormalizeTime = 0f;
		}
		float now = _ai.normalizedTime % 1f;
		this.procNameSE = StringBuilderPool.Get();
		string key = string.Empty;
		string key2 = string.Empty;
		for (int i = 0; i < this.lstInfo.Count; i++)
		{
			if (_ai.IsName(this.lstInfo[i].nameAnimation))
			{
				List<HSeCtrl.KeyInfo> list = this.lstInfo[i].IsKey(this.oldNormalizeTime, now);
				for (int j = 0; j < list.Count; j++)
				{
					this.procNameSE.Clear();
					this.procNameSE.Append(this.GetSEName(list[j].nameSE));
					if (list[j].isLoop)
					{
						if (list[j].loopSwitch == 0)
						{
							bool flag = false;
							for (int k = 0; k < list[j].nameSE.Count; k++)
							{
								int index = k;
								if (this.dicLoop.ContainsKey(list[j].nameSE[index]))
								{
									key = list[j].nameSE[index];
									if (this.dicLoop[key].ContainsKey(list[j].objParentName))
									{
										flag = true;
										key2 = list[j].objParentName;
										break;
									}
								}
							}
							if (flag)
							{
								Singleton<Sound>.Instance.Stop(this.dicLoop[key][key2]);
								this.dicLoop[key].Remove(key2);
							}
						}
						else
						{
							this.dicloopContainKey[0] = this.dicLoop.ContainsKey(this.procNameSE.ToString());
							this.dicloopContainKey[1] = (this.dicloopContainKey[0] && this.dicLoop[this.procNameSE.ToString()].ContainsKey(list[j].objParentName));
							if (!this.dicloopContainKey[0] || !this.dicloopContainKey[1])
							{
								Illusion.Game.Utils.Sound.Setting s = new Illusion.Game.Utils.Sound.Setting
								{
									type = Sound.Type.GameSE3D,
									assetBundleName = list[j].assetPath,
									assetName = this.procNameSE.ToString()
								};
								Transform trans = Illusion.Game.Utils.Sound.Play(s);
								trans.SafeProcObject(delegate(Transform _)
								{
									_.GetComponent<AudioSource>().SafeProcObject(delegate(AudioSource a)
									{
										a.loop = true;
									});
								});
								GameObject parent = list[j].objParent;
								Observable.EveryLateUpdate().Subscribe(delegate(long _)
								{
									Vector3 pos = (!parent) ? Vector3.zero : parent.transform.position;
									Quaternion rot = (!parent) ? Quaternion.identity : parent.transform.rotation;
									trans.SafeProcObject(delegate(Transform o)
									{
										o.SetPositionAndRotation(pos, rot);
									});
								}).AddTo(trans);
								if (!this.dicloopContainKey[0])
								{
									this.dicLoop.Add(this.procNameSE.ToString(), new Dictionary<string, Transform>());
								}
								if (!this.dicloopContainKey[1])
								{
									this.dicLoop[this.procNameSE.ToString()].Add(list[j].objParentName, null);
								}
								this.dicLoop[this.procNameSE.ToString()][list[j].objParentName] = trans;
							}
						}
					}
					else
					{
						Illusion.Game.Utils.Sound.Setting setting = new Illusion.Game.Utils.Sound.Setting
						{
							type = Sound.Type.GameSE3D,
							assetBundleName = list[j].assetPath,
							assetName = this.procNameSE.ToString()
						};
						if (list[j].isShorts && list[j].nameShortsSE.Count > 0 && _female.IsKokanHide())
						{
							setting.assetName = this.GetSEName(list[j].nameShortsSE);
						}
						Transform trans = Illusion.Game.Utils.Sound.Play(setting);
						GameObject parent = list[j].objParent;
						Observable.EveryLateUpdate().Subscribe(delegate(long _)
						{
							Vector3 pos = (!parent) ? Vector3.zero : parent.transform.position;
							Quaternion rot = (!parent) ? Quaternion.identity : parent.transform.rotation;
							trans.SafeProcObject(delegate(Transform o)
							{
								o.SetPositionAndRotation(pos, rot);
							});
						}).AddTo(trans);
					}
				}
				break;
			}
		}
		this.oldNormalizeTime = now;
		this.oldnameHash = _ai.shortNameHash;
		StringBuilderPool.Release(this.procNameSE);
		return true;
	}

	// Token: 0x0600506E RID: 20590 RVA: 0x001F62A5 File Offset: 0x001F46A5
	public void InitOldMember(int _init = -1)
	{
		if (_init == -1 || _init == 0)
		{
			this.oldNormalizeTime = 0f;
		}
		if (_init == -1 || _init == 1)
		{
			this.oldnameHash = 0;
		}
	}

	// Token: 0x0600506F RID: 20591 RVA: 0x001F62D4 File Offset: 0x001F46D4
	private string GetSEName(List<string> list)
	{
		if (list.Count < 1)
		{
			return string.Empty;
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	// Token: 0x040049E9 RID: 18921
	private List<HSeCtrl.Info> lstInfo = new List<HSeCtrl.Info>();

	// Token: 0x040049EA RID: 18922
	private Dictionary<string, Dictionary<string, Transform>> dicLoop = new Dictionary<string, Dictionary<string, Transform>>();

	// Token: 0x040049EB RID: 18923
	private HSceneFlagCtrl hFlag;

	// Token: 0x040049EC RID: 18924
	private int oldnameHash;

	// Token: 0x040049ED RID: 18925
	private float oldNormalizeTime;

	// Token: 0x040049EE RID: 18926
	private string abName = string.Empty;

	// Token: 0x040049EF RID: 18927
	private string assetName = string.Empty;

	// Token: 0x040049F0 RID: 18928
	private ExcelData excelData;

	// Token: 0x040049F1 RID: 18929
	private List<string> row = new List<string>();

	// Token: 0x040049F2 RID: 18930
	private HSeCtrl.KeyInfo infoKey;

	// Token: 0x040049F3 RID: 18931
	private HSeCtrl.Info info;

	// Token: 0x040049F4 RID: 18932
	private string nameAnim;

	// Token: 0x040049F5 RID: 18933
	private bool[] dicloopContainKey = new bool[2];

	// Token: 0x040049F6 RID: 18934
	private StringBuilder procNameSE;

	// Token: 0x02000AB3 RID: 2739
	private struct KeyInfo
	{
		// Token: 0x040049FB RID: 18939
		public GameObject objParent;

		// Token: 0x040049FC RID: 18940
		public string objParentName;

		// Token: 0x040049FD RID: 18941
		public float key;

		// Token: 0x040049FE RID: 18942
		public bool isLoop;

		// Token: 0x040049FF RID: 18943
		public int loopSwitch;

		// Token: 0x04004A00 RID: 18944
		public string assetPath;

		// Token: 0x04004A01 RID: 18945
		public List<string> nameSE;

		// Token: 0x04004A02 RID: 18946
		public bool isShorts;

		// Token: 0x04004A03 RID: 18947
		public List<string> nameShortsSE;

		// Token: 0x04004A04 RID: 18948
		public int nFemale;

		// Token: 0x04004A05 RID: 18949
		public bool bChangeVol;
	}

	// Token: 0x02000AB4 RID: 2740
	private class Info
	{
		// Token: 0x06005077 RID: 20599 RVA: 0x001F63C4 File Offset: 0x001F47C4
		public List<HSeCtrl.KeyInfo> IsKey(float _old, float _now)
		{
			HSeCtrl.Info.IsCheck<float, float, float>[] array = new HSeCtrl.Info.IsCheck<float, float, float>[]
			{
				new HSeCtrl.Info.IsCheck<float, float, float>(this.IsKeyLoop),
				new HSeCtrl.Info.IsCheck<float, float, float>(this.IsKeyNormal)
			};
			List<HSeCtrl.KeyInfo> list = new List<HSeCtrl.KeyInfo>();
			int num = (_old <= _now) ? 1 : 0;
			for (int i = 0; i < this.key.Count; i++)
			{
				if (array[num](_old, _now, this.key[i].key))
				{
					list.Add(this.key[i]);
				}
			}
			return list;
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x001F645E File Offset: 0x001F485E
		private bool IsKeyLoop(float _old, float _now, float _key)
		{
			return _old < _key || _now > _key;
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x001F646E File Offset: 0x001F486E
		private bool IsKeyNormal(float _old, float _now, float _key)
		{
			return _old <= _key && _key < _now;
		}

		// Token: 0x04004A06 RID: 18950
		public string nameAnimation;

		// Token: 0x04004A07 RID: 18951
		public List<HSeCtrl.KeyInfo> key = new List<HSeCtrl.KeyInfo>();

		// Token: 0x02000AB5 RID: 2741
		// (Invoke) Token: 0x0600507B RID: 20603
		public delegate bool IsCheck<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2);
	}
}
