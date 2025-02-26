using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

// Token: 0x02000FC1 RID: 4033
public class MinimapNavimesh : MonoBehaviour
{
	// Token: 0x06008622 RID: 34338 RVA: 0x003819A0 File Offset: 0x0037FDA0
	public void Init()
	{
		this.areaGroupActive = new Dictionary<int, bool>();
		if (!Singleton<Manager.Resources>.Instance.Map.AreaGroupTable.TryGetValue(this.MapID, out this.table))
		{
			this.table = new Dictionary<int, MinimapNavimesh.AreaGroupInfo>();
		}
		foreach (KeyValuePair<int, MinimapNavimesh.AreaGroupInfo> keyValuePair in this.table)
		{
			bool flag = true;
			if (keyValuePair.Value.OpenStateID[0] != -1)
			{
				for (int i = 0; i < keyValuePair.Value.OpenStateID.Count; i++)
				{
					int key = keyValuePair.Value.OpenStateID[i];
					flag &= Singleton<Game>.Instance.Environment.AreaOpenState[key];
				}
			}
			this.areaGroupActive.Add(keyValuePair.Key, flag);
		}
		this.Reflesh();
	}

	// Token: 0x06008623 RID: 34339 RVA: 0x00381AB4 File Offset: 0x0037FEB4
	public void Reflesh()
	{
		if (this.MapID != Singleton<Manager.Map>.Instance.MapID)
		{
			return;
		}
		if ((this.table == null || this.table.Count == 0) && !Singleton<Manager.Resources>.Instance.Map.AreaGroupTable.TryGetValue(this.MapID, out this.table))
		{
			this.table = new Dictionary<int, MinimapNavimesh.AreaGroupInfo>();
		}
		foreach (KeyValuePair<int, MinimapNavimesh.AreaGroupInfo> keyValuePair in this.table)
		{
			bool flag = true;
			if (keyValuePair.Value.OpenStateID[0] != -1)
			{
				for (int i = 0; i < keyValuePair.Value.OpenStateID.Count; i++)
				{
					int key = keyValuePair.Value.OpenStateID[i];
					flag &= Singleton<Game>.Instance.Environment.AreaOpenState[key];
				}
			}
			this.areaGroupActive[keyValuePair.Key] = flag;
		}
		this.mapNaviMesh.VisibleSet(this.areaGroupActive);
	}

	// Token: 0x04006D38 RID: 27960
	[SerializeField]
	private int MapID;

	// Token: 0x04006D39 RID: 27961
	[SerializeField]
	private MinimapNavimesh.Map mapNaviMesh;

	// Token: 0x04006D3A RID: 27962
	public Dictionary<int, bool> areaGroupActive = new Dictionary<int, bool>();

	// Token: 0x04006D3B RID: 27963
	private Dictionary<int, MinimapNavimesh.AreaGroupInfo> table;

	// Token: 0x02000FC2 RID: 4034
	[Serializable]
	private class Map
	{
		// Token: 0x06008625 RID: 34341 RVA: 0x00381C00 File Offset: 0x00380000
		public void VisibleSet(Dictionary<int, bool> areaActive)
		{
			foreach (MinimapNavimesh.AreaNaviInfo areaNaviInfo in this.areaNaviMeshs)
			{
				if (areaActive.ContainsKey(areaNaviInfo.areaGroupID))
				{
					areaNaviInfo.AreaVisibleSet(areaActive[areaNaviInfo.areaGroupID]);
				}
			}
			foreach (MinimapNavimesh.AreaNaviInfo areaNaviInfo2 in this.areaNaviMeshs)
			{
				if (!areaActive.ContainsKey(areaNaviInfo2.areaGroupID))
				{
					areaActive.Add(areaNaviInfo2.areaGroupID, areaNaviInfo2.Active);
				}
				else
				{
					areaActive[areaNaviInfo2.areaGroupID] = areaNaviInfo2.Active;
				}
			}
		}

		// Token: 0x04006D3C RID: 27964
		[SerializeField]
		private MinimapNavimesh.AreaNaviInfo[] areaNaviMeshs;
	}

	// Token: 0x02000FC3 RID: 4035
	[Serializable]
	private class AreaNaviInfo
	{
		// Token: 0x06008627 RID: 34343 RVA: 0x00381CC4 File Offset: 0x003800C4
		public void AreaVisibleSet(bool _active)
		{
			if (this.Active == _active)
			{
				return;
			}
			foreach (GameObject gameObject in this.naviMeshs)
			{
				gameObject.SetActive(_active);
			}
			this.Active = _active;
		}

		// Token: 0x04006D3D RID: 27965
		public int areaGroupID;

		// Token: 0x04006D3E RID: 27966
		public bool Active = true;

		// Token: 0x04006D3F RID: 27967
		[SerializeField]
		private GameObject[] naviMeshs;
	}

	// Token: 0x02000FC4 RID: 4036
	public class AreaGroupInfo
	{
		// Token: 0x04006D40 RID: 27968
		public List<int> areaID = new List<int>();

		// Token: 0x04006D41 RID: 27969
		public List<int> OpenStateID = new List<int>();
	}
}
