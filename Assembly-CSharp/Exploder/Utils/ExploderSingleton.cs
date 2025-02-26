using System;
using UnityEngine;

namespace Exploder.Utils
{
	// Token: 0x020003C8 RID: 968
	public class ExploderSingleton : MonoBehaviour
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x00064ED0 File Offset: 0x000632D0
		private void Awake()
		{
			ExploderSingleton.Instance = base.gameObject.GetComponent<ExploderObject>();
			ExploderSingleton.ExploderInstance = ExploderSingleton.Instance;
		}

		// Token: 0x04001318 RID: 4888
		[Obsolete("ExploderInstance is obsolete, please use Instance instead.")]
		public static ExploderObject ExploderInstance;

		// Token: 0x04001319 RID: 4889
		public static ExploderObject Instance;
	}
}
