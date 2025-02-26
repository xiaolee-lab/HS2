using System;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020010F9 RID: 4345
[CreateAssetMenu(fileName = "LightMapData", menuName = "LightMapData", order = 1)]
public class LightMapDataObject : ScriptableObject
{
	// Token: 0x06009009 RID: 36873 RVA: 0x003C0200 File Offset: 0x003BE600
	public static LightMapDataObject operator +(LightMapDataObject a, LightMapDataObject b)
	{
		List<Texture2D> list = a.light.ToList<Texture2D>();
		List<Texture2D> list2 = a.dir.ToList<Texture2D>();
		list.AddRange(b.light);
		list2.AddRange(b.dir);
		return new LightMapDataObject
		{
			lightProbes = a.lightProbes,
			cubemap = a.cubemap,
			lightmapsMode = a.lightmapsMode,
			light = list.ToArray(),
			dir = list2.ToArray()
		};
	}

	// Token: 0x0600900A RID: 36874 RVA: 0x003C0280 File Offset: 0x003BE680
	public void Change()
	{
		LightmapData[] array = new LightmapData[this.light.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new LightmapData
			{
				lightmapDir = this.dir[i],
				lightmapColor = this.light[i]
			};
		}
		LightmapSettings.lightmaps = array;
		LightmapSettings.lightProbes = this.lightProbes;
		LightmapSettings.lightmapsMode = this.lightmapsMode;
		if (this.cubemap != null)
		{
			RenderSettings.customReflection = this.cubemap;
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
		}
		else
		{
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
		}
		if (this.fog != null)
		{
			this.fog.Change();
		}
	}

	// Token: 0x040074D9 RID: 29913
	public LightProbes lightProbes;

	// Token: 0x040074DA RID: 29914
	public LightmapsMode lightmapsMode;

	// Token: 0x040074DB RID: 29915
	public Cubemap cubemap;

	// Token: 0x040074DC RID: 29916
	public Texture2D[] light;

	// Token: 0x040074DD RID: 29917
	public Texture2D[] dir;

	// Token: 0x040074DE RID: 29918
	public Scene.FogData fog = new Scene.FogData();
}
