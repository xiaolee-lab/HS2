using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ADV
{
	// Token: 0x020006C1 RID: 1729
	[Serializable]
	public class OpenData
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000F19D5 File Offset: 0x000EFDD5
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x000F19DD File Offset: 0x000EFDDD
		public string bundle
		{
			get
			{
				return this._bundle;
			}
			set
			{
				this._bundle = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000F19E6 File Offset: 0x000EFDE6
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000F19EE File Offset: 0x000EFDEE
		public string asset
		{
			get
			{
				return this._asset;
			}
			set
			{
				this._asset = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000F19F7 File Offset: 0x000EFDF7
		public ScenarioData data
		{
			[CompilerGenerated]
			get
			{
				return this._data;
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000F19FF File Offset: 0x000EFDFF
		public void Set(OpenData openData)
		{
			this._asset = openData._asset;
			this._bundle = openData._bundle;
			this._data = openData._data;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000F1A25 File Offset: 0x000EFE25
		public bool HasData
		{
			[CompilerGenerated]
			get
			{
				return this._data != null;
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000F1A33 File Offset: 0x000EFE33
		public void ClearData()
		{
			this._data = null;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000F1A3C File Offset: 0x000EFE3C
		public void Clear()
		{
			this._asset = null;
			this._bundle = null;
			this.ClearData();
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000F1A52 File Offset: 0x000EFE52
		public void Load()
		{
			this._data = AssetBundleManager.LoadAsset(this._bundle, this._asset, typeof(ScenarioData), null).GetAsset<ScenarioData>();
			AssetBundleManager.UnloadAssetBundle(this._bundle, false, null, false);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000F1A8C File Offset: 0x000EFE8C
		public void Load(string bundle, string asset)
		{
			bool flag = !this.HasData;
			if (this._asset != asset)
			{
				this._asset = asset;
				flag = true;
			}
			if (this._bundle != bundle)
			{
				this._bundle = bundle;
				flag = true;
			}
			if (flag)
			{
				this.Load();
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000F1AE3 File Offset: 0x000EFEE3
		public void FindLoad(string asset, string path)
		{
			this._asset = asset;
			this._bundle = Program.FindADVBundleFilePath(path, asset, out this._data);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000F1AFF File Offset: 0x000EFEFF
		public void FindLoad(string asset, int charaID, int category)
		{
			this._asset = asset;
			this._bundle = Program.FindADVBundleFilePath(charaID, category, asset, out this._data);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000F1B1C File Offset: 0x000EFF1C
		public void FindLoadMessage(string category, string asset)
		{
			this._asset = asset;
			this._bundle = Program.FindMessageADVBundleFilePath(category, asset, out this._data);
		}

		// Token: 0x04002A57 RID: 10839
		[SerializeField]
		private string _bundle = string.Empty;

		// Token: 0x04002A58 RID: 10840
		[SerializeField]
		private string _asset = string.Empty;

		// Token: 0x04002A59 RID: 10841
		private ScenarioData _data;
	}
}
