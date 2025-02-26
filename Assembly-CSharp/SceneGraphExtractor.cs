using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B5 RID: 1205
public class SceneGraphExtractor
{
	// Token: 0x0600163D RID: 5693 RVA: 0x000889F0 File Offset: 0x00086DF0
	public SceneGraphExtractor(UnityEngine.Object root)
	{
		this.m_root = root;
		foreach (string key in SceneGraphExtractor.MemCategories)
		{
			this.MemObjectIDs[key] = new List<int>();
		}
		GameObject gameObject = this.m_root as GameObject;
		if (gameObject != null)
		{
			this.ProcessRecursively(gameObject);
			this.ExtractComponentIDs<Camera>(gameObject);
			if (SceneGraphExtractor.UIWidgetExtractor != null)
			{
				List<UnityEngine.Object> list = SceneGraphExtractor.UIWidgetExtractor(gameObject);
				foreach (UnityEngine.Object obj in list)
				{
					this.CountMemObject(obj);
				}
			}
			foreach (MeshFilter meshFilter in gameObject.GetComponentsInChildren(typeof(MeshFilter), true))
			{
				Mesh sharedMesh = meshFilter.sharedMesh;
				this.CountMemObject(sharedMesh);
			}
			foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer), true))
			{
				if (renderer.sharedMaterial != null)
				{
					this.CountMemObject(renderer.sharedMaterial);
					List<Texture2D> texture2DObjsFromMaterial = ResourceTracker.Instance.GetTexture2DObjsFromMaterial(renderer.sharedMaterial);
					foreach (Texture2D obj2 in texture2DObjsFromMaterial)
					{
						this.CountMemObject(obj2);
					}
				}
			}
			this.ExtractComponentIDs<Animator>(gameObject);
			this.ExtractComponentIDs<ParticleSystem>(gameObject);
		}
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x00088C08 File Offset: 0x00087008
	private void CountMemObject(UnityEngine.Object obj)
	{
		List<int> list = null;
		if (obj != null && this.MemObjectIDs.TryGetValue(obj.GetType().Name, out list) && list != null && !list.Contains(obj.GetInstanceID()))
		{
			list.Add(obj.GetInstanceID());
		}
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x00088C64 File Offset: 0x00087064
	private void ExtractComponentIDs<T>(GameObject go) where T : Component
	{
		Component[] componentsInChildren = go.GetComponentsInChildren(typeof(T), true);
		Component[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			T t = (T)((object)array[i]);
			this.CountMemObject(t);
		}
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x00088CB0 File Offset: 0x000870B0
	public void ProcessRecursively(GameObject obj)
	{
		if (!this.GameObjectIDs.Contains(obj.GetInstanceID()))
		{
			this.GameObjectIDs.Add(obj.GetInstanceID());
		}
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			GameObject gameObject = obj.transform.GetChild(i).gameObject;
			if (gameObject != null)
			{
				this.ProcessRecursively(gameObject);
			}
		}
	}

	// Token: 0x040018FA RID: 6394
	public UnityEngine.Object m_root;

	// Token: 0x040018FB RID: 6395
	public List<int> GameObjectIDs = new List<int>();

	// Token: 0x040018FC RID: 6396
	public static List<string> MemCategories = new List<string>
	{
		"Texture2D",
		"AnimationClip",
		"Mesh",
		"Font",
		"ParticleSystem",
		"Camera"
	};

	// Token: 0x040018FD RID: 6397
	public static AdditionalMemObjectExtractor UIWidgetExtractor;

	// Token: 0x040018FE RID: 6398
	public Dictionary<string, List<int>> MemObjectIDs = new Dictionary<string, List<int>>();
}
