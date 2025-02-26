using System;

// Token: 0x02000481 RID: 1153
public class MeshData
{
	// Token: 0x06001557 RID: 5463 RVA: 0x0008410F File Offset: 0x0008250F
	public void Write(UsCmd cmd)
	{
		cmd.WriteInt32(this._instID);
		cmd.WriteInt32(this._vertCount);
		cmd.WriteInt32(this._materialCount);
		cmd.WriteFloat(this._boundSize);
		cmd.WriteFloat(this._camDist);
	}

	// Token: 0x0400184C RID: 6220
	public int _instID;

	// Token: 0x0400184D RID: 6221
	public int _vertCount;

	// Token: 0x0400184E RID: 6222
	public int _materialCount;

	// Token: 0x0400184F RID: 6223
	public float _boundSize;

	// Token: 0x04001850 RID: 6224
	public float _camDist;
}
