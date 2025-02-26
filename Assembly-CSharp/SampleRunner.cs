using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class SampleRunner : MonoBehaviour
{
	// Token: 0x0600160F RID: 5647 RVA: 0x00087A33 File Offset: 0x00085E33
	private void SendEditorCommand(string cmd)
	{
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x00087A38 File Offset: 0x00085E38
	private void Start()
	{
		CoroutineRuntimeTrackingConfig.EnableTracking = true;
		base.StartCoroutine(RuntimeCoroutineStats.Instance.BroadcastCoroutine());
		this.SendEditorCommand("AppStarted");
		base.gameObject.AddComponent<TestPluginRunner>();
		if (SampleRunner.<>f__mg$cache0 == null)
		{
			SampleRunner.<>f__mg$cache0 = new CoroutineStartHandler_IEnumerator(RuntimeCoroutineTracker.InvokeStart);
		}
		CoroutinePluginForwarder.InvokeStart_IEnumerator = SampleRunner.<>f__mg$cache0;
		if (SampleRunner.<>f__mg$cache1 == null)
		{
			SampleRunner.<>f__mg$cache1 = new CoroutineStartHandler_String(RuntimeCoroutineTracker.InvokeStart);
		}
		CoroutinePluginForwarder.InvokeStart_String = SampleRunner.<>f__mg$cache1;
		CoroutineSpawner initiator = base.gameObject.AddComponent<CoroutineSpawner>();
		RuntimeCoroutineTracker.InvokeStart(initiator, "Co01_WaitForSeconds", null);
		RuntimeCoroutineTracker.InvokeStart(initiator, "Co02_PerFrame_NULL", null);
		RuntimeCoroutineTracker.InvokeStart(initiator, "Co03_PerFrame_EOF", null);
		RuntimeCoroutineTracker.InvokeStart(initiator, "Co04_PerFrame_ARG", 0.683f);
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x00087B00 File Offset: 0x00085F00
	private void OnDestroy()
	{
		this.SendEditorCommand("AppDestroyed");
	}

	// Token: 0x040018DA RID: 6362
	[CompilerGenerated]
	private static CoroutineStartHandler_IEnumerator <>f__mg$cache0;

	// Token: 0x040018DB RID: 6363
	[CompilerGenerated]
	private static CoroutineStartHandler_String <>f__mg$cache1;
}
