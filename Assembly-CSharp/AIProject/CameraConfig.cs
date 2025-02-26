using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace AIProject
{
	// Token: 0x02000850 RID: 2128
	public class CameraConfig : MonoBehaviour
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x0014029C File Offset: 0x0013E69C
		public PostProcessProfile PPProfile
		{
			[CompilerGenerated]
			get
			{
				return this._ppProfile;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x001402A4 File Offset: 0x0013E6A4
		public PostProcessLayer PPLayer
		{
			[CompilerGenerated]
			get
			{
				return this._ppLayer;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x001402AC File Offset: 0x0013E6AC
		public EnviroSky EnviroSky
		{
			[CompilerGenerated]
			get
			{
				return this._enviroSky;
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x001402B4 File Offset: 0x0013E6B4
		private void Reset()
		{
		}

		// Token: 0x04003697 RID: 13975
		[SerializeField]
		private PostProcessProfile _ppProfile;

		// Token: 0x04003698 RID: 13976
		[SerializeField]
		private PostProcessLayer _ppLayer;

		// Token: 0x04003699 RID: 13977
		[SerializeField]
		private EnviroSky _enviroSky;
	}
}
