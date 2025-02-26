using System;

namespace Rewired
{
	// Token: 0x0200057B RID: 1403
	public interface IFlightPedalsTemplate : IControllerTemplate
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001EDC RID: 7900
		IControllerTemplateAxis leftPedal { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001EDD RID: 7901
		IControllerTemplateAxis rightPedal { get; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001EDE RID: 7902
		IControllerTemplateAxis slide { get; }
	}
}
