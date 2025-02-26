using System;
using UnityEngine;

// Token: 0x02001122 RID: 4386
public class WireFrameRender : MonoBehaviour
{
	// Token: 0x06009131 RID: 37169 RVA: 0x003C702F File Offset: 0x003C542F
	private void OnPreRender()
	{
		if (this.wireFrameDraw)
		{
			GL.wireframe = true;
		}
	}

	// Token: 0x06009132 RID: 37170 RVA: 0x003C7042 File Offset: 0x003C5442
	private void OnPostRender()
	{
		if (this.wireFrameDraw)
		{
			GL.wireframe = false;
		}
	}

	// Token: 0x040075C5 RID: 30149
	public bool wireFrameDraw;
}
