using System;
using System.Runtime.CompilerServices;

namespace Housing
{
	// Token: 0x020008C9 RID: 2249
	public class Selection : Singleton<Selection>
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x00158009 File Offset: 0x00156409
		public ObjectCtrl SelectObject
		{
			[CompilerGenerated]
			get
			{
				return this.SelectObjects.SafeGet(0);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x00158017 File Offset: 0x00156417
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x0015801F File Offset: 0x0015641F
		public ObjectCtrl[] SelectObjects { get; private set; }

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00158028 File Offset: 0x00156428
		public void SetSelectObjects(ObjectCtrl[] _objectCtrls)
		{
			this.SelectObjects = _objectCtrls;
			if (this.onSelectFunc != null)
			{
				this.onSelectFunc(this.SelectObjects);
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x0015804F File Offset: 0x0015644F
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
		}

		// Token: 0x04003A05 RID: 14853
		public Action<ObjectCtrl[]> onSelectFunc;
	}
}
