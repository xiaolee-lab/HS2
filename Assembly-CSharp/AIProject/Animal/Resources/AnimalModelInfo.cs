using System;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal.Resources
{
	// Token: 0x02000B91 RID: 2961
	public struct AnimalModelInfo
	{
		// Token: 0x06005837 RID: 22583 RVA: 0x0025F1E0 File Offset: 0x0025D5E0
		public AnimalModelInfo(AssetBundleInfo _assetInfo)
		{
			this.AssetInfo = _assetInfo;
			this.EyesShapeInfo = default(AnimalShapeInfo);
			this.MouthShapeInfo = default(AnimalShapeInfo);
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0025F212 File Offset: 0x0025D612
		public AnimalModelInfo(AssetBundleInfo _assetInfo, string _eyesName, int _eyesShapeIndex, string _mouthsName, int _mouthShapeIndex)
		{
			this.AssetInfo = _assetInfo;
			this.EyesShapeInfo = new AnimalShapeInfo(!_eyesName.IsNullOrEmpty(), _eyesName, _eyesShapeIndex);
			this.MouthShapeInfo = new AnimalShapeInfo(!_mouthsName.IsNullOrEmpty(), _mouthsName, _mouthShapeIndex);
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0025F24A File Offset: 0x0025D64A
		public AnimalModelInfo(AssetBundleInfo _assetInfo, bool _eyesShapeEnabled, string _eyesName, int _eyesShapeIndex, bool _mouthShapeEnabled, string _mouthName, int _mouthShapeIndex)
		{
			this.AssetInfo = _assetInfo;
			this.EyesShapeInfo = new AnimalShapeInfo(_eyesShapeEnabled, _eyesName, _eyesShapeIndex);
			this.MouthShapeInfo = new AnimalShapeInfo(_mouthShapeEnabled, _mouthName, _mouthShapeIndex);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0025F273 File Offset: 0x0025D673
		public AnimalModelInfo(AssetBundleInfo _assetInfo, AnimalShapeInfo _eyesShapeInfo, AnimalShapeInfo _mouthShapeInfo)
		{
			this.AssetInfo = _assetInfo;
			this.EyesShapeInfo = _eyesShapeInfo;
			this.MouthShapeInfo = _mouthShapeInfo;
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0025F28A File Offset: 0x0025D68A
		public void ClearShapeState()
		{
			this.EyesShapeInfo.ClearState();
			this.MouthShapeInfo.ClearState();
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0025F2A2 File Offset: 0x0025D6A2
		public void SetShapeState(Transform _transform)
		{
			this.EyesShapeInfo.SetRenderer(_transform, null);
			this.MouthShapeInfo.SetRenderer(_transform, null);
		}

		// Token: 0x040050F8 RID: 20728
		public AssetBundleInfo AssetInfo;

		// Token: 0x040050F9 RID: 20729
		public AnimalShapeInfo EyesShapeInfo;

		// Token: 0x040050FA RID: 20730
		public AnimalShapeInfo MouthShapeInfo;
	}
}
