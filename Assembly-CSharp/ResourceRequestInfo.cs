using System;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class ResourceRequestInfo
{
	// Token: 0x06001613 RID: 5651 RVA: 0x00087B38 File Offset: 0x00085F38
	public override string ToString()
	{
		return string.Format("#{0} ({1:0.000}) {2} {3} {4} +{5} +{6} ({7})", new object[]
		{
			this.seqID,
			this.requestTime,
			this.rootID,
			(!(this.resourceType != null)) ? null : this.resourceType.ToString(),
			(this.requestType != ResourceRequestType.Async) ? string.Empty : "(a)",
			this.resourcePath,
			this.srcFile,
			this.srcLineNum
		});
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x00087BE2 File Offset: 0x00085FE2
	public void RecordObject(UnityEngine.Object obj)
	{
		this.rootID = obj.GetInstanceID();
		this.resourceType = obj.GetType();
	}

	// Token: 0x040018DF RID: 6367
	public int seqID;

	// Token: 0x040018E0 RID: 6368
	public int rootID;

	// Token: 0x040018E1 RID: 6369
	public ResourceRequestType requestType;

	// Token: 0x040018E2 RID: 6370
	public string resourcePath = string.Empty;

	// Token: 0x040018E3 RID: 6371
	public Type resourceType;

	// Token: 0x040018E4 RID: 6372
	public string srcFile = string.Empty;

	// Token: 0x040018E5 RID: 6373
	public int srcLineNum;

	// Token: 0x040018E6 RID: 6374
	public double requestTime = (double)Time.realtimeSinceStartup;

	// Token: 0x040018E7 RID: 6375
	public int stacktraceHash;
}
