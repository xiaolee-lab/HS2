using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000566 RID: 1382
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class UIImageHelper : MonoBehaviour
	{
		// Token: 0x06001D17 RID: 7447 RVA: 0x000AB4BC File Offset: 0x000A98BC
		public void SetEnabledState(bool newState)
		{
			this.currentState = newState;
			UIImageHelper.State state = (!newState) ? this.disabledState : this.enabledState;
			if (state == null)
			{
				return;
			}
			Image component = base.gameObject.GetComponent<Image>();
			if (component == null)
			{
				UnityEngine.Debug.LogError("Image is missing!");
				return;
			}
			state.Set(component);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000AB519 File Offset: 0x000A9919
		public void SetEnabledStateColor(Color color)
		{
			this.enabledState.color = color;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000AB527 File Offset: 0x000A9927
		public void SetDisabledStateColor(Color color)
		{
			this.disabledState.color = color;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000AB538 File Offset: 0x000A9938
		public void Refresh()
		{
			UIImageHelper.State state = (!this.currentState) ? this.disabledState : this.enabledState;
			Image component = base.gameObject.GetComponent<Image>();
			if (component == null)
			{
				return;
			}
			state.Set(component);
		}

		// Token: 0x04001E2F RID: 7727
		[SerializeField]
		private UIImageHelper.State enabledState;

		// Token: 0x04001E30 RID: 7728
		[SerializeField]
		private UIImageHelper.State disabledState;

		// Token: 0x04001E31 RID: 7729
		private bool currentState;

		// Token: 0x02000567 RID: 1383
		[Serializable]
		private class State
		{
			// Token: 0x06001D1C RID: 7452 RVA: 0x000AB58A File Offset: 0x000A998A
			public void Set(Image image)
			{
				if (image == null)
				{
					return;
				}
				image.color = this.color;
			}

			// Token: 0x04001E32 RID: 7730
			[SerializeField]
			public Color color;
		}
	}
}
