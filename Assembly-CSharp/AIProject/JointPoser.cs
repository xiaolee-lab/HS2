using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ReMotion;
using RootMotion.FinalIK;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C72 RID: 3186
	public class JointPoser : Poser
	{
		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x002C8E8D File Offset: 0x002C728D
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x002C8E95 File Offset: 0x002C7295
		public bool LeftRight { get; set; }

		// Token: 0x06006880 RID: 26752 RVA: 0x002C8E9E File Offset: 0x002C729E
		private void OnEnable()
		{
			ObservableEasing.Linear(0.5f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				this.weight = x.Value;
			});
		}

		// Token: 0x06006881 RID: 26753 RVA: 0x002C8EC3 File Offset: 0x002C72C3
		private void OnDisable()
		{
			ObservableEasing.Linear(0.5f, false).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				this.weight = 1f - x.Value;
			});
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x002C8EE8 File Offset: 0x002C72E8
		public void LoadJointData(string assetbundleName, string assetName)
		{
			JointPoser.JointData jointData = AssetUtility.LoadAsset<JointPoser.JointData>(assetbundleName, assetName, string.Empty);
			this._jointData = jointData;
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x002C8F0C File Offset: 0x002C730C
		protected override void InitiatePoser()
		{
			this._children = (from x in base.GetComponentsInChildren<Transform>(true)
			where this._jointData.NullList.Contains(x.name)
			select x).ToArray<Transform>();
			this._defaultLocalPositions = new Vector3[this._children.Length];
			this._defaultLocalRotations = new Quaternion[this._children.Length];
			for (int i = 0; i < this._children.Length; i++)
			{
				this._defaultLocalPositions[i] = this._children[i].localPosition;
				this._defaultLocalRotations[i] = this._children[i].localRotation;
			}
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x002C8FB8 File Offset: 0x002C73B8
		public override void AutoMapping()
		{
			if (this.poseRoot == null)
			{
				this._poseChildren = new Transform[0];
			}
			else
			{
				this._poseChildren = (from x in this.poseRoot.GetComponentsInChildren<Transform>(true)
				where this._jointData.NullList.Contains(x.name)
				select x).ToArray<Transform>();
			}
			this._poseRoot = this.poseRoot;
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x002C901C File Offset: 0x002C741C
		protected override void FixPoserTransforms()
		{
			for (int i = 0; i < this._children.Length; i++)
			{
				this._children[i].localPosition = this._defaultLocalPositions[i];
				this._children[i].localRotation = this._defaultLocalRotations[i];
			}
		}

		// Token: 0x06006886 RID: 26758 RVA: 0x002C9080 File Offset: 0x002C7480
		protected override void UpdatePoser()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (this.localPositionWeight <= 0f && this.localRotationWeight <= 0f)
			{
				return;
			}
			if (this._poseRoot != this.poseRoot)
			{
				this.AutoMapping();
			}
			if (this.poseRoot == null)
			{
				return;
			}
			if (this._children.Length != this._poseChildren.Length)
			{
				return;
			}
			float t = this.localRotationWeight * this.weight;
			float t2 = this.localPositionWeight * this.weight;
			for (int i = 0; i < this._children.Length; i++)
			{
				if (this._children[i] != base.transform)
				{
					this._children[i].localRotation = Quaternion.Lerp(this._children[i].localRotation, this._poseChildren[i].localRotation, t);
					this._children[i].localPosition = Vector3.Lerp(this._children[i].localPosition, this._poseChildren[i].localPosition, t2);
				}
			}
		}

		// Token: 0x04005940 RID: 22848
		[SerializeField]
		private JointPoser.JointData _jointData;

		// Token: 0x04005941 RID: 22849
		private Transform _poseRoot;

		// Token: 0x04005942 RID: 22850
		private Transform[] _children;

		// Token: 0x04005943 RID: 22851
		private Transform[] _poseChildren;

		// Token: 0x04005944 RID: 22852
		private Vector3[] _defaultLocalPositions;

		// Token: 0x04005945 RID: 22853
		private Quaternion[] _defaultLocalRotations;

		// Token: 0x02000C73 RID: 3187
		public class JointData : ScriptableObject
		{
			// Token: 0x17001506 RID: 5382
			// (get) Token: 0x0600688C RID: 26764 RVA: 0x002C9206 File Offset: 0x002C7606
			public string[] NullList
			{
				[CompilerGenerated]
				get
				{
					return this._nullList;
				}
			}

			// Token: 0x04005947 RID: 22855
			[SerializeField]
			private string[] _nullList;
		}
	}
}
