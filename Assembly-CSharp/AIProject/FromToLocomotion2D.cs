using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E7A RID: 3706
	public abstract class FromToLocomotion2D : MonoBehaviour
	{
		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06007649 RID: 30281 RVA: 0x00321923 File Offset: 0x0031FD23
		public RectTransform AnimationRoot
		{
			[CompilerGenerated]
			get
			{
				return this._animationRoot;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600764A RID: 30282 RVA: 0x0032192B File Offset: 0x0031FD2B
		// (set) Token: 0x0600764B RID: 30283 RVA: 0x00321933 File Offset: 0x0031FD33
		public Vector2 Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x0600764C RID: 30284 RVA: 0x0032193C File Offset: 0x0031FD3C
		// (set) Token: 0x0600764D RID: 30285 RVA: 0x00321944 File Offset: 0x0031FD44
		public Vector2 Destination
		{
			get
			{
				return this._destination;
			}
			set
			{
				this._destination = value;
			}
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x0032194D File Offset: 0x0031FD4D
		private void Awake()
		{
		}

		// Token: 0x0600764F RID: 30287 RVA: 0x00321950 File Offset: 0x0031FD50
		protected void SetPosition(Vector2 diff, float t)
		{
			if (this._animationRoot == null)
			{
				return;
			}
			Vector2 anchoredPosition = this._destination + diff * t;
			this._animationRoot.anchoredPosition = anchoredPosition;
		}

		// Token: 0x04006035 RID: 24629
		[SerializeField]
		protected RectTransform _animationRoot;

		// Token: 0x04006036 RID: 24630
		[SerializeField]
		protected EasingPair _alphaFadingTypes = default(EasingPair);

		// Token: 0x04006037 RID: 24631
		[SerializeField]
		protected EasingPair _motionTypes = default(EasingPair);

		// Token: 0x04006038 RID: 24632
		[SerializeField]
		protected Vector2 _source = Vector2.zero;

		// Token: 0x04006039 RID: 24633
		[SerializeField]
		protected Vector2 _destination = Vector2.zero;

		// Token: 0x0400603A RID: 24634
		protected const float _fadeDuration = 0.3f;
	}
}
