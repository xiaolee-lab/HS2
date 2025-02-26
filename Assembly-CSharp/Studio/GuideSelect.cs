using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio
{
	// Token: 0x02001255 RID: 4693
	public class GuideSelect : GuideBase, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x17002109 RID: 8457
		// (set) Token: 0x06009ACF RID: 39631 RVA: 0x003F8518 File Offset: 0x003F6918
		public Color color
		{
			set
			{
				this.colorNormal = base.ConvertColor(value);
				this.colorHighlighted = new Color(value.r, value.g, value.b, 1f);
				base.colorNow = this.colorNormal;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06009AD0 RID: 39632 RVA: 0x003F8558 File Offset: 0x003F6958
		// (set) Token: 0x06009AD1 RID: 39633 RVA: 0x003F8560 File Offset: 0x003F6960
		public TreeNodeObject treeNodeObject { get; set; }

		// Token: 0x06009AD2 RID: 39634 RVA: 0x003F8569 File Offset: 0x003F6969
		public void OnPointerClick(PointerEventData _eventData)
		{
			if (this.treeNodeObject != null)
			{
				this.treeNodeObject.Select(false);
			}
			else
			{
				if (!Singleton<GuideObjectManager>.IsInstance())
				{
					return;
				}
				Singleton<GuideObjectManager>.Instance.selectObject = base.guideObject;
			}
		}

		// Token: 0x06009AD3 RID: 39635 RVA: 0x003F85A8 File Offset: 0x003F69A8
		private void Awake()
		{
			base.Start();
			this.treeNodeObject = null;
		}

		// Token: 0x06009AD4 RID: 39636 RVA: 0x003F85B7 File Offset: 0x003F69B7
		public override void Start()
		{
			base.colorNow = this.colorNormal;
		}
	}
}
