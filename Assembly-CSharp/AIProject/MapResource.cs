using System;
using System.Collections;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F18 RID: 3864
	public class MapResource : MonoBehaviour
	{
		// Token: 0x06007EAB RID: 32427 RVA: 0x0035E1E0 File Offset: 0x0035C5E0
		public IEnumerator Load()
		{
			foreach (string sourceName in this._prefabs)
			{
				GameObject prefab = AssetUtility.LoadAsset<GameObject>(this._assetBundleName, sourceName, this._manifestName);
				UnityEngine.Object.Instantiate<GameObject>(prefab, base.transform, true);
				yield return null;
			}
			yield break;
		}

		// Token: 0x04006627 RID: 26151
		[SerializeField]
		private string _assetBundleName = string.Empty;

		// Token: 0x04006628 RID: 26152
		[SerializeField]
		private string _manifestName = string.Empty;

		// Token: 0x04006629 RID: 26153
		[SerializeField]
		private string[] _prefabs = Array.Empty<string>();
	}
}
