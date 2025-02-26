using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B05 RID: 2821
public class HAnimationInfoComponent : MonoBehaviour
{
	// Token: 0x0600529A RID: 21146 RVA: 0x0023F13C File Offset: 0x0023D53C
	private void Start()
	{
		this.toggle = base.gameObject.GetComponent<Toggle>();
		this.node = base.gameObject.GetComponent<ScrollCylinderNode>();
	}

	// Token: 0x0600529B RID: 21147 RVA: 0x0023F160 File Offset: 0x0023D560
	private void Update()
	{
		if (this.toggle != null && this.node.BG.transform.localScale != new Vector3(1f, 1f, 1f))
		{
			this.toggle.interactable = false;
		}
		else
		{
			this.toggle.interactable = true;
		}
	}

	// Token: 0x04004D05 RID: 19717
	public HScene.AnimationListInfo info;

	// Token: 0x04004D06 RID: 19718
	private Toggle toggle;

	// Token: 0x04004D07 RID: 19719
	private ScrollCylinderNode node;
}
