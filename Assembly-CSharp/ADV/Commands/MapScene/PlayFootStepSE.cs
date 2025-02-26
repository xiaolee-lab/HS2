using System;
using AIProject;
using AIProject.Definitions;
using Illusion;
using Manager;
using UnityEngine;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000750 RID: 1872
	public class PlayFootStepSE : CommandBase
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000FE493 File Offset: 0x000FC893
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"sex",
					"bareFoot",
					"seType",
					"weather",
					"areaType",
					"Pos"
				};
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x000FE4CC File Offset: 0x000FC8CC
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					bool.FalseString,
					AIProject.Definitions.Map.FootStepSE.Sand.ToString(),
					Weather.Clear.ToString(),
					SoundPack.PlayAreaType.Normal.ToString(),
					string.Empty
				};
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000FE530 File Offset: 0x000FC930
		public override void Do()
		{
			base.Do();
			int num = 0;
			int num2 = int.Parse(this.args[num++]);
			bool bareFoot = bool.Parse(this.args[num++]);
			AIProject.Definitions.Map.FootStepSE seType = (AIProject.Definitions.Map.FootStepSE)PlayFootStepSE.Get<AIProject.Definitions.Map.FootStepSE>(this.args[num++]);
			Weather weather = (Weather)PlayFootStepSE.Get<Weather>(this.args[num++]);
			SoundPack.PlayAreaType areaType = (SoundPack.PlayAreaType)PlayFootStepSE.Get<SoundPack.PlayAreaType>(this.args[num++]);
			string posStr = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				posStr = s;
			});
			AudioSource audioSource = Singleton<Manager.Resources>.Instance.SoundPack.PlayFootStep((byte)num2, bareFoot, seType, weather, areaType);
			if (audioSource != null)
			{
				Vector3 position;
				if (base.scenario.commandController.V3Dic.TryGetValue(posStr, out position))
				{
					audioSource.transform.position = position;
				}
				else if (base.scenario.AdvCamera != null)
				{
					audioSource.transform.position = base.scenario.AdvCamera.transform.position;
				}
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000FE664 File Offset: 0x000FCA64
		private static int Get<T>(string name) where T : struct
		{
			int result;
			if (!int.TryParse(name, out result))
			{
				result = Illusion.Utils.Enum<T>.FindIndex(name, true);
			}
			return result;
		}
	}
}
