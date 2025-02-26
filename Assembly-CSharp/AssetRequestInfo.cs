using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004B7 RID: 1207
public class AssetRequestInfo
{
	// Token: 0x06001645 RID: 5701 RVA: 0x00088DC4 File Offset: 0x000871C4
	public override string ToString()
	{
		Scene activeScene = SceneManager.GetActiveScene();
		return string.Format("{0:0.00} {1} {2} {3} {4:0.00}ms {5}", new object[]
		{
			this.requestTime,
			(!(this.resourceType != null)) ? "<null_type>" : this.resourceType.ToString(),
			this.resourcePath,
			activeScene.name,
			this.duration,
			(this.requestType != ResourceRequestType.Async) ? "sync" : "async"
		});
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x00088E60 File Offset: 0x00087260
	public void RecordObject(UnityEngine.Object obj)
	{
		if (obj.name == "LuaAsset")
		{
			this.rootID = -1;
			this.resourceType = AssetRequestInfo.LuaAsset.Instance.GetType();
		}
		else
		{
			this.rootID = obj.GetInstanceID();
			this.resourceType = obj.GetType();
		}
	}

	// Token: 0x04001900 RID: 6400
	public int seqID;

	// Token: 0x04001901 RID: 6401
	public int rootID;

	// Token: 0x04001902 RID: 6402
	public ResourceRequestType requestType;

	// Token: 0x04001903 RID: 6403
	public string resourcePath = string.Empty;

	// Token: 0x04001904 RID: 6404
	public Type resourceType;

	// Token: 0x04001905 RID: 6405
	public string srcFile = string.Empty;

	// Token: 0x04001906 RID: 6406
	public int srcLineNum;

	// Token: 0x04001907 RID: 6407
	public double requestTime = (double)Time.realtimeSinceStartup;

	// Token: 0x04001908 RID: 6408
	public int stacktraceHash;

	// Token: 0x04001909 RID: 6409
	public double duration;

	// Token: 0x0400190A RID: 6410
	public bool isAsync;

	// Token: 0x020004B8 RID: 1208
	private class LuaAsset : UnityEngine.Object
	{
		// Token: 0x0400190B RID: 6411
		public static AssetRequestInfo.LuaAsset Instance = new AssetRequestInfo.LuaAsset();
	}
}
