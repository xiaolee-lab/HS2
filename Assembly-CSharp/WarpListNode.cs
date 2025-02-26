using System;
using System.Runtime.CompilerServices;
using AIProject;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000FC7 RID: 4039
public class WarpListNode : MonoBehaviour
{
	// Token: 0x17001D45 RID: 7493
	// (get) Token: 0x06008636 RID: 34358 RVA: 0x00381FAB File Offset: 0x003803AB
	public Transform TargetPos
	{
		[CompilerGenerated]
		get
		{
			return this.targetPos;
		}
	}

	// Token: 0x06008637 RID: 34359 RVA: 0x00381FB3 File Offset: 0x003803B3
	public void Set(BasePoint _add, string _name)
	{
		this.Text.text = _name;
		this.targetPos = _add.WarpPoint;
		this.basePoint = _add;
	}

	// Token: 0x06008638 RID: 34360 RVA: 0x00381FD4 File Offset: 0x003803D4
	public void IconSet(Sprite sprite)
	{
		this.Icon.sprite = sprite;
		this.canWarp = true;
	}

	// Token: 0x04006D49 RID: 27977
	[SerializeField]
	private Image Icon;

	// Token: 0x04006D4A RID: 27978
	[SerializeField]
	private Text Text;

	// Token: 0x04006D4B RID: 27979
	private Transform targetPos;

	// Token: 0x04006D4C RID: 27980
	public bool canWarp;

	// Token: 0x04006D4D RID: 27981
	public BasePoint basePoint;
}
