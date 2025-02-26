using System;

namespace AIProject.Animal
{
	// Token: 0x02000B4D RID: 2893
	public struct AnimalAssetInfo
	{
		// Token: 0x06005642 RID: 22082 RVA: 0x00258E5E File Offset: 0x0025725E
		public AnimalAssetInfo(int _TableID, string _BundleName, string _AssetName, string _ManifestName)
		{
			this.TableID = _TableID;
			this.BundleName = _BundleName;
			this.AssetName = _AssetName;
			this.ManifestName = _ManifestName;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005643 RID: 22083 RVA: 0x00258E7D File Offset: 0x0025727D
		// (set) Token: 0x06005644 RID: 22084 RVA: 0x00258E85 File Offset: 0x00257285
		public int TableID { get; private set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005645 RID: 22085 RVA: 0x00258E8E File Offset: 0x0025728E
		// (set) Token: 0x06005646 RID: 22086 RVA: 0x00258E96 File Offset: 0x00257296
		public string BundleName { get; private set; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005647 RID: 22087 RVA: 0x00258E9F File Offset: 0x0025729F
		// (set) Token: 0x06005648 RID: 22088 RVA: 0x00258EA7 File Offset: 0x002572A7
		public string AssetName { get; private set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x00258EB0 File Offset: 0x002572B0
		// (set) Token: 0x0600564A RID: 22090 RVA: 0x00258EB8 File Offset: 0x002572B8
		public string ManifestName { get; private set; }
	}
}
