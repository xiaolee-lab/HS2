using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEx;

// Token: 0x02000AC2 RID: 2754
[Serializable]
public class HPoint : MonoBehaviour
{
	// Token: 0x17000EDC RID: 3804
	// (get) Token: 0x060050AE RID: 20654 RVA: 0x001F8350 File Offset: 0x001F6750
	// (set) Token: 0x060050AF RID: 20655 RVA: 0x001F8358 File Offset: 0x001F6758
	public Dictionary<int, UnityEx.ValueTuple<int, int>> _nPlace
	{
		get
		{
			return this.nPlace;
		}
		set
		{
			this.nPlace = value;
		}
	}

	// Token: 0x060050B0 RID: 20656 RVA: 0x001F8364 File Offset: 0x001F6764
	public void Init()
	{
		if (HPoint.animationLists == null)
		{
			HPoint.animationLists = Singleton<Manager.Resources>.Instance.HSceneTable.lstAnimInfo;
		}
		if (this.markerPos != null)
		{
			this.col = this.markerPos.GetComponent<Collider>();
			if (this.col != null)
			{
				this.col.enabled = false;
			}
		}
		this.effect = base.gameObject.GetComponentsInChildren<ParticleSystem>(true);
		if (this.effect != null)
		{
			foreach (ParticleSystem particleSystem in this.effect)
			{
				particleSystem.Stop();
				particleSystem.gameObject.SetActive(false);
			}
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.loadHPointDatas.TryGetValue(this.id, out this.data))
		{
			return;
		}
		foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair in this.data.place)
		{
			this.nPlace[keyValuePair.Key] = new UnityEx.ValueTuple<int, int>(keyValuePair.Value.Item1, keyValuePair.Value.Item2);
		}
		for (int j = 0; j < 6; j++)
		{
			foreach (HScene.AnimationListInfo animationListInfo in HPoint.animationLists[j])
			{
				if (this.data.notMotion[j].motionID.Contains(animationListInfo.id))
				{
					this.notMotion[j].motionID.Add(animationListInfo.id);
					this.notMotion[j].motionNames.Add(animationListInfo.nameAnimation);
				}
			}
		}
	}

	// Token: 0x060050B1 RID: 20657 RVA: 0x001F8590 File Offset: 0x001F6990
	public Collider GetCollider()
	{
		return (!(this.col != null)) ? null : this.col;
	}

	// Token: 0x060050B2 RID: 20658 RVA: 0x001F85B0 File Offset: 0x001F69B0
	public bool EffectActive()
	{
		bool flag = true;
		if (this.effect == null)
		{
			return false;
		}
		for (int i = 0; i < this.effect.Length; i++)
		{
			if (!(this.effect[i] == null))
			{
				flag &= this.effect[i].gameObject.activeSelf;
				flag &= this.effect[i].isPlaying;
			}
		}
		return flag;
	}

	// Token: 0x060050B3 RID: 20659 RVA: 0x001F8624 File Offset: 0x001F6A24
	public void SetEffectActive(bool set)
	{
		if (this.effect == null)
		{
			return;
		}
		for (int i = 0; i < this.effect.Length; i++)
		{
			if (!(this.effect[i] == null))
			{
				if (set)
				{
					this.effect[i].Play();
				}
				else
				{
					this.effect[i].Stop();
				}
				this.effect[i].gameObject.SetActive(set);
			}
		}
	}

	// Token: 0x04004A41 RID: 19009
	private static List<HScene.AnimationListInfo>[] animationLists;

	// Token: 0x04004A42 RID: 19010
	[Tooltip("登録ID")]
	public int id;

	// Token: 0x04004A43 RID: 19011
	private Dictionary<int, UnityEx.ValueTuple<int, int>> nPlace = new Dictionary<int, UnityEx.ValueTuple<int, int>>();

	// Token: 0x04004A44 RID: 19012
	[Tooltip("あたり")]
	public Transform markerPos;

	// Token: 0x04004A45 RID: 19013
	public Transform pivot;

	// Token: 0x04004A46 RID: 19014
	private Collider col;

	// Token: 0x04004A47 RID: 19015
	[Space]
	[Header("終了時の復帰ポイント")]
	public Transform endPlayerPos;

	// Token: 0x04004A48 RID: 19016
	public Transform[] endFemalePos = new Transform[2];

	// Token: 0x04004A49 RID: 19017
	private ParticleSystem[] effect;

	// Token: 0x04004A4A RID: 19018
	public List<int> OpenID = new List<int>();

	// Token: 0x04004A4B RID: 19019
	[Header("デバッグ表示 モーション除外リスト")]
	public HPoint.NotMotionInfo[] notMotion = new HPoint.NotMotionInfo[6];

	// Token: 0x04004A4C RID: 19020
	private HPoint.HpointData data;

	// Token: 0x02000AC3 RID: 2755
	[Serializable]
	public struct NotMotionInfo
	{
		// Token: 0x04004A4D RID: 19021
		[HideInInspector]
		public List<int> motionID;

		// Token: 0x04004A4E RID: 19022
		public List<string> motionNames;
	}

	// Token: 0x02000AC4 RID: 2756
	[Serializable]
	public class HpointData
	{
		// Token: 0x04004A4F RID: 19023
		public Dictionary<int, UnityEx.ValueTuple<int, int>> place = new Dictionary<int, UnityEx.ValueTuple<int, int>>();

		// Token: 0x04004A50 RID: 19024
		public HPoint.NotMotionInfo[] notMotion = new HPoint.NotMotionInfo[6];
	}
}
