using System;
using System.Collections;
using Rewired.UI.ControlMapper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000526 RID: 1318
	[AddComponentMenu("")]
	public class ControlMapperDemoMessage : MonoBehaviour
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0009C83C File Offset: 0x0009AC3C
		private void Awake()
		{
			if (this.controlMapper != null)
			{
				this.controlMapper.ScreenClosedEvent += this.OnControlMapperClosed;
				this.controlMapper.ScreenOpenedEvent += this.OnControlMapperOpened;
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0009C888 File Offset: 0x0009AC88
		private void Start()
		{
			this.SelectDefault();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0009C890 File Offset: 0x0009AC90
		private void OnControlMapperClosed()
		{
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.SelectDefaultDeferred());
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0009C8AB File Offset: 0x0009ACAB
		private void OnControlMapperOpened()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0009C8B9 File Offset: 0x0009ACB9
		private void SelectDefault()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (this.defaultSelectable != null)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultSelectable.gameObject);
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0009C8F4 File Offset: 0x0009ACF4
		private IEnumerator SelectDefaultDeferred()
		{
			yield return null;
			this.SelectDefault();
			yield break;
		}

		// Token: 0x04001C45 RID: 7237
		public ControlMapper controlMapper;

		// Token: 0x04001C46 RID: 7238
		public Selectable defaultSelectable;
	}
}
