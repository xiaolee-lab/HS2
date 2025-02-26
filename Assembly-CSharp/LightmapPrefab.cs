using System;
using System.Linq;
using UnityEngine;

// Token: 0x020010FA RID: 4346
[ExecuteInEditMode]
public class LightmapPrefab : MonoBehaviour
{
	// Token: 0x0600900C RID: 36876 RVA: 0x003C033C File Offset: 0x003BE73C
	private void Start()
	{
		this.Resetup();
	}

	// Token: 0x0600900D RID: 36877 RVA: 0x003C0344 File Offset: 0x003BE744
	[ContextMenu("Setup")]
	private void Setup()
	{
		Renderer[] array = (from v in base.GetComponentsInChildren<Renderer>()
		where v.enabled && v.lightmapIndex >= 0
		select v).ToArray<Renderer>();
		this.lightmapParameters = new LightmapPrefab.LightmapParameter[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			Renderer renderer = array[i];
			this.lightmapParameters[i] = new LightmapPrefab.LightmapParameter
			{
				lightmapIndex = renderer.lightmapIndex,
				scaleOffset = renderer.lightmapScaleOffset,
				renderer = renderer
			};
		}
	}

	// Token: 0x0600900E RID: 36878 RVA: 0x003C03D4 File Offset: 0x003BE7D4
	[ContextMenu("Resetup")]
	public void Resetup()
	{
		if (this.lightmapParameters == null)
		{
			return;
		}
		int num = this.lightmapParameters.Length;
		for (int i = 0; i < num; i++)
		{
			if (this.lightmapParameters[i] != null)
			{
				this.lightmapParameters[i].UpdateLightmapUVs();
			}
		}
	}

	// Token: 0x040074DF RID: 29919
	[SerializeField]
	private LightmapPrefab.LightmapParameter[] lightmapParameters;

	// Token: 0x020010FB RID: 4347
	[Serializable]
	private class LightmapParameter
	{
		// Token: 0x06009011 RID: 36881 RVA: 0x003C0458 File Offset: 0x003BE858
		public void UpdateLightmapUVs()
		{
			if (this.renderer == null)
			{
				return;
			}
			if (this.lightmapIndex < 0)
			{
				return;
			}
			this.renderer.lightmapScaleOffset = this.scaleOffset;
			this.renderer.lightmapIndex = this.lightmapIndex;
		}

		// Token: 0x040074E1 RID: 29921
		public int lightmapIndex = -1;

		// Token: 0x040074E2 RID: 29922
		public Vector4 scaleOffset = Vector4.zero;

		// Token: 0x040074E3 RID: 29923
		public Renderer renderer;
	}
}
