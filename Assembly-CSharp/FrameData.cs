using System;
using System.Collections.Generic;

// Token: 0x02000480 RID: 1152
public class FrameData
{
	// Token: 0x06001555 RID: 5461 RVA: 0x00084094 File Offset: 0x00082494
	public UsCmd CreatePacket()
	{
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_FrameDataV2);
		usCmd.WriteInt32(this._frameCount);
		usCmd.WriteFloat(this._frameDeltaTime);
		usCmd.WriteFloat(this._frameRealTime);
		usCmd.WriteFloat(this._frameStartTime);
		UsCmdUtil.WriteIntList(usCmd, this._frameMeshes);
		UsCmdUtil.WriteIntList(usCmd, this._frameMaterials);
		UsCmdUtil.WriteIntList(usCmd, this._frameTextures);
		return usCmd;
	}

	// Token: 0x04001845 RID: 6213
	public int _frameCount;

	// Token: 0x04001846 RID: 6214
	public float _frameDeltaTime;

	// Token: 0x04001847 RID: 6215
	public float _frameRealTime;

	// Token: 0x04001848 RID: 6216
	public float _frameStartTime;

	// Token: 0x04001849 RID: 6217
	public List<int> _frameMeshes = new List<int>();

	// Token: 0x0400184A RID: 6218
	public List<int> _frameMaterials = new List<int>();

	// Token: 0x0400184B RID: 6219
	public List<int> _frameTextures = new List<int>();
}
