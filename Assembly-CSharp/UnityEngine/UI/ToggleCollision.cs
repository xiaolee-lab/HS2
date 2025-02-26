using System;

namespace UnityEngine.UI
{
	// Token: 0x0200079C RID: 1948
	internal class ToggleCollision : Toggle, ICanvasRaycastFilter
	{
		// Token: 0x06002E30 RID: 11824 RVA: 0x001051AB File Offset: 0x001035AB
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(this._target, sp, eventCamera);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x001051BA File Offset: 0x001035BA
		protected override void Awake()
		{
			base.Awake();
			if (this._target == null)
			{
				this._target = (base.transform as RectTransform);
			}
		}

		// Token: 0x04002D17 RID: 11543
		[SerializeField]
		private RectTransform _target;
	}
}
