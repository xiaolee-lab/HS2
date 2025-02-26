using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000554 RID: 1364
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x06001CAB RID: 7339 RVA: 0x000AA66C File Offset: 0x000A8A6C
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		// Token: 0x04001DE1 RID: 7649
		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		// Token: 0x02000555 RID: 1365
		[Serializable]
		public class ElementInfo
		{
			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06001CAD RID: 7341 RVA: 0x000AA681 File Offset: 0x000A8A81
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06001CAE RID: 7342 RVA: 0x000AA689 File Offset: 0x000A8A89
			public Component component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x04001DE2 RID: 7650
			[SerializeField]
			private string _themeClass;

			// Token: 0x04001DE3 RID: 7651
			[SerializeField]
			private Component _component;
		}
	}
}
