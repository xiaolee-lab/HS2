using System;

namespace Rewired
{
	// Token: 0x02000581 RID: 1409
	public sealed class FlightPedalsTemplate : ControllerTemplate, IFlightPedalsTemplate, IControllerTemplate
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x000AF8C0 File Offset: 0x000ADCC0
		public FlightPedalsTemplate(object payload) : base(payload)
		{
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000AF8C9 File Offset: 0x000ADCC9
		IControllerTemplateAxis IFlightPedalsTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(0);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x000AF8D2 File Offset: 0x000ADCD2
		IControllerTemplateAxis IFlightPedalsTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(1);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000AF8DB File Offset: 0x000ADCDB
		IControllerTemplateAxis IFlightPedalsTemplate.slide
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(2);
			}
		}

		// Token: 0x04001FD9 RID: 8153
		public static readonly Guid typeGuid = new Guid("f6fe76f8-be2a-4db2-b853-9e3652075913");

		// Token: 0x04001FDA RID: 8154
		public const int elementId_leftPedal = 0;

		// Token: 0x04001FDB RID: 8155
		public const int elementId_rightPedal = 1;

		// Token: 0x04001FDC RID: 8156
		public const int elementId_slide = 2;
	}
}
