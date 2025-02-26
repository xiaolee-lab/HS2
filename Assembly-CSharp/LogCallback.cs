using System;
using UnityEngine;

// Token: 0x02001171 RID: 4465
public class LogCallback : MonoBehaviour
{
	// Token: 0x0400770E RID: 30478
	[SerializeField]
	private bool isDraw = true;

	// Token: 0x0400770F RID: 30479
	[SerializeField]
	private bool isLeft = true;

	// Token: 0x04007710 RID: 30480
	[SerializeField]
	private bool isUp;

	// Token: 0x04007711 RID: 30481
	private bool isGuiArea = true;

	// Token: 0x04007712 RID: 30482
	private LogType level = LogType.Log;
}
