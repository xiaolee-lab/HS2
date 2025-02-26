using System;
using UnityEngine;

// Token: 0x02000376 RID: 886
[Serializable]
public class EnviroTerrainSeasonsChangeOrder
{
	// Token: 0x04001150 RID: 4432
	public int terrainTextureID;

	// Token: 0x04001151 RID: 4433
	[Header("Textures")]
	public Texture2D SpringTexture;

	// Token: 0x04001152 RID: 4434
	public Texture2D SpringNormal;

	// Token: 0x04001153 RID: 4435
	public Texture2D SummerTexture;

	// Token: 0x04001154 RID: 4436
	public Texture2D SummerNormal;

	// Token: 0x04001155 RID: 4437
	public Texture2D AutumnTexture;

	// Token: 0x04001156 RID: 4438
	public Texture2D AutumnNormal;

	// Token: 0x04001157 RID: 4439
	public Texture2D WinterTexture;

	// Token: 0x04001158 RID: 4440
	public Texture2D WinterNormal;

	// Token: 0x04001159 RID: 4441
	public Vector2 tiling = new Vector2(10f, 10f);
}
