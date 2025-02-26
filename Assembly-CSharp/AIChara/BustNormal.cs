using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007B6 RID: 1974
	public class BustNormal
	{
		// Token: 0x06003051 RID: 12369 RVA: 0x00120850 File Offset: 0x0011EC50
		public bool Init(GameObject objTarg, string assetBundleName, string assetName, string manifest)
		{
			this.initEnd = false;
			this.normal = CommonLib.LoadAsset<NormalData>(assetBundleName, assetName, true, manifest);
			if (null == this.normal)
			{
				return false;
			}
			Singleton<Character>.Instance.AddLoadAssetBundle(assetBundleName, string.Empty);
			this.dictNormal.Clear();
			this.dictSmr.Clear();
			for (int i = 0; i < this.normal.data.Count; i++)
			{
				GameObject gameObject = objTarg.transform.FindLoop(this.normal.data[i].ObjectName);
				if (!(null == gameObject))
				{
					SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
					if (!(null == component))
					{
						this.dictSmr[gameObject] = component;
						if (null != this.dictSmr[gameObject] && null != this.dictSmr[gameObject].sharedMesh)
						{
							Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(this.dictSmr[gameObject].sharedMesh);
							mesh.name = this.dictSmr[gameObject].sharedMesh.name;
							this.dictSmr[gameObject].sharedMesh = mesh;
						}
						this.dictNormal[gameObject] = this.normal.data[i];
						Vector3[] value = new Vector3[this.normal.data[i].NormalMin.Count];
						this.dictCalc[gameObject] = value;
					}
				}
			}
			this.CheckNormals(assetName);
			if (this.dictNormal.Count != 0)
			{
				this.initEnd = true;
			}
			return this.initEnd;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x00120A0F File Offset: 0x0011EE0F
		public void Release()
		{
			this.initEnd = false;
			this.normal = null;
			this.dictNormal.Clear();
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x00120A2C File Offset: 0x0011EE2C
		private void CheckNormals(string assetName)
		{
			if (this.dictNormal == null)
			{
				return;
			}
			foreach (KeyValuePair<GameObject, NormalData.Param> keyValuePair in this.dictNormal)
			{
				if (keyValuePair.Value.NormalMin.Count != keyValuePair.Value.NormalMax.Count)
				{
				}
			}
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x00120ABC File Offset: 0x0011EEBC
		public void Blend(float rate)
		{
			if (!this.initEnd)
			{
				return;
			}
			foreach (KeyValuePair<GameObject, NormalData.Param> keyValuePair in this.dictNormal)
			{
				if (keyValuePair.Value.NormalMin.Count == keyValuePair.Value.NormalMax.Count)
				{
					if (keyValuePair.Value.NormalMin.Count == this.dictSmr[keyValuePair.Key].sharedMesh.normals.Length)
					{
						for (int i = 0; i < keyValuePair.Value.NormalMin.Count; i++)
						{
							this.dictCalc[keyValuePair.Key][i] = Vector3.Lerp(keyValuePair.Value.NormalMin[i], keyValuePair.Value.NormalMax[i], rate);
						}
						this.dictSmr[keyValuePair.Key].sharedMesh.normals = this.dictCalc[keyValuePair.Key];
					}
				}
			}
		}

		// Token: 0x04002E04 RID: 11780
		private bool initEnd;

		// Token: 0x04002E05 RID: 11781
		private NormalData normal;

		// Token: 0x04002E06 RID: 11782
		private Dictionary<GameObject, NormalData.Param> dictNormal = new Dictionary<GameObject, NormalData.Param>();

		// Token: 0x04002E07 RID: 11783
		private Dictionary<GameObject, SkinnedMeshRenderer> dictSmr = new Dictionary<GameObject, SkinnedMeshRenderer>();

		// Token: 0x04002E08 RID: 11784
		private Dictionary<GameObject, Vector3[]> dictCalc = new Dictionary<GameObject, Vector3[]>();
	}
}
