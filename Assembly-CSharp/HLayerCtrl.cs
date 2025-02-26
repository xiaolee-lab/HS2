using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;

// Token: 0x02000AA6 RID: 2726
public class HLayerCtrl : MonoBehaviour
{
	// Token: 0x06005024 RID: 20516 RVA: 0x001EE3A8 File Offset: 0x001EC7A8
	public void Init(ChaControl[] _chaFemales, ChaControl[] _chaMales)
	{
		this.ctrlFlag = Singleton<HSceneFlagCtrl>.Instance;
		this.chaFemales = _chaFemales;
		this.chaMales = _chaMales;
		this.LayerInfos = new Dictionary<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]>();
		this.LayerInfos.Add(0, new Dictionary<string, HLayerCtrl.HLayerInfo>[2]);
		this.LayerInfos.Add(1, new Dictionary<string, HLayerCtrl.HLayerInfo>[2]);
		foreach (KeyValuePair<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]> keyValuePair in this.LayerInfos)
		{
			keyValuePair.Value[0] = new Dictionary<string, HLayerCtrl.HLayerInfo>();
			keyValuePair.Value[1] = new Dictionary<string, HLayerCtrl.HLayerInfo>();
		}
	}

	// Token: 0x06005025 RID: 20517 RVA: 0x001EE464 File Offset: 0x001EC864
	public int Init(ChaControl _chaFemale, HSceneFlagCtrl _ctrlFlag)
	{
		this.ctrlFlag = _ctrlFlag;
		if (this.MapHchaFemales == null)
		{
			this.MapHchaFemales = new Dictionary<int, ChaControl[]>();
		}
		int num = this.MapHchaFemales.Count;
		if (this.MapHchaFemales.ContainsKey(num))
		{
			int num2 = 0;
			while (this.MapHchaFemales.ContainsKey(num2))
			{
				num2++;
			}
			num = num2;
		}
		Dictionary<int, ChaControl[]> mapHchaFemales = this.MapHchaFemales;
		int key = num;
		ChaControl[] array = new ChaControl[2];
		array[0] = _chaFemale;
		mapHchaFemales.Add(key, array);
		if (this.MapHLayerInfos == null)
		{
			this.MapHLayerInfos = new Dictionary<int, Dictionary<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]>>();
		}
		this.MapHLayerInfos.Add(num, new Dictionary<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]>());
		this.MapHLayerInfos[num].Add(1, new Dictionary<string, HLayerCtrl.HLayerInfo>[2]);
		this.MapHLayerInfos[num][1][0] = new Dictionary<string, HLayerCtrl.HLayerInfo>();
		this.ctrlFlag.AddMapSyncAnimLayer(num);
		return num;
	}

	// Token: 0x06005026 RID: 20518 RVA: 0x001EE544 File Offset: 0x001EC944
	public void Release()
	{
		foreach (KeyValuePair<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]> keyValuePair in this.LayerInfos)
		{
			foreach (Dictionary<string, HLayerCtrl.HLayerInfo> dictionary in keyValuePair.Value)
			{
				if (dictionary != null)
				{
					dictionary.Clear();
				}
			}
		}
		this.LayerInfos.Clear();
		this.chaFemales = null;
		this.chaMales = null;
	}

	// Token: 0x06005027 RID: 20519 RVA: 0x001EE5E4 File Offset: 0x001EC9E4
	public void MapHLayerRemove(int _id)
	{
		if (this.MapHLayerInfos.ContainsKey(_id))
		{
			foreach (KeyValuePair<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]> keyValuePair in this.MapHLayerInfos[_id])
			{
				foreach (Dictionary<string, HLayerCtrl.HLayerInfo> dictionary in keyValuePair.Value)
				{
					if (dictionary != null)
					{
						dictionary.Clear();
					}
				}
			}
			this.MapHLayerInfos[_id].Clear();
			this.MapHLayerInfos.Remove(_id);
		}
		if (this.chaFemales != null)
		{
			this.chaFemales = null;
		}
		if (this.chaMales != null)
		{
			this.chaMales = null;
		}
		if (this.MapHchaFemales.ContainsKey(_id))
		{
			this.MapHchaFemales[_id] = null;
			this.MapHchaFemales.Remove(_id);
		}
		this.ctrlFlag.RemoveMapSyncAnimLayer(_id);
	}

	// Token: 0x06005028 RID: 20520 RVA: 0x001EE6FC File Offset: 0x001ECAFC
	public void LoadExcel(string animatorName, int _sex, int _id, bool mapH = false, int mapHID = 0)
	{
		Dictionary<string, HLayerCtrl.HLayerInfo> dictionary = null;
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.LayerInfos.TryGetValue(animatorName, out dictionary))
		{
			dictionary = new Dictionary<string, HLayerCtrl.HLayerInfo>();
		}
		if (!mapH)
		{
			this.LayerInfos[_sex][_id].Clear();
			foreach (KeyValuePair<string, HLayerCtrl.HLayerInfo> keyValuePair in dictionary)
			{
				this.LayerInfos[_sex][_id].Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		else
		{
			this.MapHLayerInfos[mapHID][_sex][_id].Clear();
			foreach (KeyValuePair<string, HLayerCtrl.HLayerInfo> keyValuePair2 in dictionary)
			{
				this.MapHLayerInfos[mapHID][_sex][_id].Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}
	}

	// Token: 0x06005029 RID: 20521 RVA: 0x001EE834 File Offset: 0x001ECC34
	private void LateUpdate()
	{
		if (this.chaFemales == null || this.chaFemales.Length <= 0 || this.chaFemales[0] == null)
		{
			return;
		}
		this.setLayer(this.chaFemales, 1, -1);
		this.setLayer(this.chaMales, 0, -1);
	}

	// Token: 0x0600502A RID: 20522 RVA: 0x001EE88C File Offset: 0x001ECC8C
	public void MapHProc(int ID)
	{
		if (this.MapHchaFemales == null || this.MapHchaFemales.Count <= 0 || this.MapHchaFemales[ID] == null)
		{
			return;
		}
		this.setLayer(this.MapHchaFemales[ID], 1, ID);
	}

	// Token: 0x0600502B RID: 20523 RVA: 0x001EE8DC File Offset: 0x001ECCDC
	private void setLayer(ChaControl[] charas, int Sex, int _mapHID = -1)
	{
		for (int i = 0; i < 2; i++)
		{
			if (!(charas[i] == null) && !(charas[i].animBody == null) && !(charas[i].animBody.runtimeAnimatorController == null))
			{
				this.stateInfo = charas[i].getAnimatorStateInfo(0);
				bool flag = false;
				if (_mapHID == -1)
				{
					foreach (string text in this.LayerInfos[Sex][i].Keys)
					{
						if (this.stateInfo.IsName(text))
						{
							flag = true;
							int layerID = this.LayerInfos[Sex][i][text].LayerID;
							float weight = this.LayerInfos[Sex][i][text].weight;
							this.setLayer(charas, Sex, i, layerID, weight, -1);
						}
					}
				}
				else
				{
					foreach (string text2 in this.MapHLayerInfos[_mapHID][Sex][i].Keys)
					{
						if (this.stateInfo.IsName(text2))
						{
							flag = true;
							int layerID2 = this.MapHLayerInfos[_mapHID][Sex][i][text2].LayerID;
							float weight2 = this.MapHLayerInfos[_mapHID][Sex][i][text2].weight;
							this.setLayer(charas, Sex, i, layerID2, weight2, _mapHID);
						}
					}
				}
				if (!flag)
				{
					if (_mapHID == -1)
					{
						for (int j = 1; j < charas[i].animBody.layerCount; j++)
						{
							charas[i].setLayerWeight(0f, j);
							this.ctrlFlag.lstSyncAnimLayers[Sex, i].Remove(j);
						}
					}
					else
					{
						for (int k = 1; k < charas[i].animBody.layerCount; k++)
						{
							charas[i].setLayerWeight(0f, k);
							this.ctrlFlag.lstMapSyncAnimLayers[_mapHID].Remove(k);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600502C RID: 20524 RVA: 0x001EEB84 File Offset: 0x001ECF84
	private void setLayer(ChaControl[] charas, int sex, int index, int layer, float weight, int _mapHID = -1)
	{
		if (layer != 0)
		{
			List<int> list = (_mapHID != -1) ? this.ctrlFlag.lstMapSyncAnimLayers[_mapHID] : this.ctrlFlag.lstSyncAnimLayers[sex, index];
			if (!list.Contains(layer))
			{
				for (int i = 1; i < charas[index].animBody.layerCount; i++)
				{
					if (layer == i)
					{
						charas[index].setLayerWeight(weight, layer);
					}
					else
					{
						charas[index].setLayerWeight(0f, i);
					}
				}
				list.Add(layer);
			}
			else if (weight == 0f)
			{
				charas[index].setLayerWeight(0f, layer);
				list.Remove(layer);
			}
		}
		else
		{
			for (int j = 1; j < charas[index].animBody.layerCount; j++)
			{
				charas[index].setLayerWeight(0f, j);
			}
		}
	}

	// Token: 0x0400492F RID: 18735
	private ChaControl[] chaFemales;

	// Token: 0x04004930 RID: 18736
	private ChaControl[] chaMales;

	// Token: 0x04004931 RID: 18737
	private HSceneFlagCtrl ctrlFlag;

	// Token: 0x04004932 RID: 18738
	private ExcelData excelData;

	// Token: 0x04004933 RID: 18739
	private AnimatorStateInfo stateInfo;

	// Token: 0x04004934 RID: 18740
	private Dictionary<int, ChaControl[]> MapHchaFemales;

	// Token: 0x04004935 RID: 18741
	private Dictionary<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]> LayerInfos;

	// Token: 0x04004936 RID: 18742
	private Dictionary<int, Dictionary<int, Dictionary<string, HLayerCtrl.HLayerInfo>[]>> MapHLayerInfos;

	// Token: 0x02000AA7 RID: 2727
	public struct HLayerInfo
	{
		// Token: 0x04004937 RID: 18743
		public int LayerID;

		// Token: 0x04004938 RID: 18744
		public float weight;
	}
}
