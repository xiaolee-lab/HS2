using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082A RID: 2090
public class FindAssist
{
	// Token: 0x1700099A RID: 2458
	// (get) Token: 0x0600352D RID: 13613 RVA: 0x0013A264 File Offset: 0x00138664
	// (set) Token: 0x0600352E RID: 13614 RVA: 0x0013A26C File Offset: 0x0013866C
	public Dictionary<string, GameObject> dictObjName { get; private set; }

	// Token: 0x1700099B RID: 2459
	// (get) Token: 0x0600352F RID: 13615 RVA: 0x0013A275 File Offset: 0x00138675
	// (set) Token: 0x06003530 RID: 13616 RVA: 0x0013A27D File Offset: 0x0013867D
	public Dictionary<string, List<GameObject>> dictTagName { get; private set; }

	// Token: 0x06003531 RID: 13617 RVA: 0x0013A286 File Offset: 0x00138686
	public void Initialize(Transform trf)
	{
		this.dictObjName = new Dictionary<string, GameObject>();
		this.dictTagName = new Dictionary<string, List<GameObject>>();
		this.FindAll(trf);
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x0013A2A8 File Offset: 0x001386A8
	private void FindAll(Transform trf)
	{
		if (!this.dictObjName.ContainsKey(trf.name))
		{
			this.dictObjName[trf.name] = trf.gameObject;
		}
		string tag = trf.tag;
		if (string.Empty != tag)
		{
			List<GameObject> list = null;
			if (!this.dictTagName.TryGetValue(tag, out list))
			{
				list = new List<GameObject>();
				this.dictTagName[tag] = list;
			}
			list.Add(trf.gameObject);
		}
		for (int i = 0; i < trf.childCount; i++)
		{
			this.FindAll(trf.GetChild(i));
		}
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x0013A354 File Offset: 0x00138754
	public GameObject GetObjectFromName(string objName)
	{
		if (this.dictObjName == null)
		{
			return null;
		}
		GameObject result = null;
		this.dictObjName.TryGetValue(objName, out result);
		return result;
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x0013A380 File Offset: 0x00138780
	public Transform GetTransformFromName(string objName)
	{
		if (this.dictObjName == null)
		{
			return null;
		}
		GameObject gameObject = null;
		if (this.dictObjName.TryGetValue(objName, out gameObject))
		{
			return gameObject.transform;
		}
		return null;
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x0013A3B8 File Offset: 0x001387B8
	public List<GameObject> GetObjectFromTag(string tagName)
	{
		if (this.dictTagName == null)
		{
			return null;
		}
		List<GameObject> result = null;
		this.dictTagName.TryGetValue(tagName, out result);
		return result;
	}
}
