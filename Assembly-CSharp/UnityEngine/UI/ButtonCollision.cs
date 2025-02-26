using System;

namespace UnityEngine.UI
{
	// Token: 0x0200079A RID: 1946
	internal class ButtonCollision : Button, ICanvasRaycastFilter
	{
		// Token: 0x06002E2A RID: 11818 RVA: 0x00104E35 File Offset: 0x00103235
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(this._target, sp, eventCamera);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x00104E44 File Offset: 0x00103244
		protected override void Awake()
		{
			base.Awake();
			if (this._target == null)
			{
				this._target = (base.transform as RectTransform);
			}
		}

		// Token: 0x04002D13 RID: 11539
		[SerializeField]
		private RectTransform _target;
	}
}
