using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012C6 RID: 4806
	public class AutoLayoutCtrl : MonoBehaviour
	{
		// Token: 0x0600A062 RID: 41058 RVA: 0x0041EEAC File Offset: 0x0041D2AC
		private IEnumerator WaitFuncCoroutine()
		{
			yield return new WaitForEndOfFrame();
			AutoLayoutCtrl.Func func = this.func;
			if (func != AutoLayoutCtrl.Func.Disabled)
			{
				if (func == AutoLayoutCtrl.Func.Delete)
				{
					if (this.horizontalOrVerticalLayoutGroup)
					{
						UnityEngine.Object.Destroy(this.horizontalOrVerticalLayoutGroup);
					}
					if (this.contentSizeFitter)
					{
						UnityEngine.Object.Destroy(this.contentSizeFitter);
					}
				}
			}
			else
			{
				if (this.horizontalOrVerticalLayoutGroup)
				{
					this.horizontalOrVerticalLayoutGroup.enabled = false;
				}
				if (this.contentSizeFitter)
				{
					this.contentSizeFitter.enabled = false;
				}
			}
			UnityEngine.Object.Destroy(this);
			yield break;
		}

		// Token: 0x0600A063 RID: 41059 RVA: 0x0041EEC7 File Offset: 0x0041D2C7
		private void Start()
		{
			base.StartCoroutine(this.WaitFuncCoroutine());
		}

		// Token: 0x04007EB5 RID: 32437
		[SerializeField]
		private HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup;

		// Token: 0x04007EB6 RID: 32438
		[SerializeField]
		private ContentSizeFitter contentSizeFitter;

		// Token: 0x04007EB7 RID: 32439
		[SerializeField]
		private AutoLayoutCtrl.Func func = AutoLayoutCtrl.Func.Delete;

		// Token: 0x020012C7 RID: 4807
		private enum Func
		{
			// Token: 0x04007EB9 RID: 32441
			Disabled,
			// Token: 0x04007EBA RID: 32442
			Delete
		}
	}
}
