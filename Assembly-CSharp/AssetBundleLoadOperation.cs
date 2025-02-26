using System;
using System.Collections;

// Token: 0x02001126 RID: 4390
public abstract class AssetBundleLoadOperation : IEnumerator
{
	// Token: 0x17001F42 RID: 8002
	// (get) Token: 0x0600915A RID: 37210 RVA: 0x003C7430 File Offset: 0x003C5830
	public object Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600915B RID: 37211 RVA: 0x003C7433 File Offset: 0x003C5833
	public bool MoveNext()
	{
		return !this.IsDone();
	}

	// Token: 0x0600915C RID: 37212 RVA: 0x003C743E File Offset: 0x003C583E
	public void Reset()
	{
	}

	// Token: 0x0600915D RID: 37213
	public abstract bool Update();

	// Token: 0x0600915E RID: 37214
	public abstract bool IsDone();
}
