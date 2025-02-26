using System;
using AIProject.Definitions;
using Manager;

namespace AIProject
{
	// Token: 0x02000C52 RID: 3154
	public class AgentTimer
	{
		// Token: 0x060064FC RID: 25852 RVA: 0x002AFC2E File Offset: 0x002AE02E
		public AgentTimer(AgentActor source)
		{
			this._instance = source;
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x002AFC3D File Offset: 0x002AE03D
		public void Update(TimeSpan delta)
		{
			if (this._instance == null)
			{
				return;
			}
			this._instance.AgentData.ElapseAssignedDuration(delta);
			this._instance.AgentData.ElapseADVEventTimeCount(delta);
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x002AFC74 File Offset: 0x002AE074
		public bool CheckPeriodEventADV()
		{
			if (!Game.isAdd50)
			{
				return false;
			}
			if (this._instance.AttitudeID != 0)
			{
				return false;
			}
			int item = 6;
			return !this._instance.AgentData.advEventLimitation.Contains(item) && this._instance.AgentData.SickState.ID == -1 && !this._instance.IsBadMood() && this._instance.GetFlavorSkill(FlavorSkill.Type.Reliability) >= Singleton<Resources>.Instance.StatusProfile.RandomADVReliabilityCond && this._instance.AgentData.IsOverADVEventTimeCount();
		}

		// Token: 0x040057C3 RID: 22467
		private AgentActor _instance;
	}
}
