using System;
using System.Collections.Generic;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x02000708 RID: 1800
	public class Vector : CommandBase
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000F99EA File Offset: 0x000F7DEA
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Variable",
					"Type",
					"X",
					"Y",
					"Z"
				};
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000F9A1A File Offset: 0x000F7E1A
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000F9A4C File Offset: 0x000F7E4C
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string a = this.args[num++];
			Vector3 vector = Vector3.zero;
			Dictionary<string, Vector3> v3Dic = base.scenario.commandController.V3Dic;
			if (a == "=")
			{
				if (!v3Dic.TryGetValue(this.args[num], out vector))
				{
					for (int i = 0; i < 3; i++)
					{
						int num2 = i + num;
						float value;
						if (!this.args.SafeGet(num2).IsNullOrEmpty() && float.TryParse(this.args[num2], out value))
						{
							vector[i] = value;
						}
					}
				}
			}
			else if (a == "+=")
			{
				if (v3Dic.TryGetValue(key, out vector))
				{
					float num3;
					if (float.TryParse(this.args[num], out num3))
					{
						for (int j = 0; j < 3; j++)
						{
							int num4 = j + num;
							if (!this.args.SafeGet(num4).IsNullOrEmpty() && float.TryParse(this.args[num4], out num3))
							{
								ref Vector3 ptr = ref vector;
								int index;
								vector[index = j] = ptr[index] + num3;
							}
						}
					}
					else
					{
						vector += v3Dic[this.args[num++]];
					}
				}
			}
			else if (a == "-=" && v3Dic.TryGetValue(key, out vector))
			{
				float num5;
				if (float.TryParse(this.args[num], out num5))
				{
					for (int k = 0; k < 3; k++)
					{
						int num6 = k + num;
						if (!this.args.SafeGet(num6).IsNullOrEmpty() && float.TryParse(this.args[num6], out num5))
						{
							ref Vector3 ptr = ref vector;
							int index2;
							vector[index2 = k] = ptr[index2] - num5;
						}
					}
				}
				else
				{
					vector -= v3Dic[this.args[num++]];
				}
			}
			base.scenario.commandController.V3Dic[key] = vector;
		}
	}
}
