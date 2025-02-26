using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ADV
{
	// Token: 0x020006C6 RID: 1734
	public class Root : MonoBehaviour
	{
		// Token: 0x06002916 RID: 10518 RVA: 0x000F1C34 File Offset: 0x000F0034
		public static Root Load(Transform parent)
		{
			string assetBundleName = "adv/root.unity3d";
			AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, "ADV_Root", typeof(GameObject), null);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
			return UnityEngine.Object.Instantiate<GameObject>(assetBundleLoadAssetOperation.GetAsset<GameObject>(), parent, false).GetComponent<Root>();
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000F1C79 File Offset: 0x000F0079
		public void SetBackup()
		{
			BackupPosRot cameraRootBK = this.cameraRootBK;
			if (cameraRootBK != null)
			{
				cameraRootBK.Set(this._cameraRoot);
			}
			BackupPosRot charaRootBK = this.charaRootBK;
			if (charaRootBK != null)
			{
				charaRootBK.Set(this._charaRoot);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000F1CAF File Offset: 0x000F00AF
		public Dictionary<string, Transform> characterStandNulls
		{
			get
			{
				return this.GetCache(ref this._characterStandNulls, () => (from i in Enumerable.Range(0, this.charaStartIndex)
				select this._charaRoot.GetChild(i)).ToDictionary((Transform v) => v.name, (Transform v) => v, StringComparer.InvariantCultureIgnoreCase));
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x000F1CC9 File Offset: 0x000F00C9
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x000F1CD1 File Offset: 0x000F00D1
		private BackupPosRot cameraRootBK { get; set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x000F1CDA File Offset: 0x000F00DA
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x000F1CE2 File Offset: 0x000F00E2
		private BackupPosRot charaRootBK { get; set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000F1CEB File Offset: 0x000F00EB
		public Transform nullRoot
		{
			[CompilerGenerated]
			get
			{
				return this._nullRoot;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000F1CF3 File Offset: 0x000F00F3
		public Transform baseRoot
		{
			[CompilerGenerated]
			get
			{
				return this._baseRoot;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000F1CFB File Offset: 0x000F00FB
		public Transform cameraRoot
		{
			[CompilerGenerated]
			get
			{
				return this._cameraRoot;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000F1D03 File Offset: 0x000F0103
		public Transform charaRoot
		{
			[CompilerGenerated]
			get
			{
				return this._charaRoot;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000F1D0B File Offset: 0x000F010B
		public Transform eventCGRoot
		{
			[CompilerGenerated]
			get
			{
				return this._eventCGRoot;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000F1D13 File Offset: 0x000F0113
		public Transform objectRoot
		{
			[CompilerGenerated]
			get
			{
				return this._objectRoot;
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000F1D1C File Offset: 0x000F011C
		private void Awake()
		{
			if (this._cameraRoot != null)
			{
				this.cameraRootBK = new BackupPosRot(this._cameraRoot);
			}
			if (this._charaRoot != null)
			{
				this.charaStartIndex = this._charaRoot.childCount;
				this.charaRootBK = new BackupPosRot(this._charaRoot);
			}
		}

		// Token: 0x04002A5D RID: 10845
		private Dictionary<string, Transform> _characterStandNulls;

		// Token: 0x04002A5E RID: 10846
		private int charaStartIndex;

		// Token: 0x04002A61 RID: 10849
		[SerializeField]
		private Transform _nullRoot;

		// Token: 0x04002A62 RID: 10850
		[SerializeField]
		private Transform _baseRoot;

		// Token: 0x04002A63 RID: 10851
		[SerializeField]
		private Transform _cameraRoot;

		// Token: 0x04002A64 RID: 10852
		[SerializeField]
		private Transform _charaRoot;

		// Token: 0x04002A65 RID: 10853
		[SerializeField]
		private Transform _eventCGRoot;

		// Token: 0x04002A66 RID: 10854
		[SerializeField]
		private Transform _objectRoot;
	}
}
