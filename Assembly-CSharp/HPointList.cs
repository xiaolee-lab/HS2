using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000ACA RID: 2762
public class HPointList : MonoBehaviour
{
	// Token: 0x060050F1 RID: 20721 RVA: 0x001FE140 File Offset: 0x001FC540
	public void Init()
	{
		this.lst = new Dictionary<int, List<HPoint>>();
		for (int i = 0; i < this.Areas.Length; i++)
		{
			int num = i;
			this.HPoints = this.Areas[num].HPoints.GetComponentsInChildren<HPoint>();
			foreach (HPoint hpoint in this.HPoints)
			{
				ParticleSystem componentInChildren = hpoint.GetComponentInChildren<ParticleSystem>();
				if (componentInChildren != null && componentInChildren.gameObject.activeSelf)
				{
					componentInChildren.gameObject.SetActive(false);
				}
			}
			this.lst.Add(this.Areas[num].Area, new List<HPoint>(this.HPoints));
		}
	}

	// Token: 0x04004A78 RID: 19064
	public Dictionary<int, List<HPoint>> lst;

	// Token: 0x04004A79 RID: 19065
	private HPoint[] HPoints;

	// Token: 0x04004A7A RID: 19066
	[SerializeField]
	private HPointList.AreaInfo[] Areas;

	// Token: 0x02000ACB RID: 2763
	[Serializable]
	private class AreaInfo
	{
		// Token: 0x04004A7B RID: 19067
		[Label("エリア")]
		public int Area;

		// Token: 0x04004A7C RID: 19068
		[Label("Hポイント")]
		public GameObject HPoints;
	}

	// Token: 0x02000ACC RID: 2764
	[Serializable]
	public class LoadInfo
	{
		// Token: 0x04004A7D RID: 19069
		public string Path;

		// Token: 0x04004A7E RID: 19070
		public string Name;

		// Token: 0x04004A7F RID: 19071
		public string Manifest;
	}
}
