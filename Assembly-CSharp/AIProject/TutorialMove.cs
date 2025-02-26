using System;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CF1 RID: 3313
	[TaskCategory("")]
	public class TutorialMove : AgentTutorialMoveAction
	{
		// Token: 0x06006ABF RID: 27327 RVA: 0x002D9608 File Offset: 0x002D7A08
		public override void OnStart()
		{
			base.Agent.EventKey = EventType.Move;
			ActionPoint component = base.Agent.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<ActionPoint>();
			base.Agent.TargetInSightActionPoint = component;
			this._actionMotion = new PoseKeyPair
			{
				postureID = -1,
				poseID = -1
			};
			if (Singleton<Resources>.IsInstance() && component != null)
			{
				AgentProfile.TutorialSetting tutorial = Singleton<Resources>.Instance.AgentProfile.Tutorial;
				if (0 <= component.ID)
				{
					int id = component.ID;
					if (tutorial.GoGhroughActionIDList.Contains(id))
					{
						PoseKeyPair actionMotion;
						if (Singleton<Resources>.Instance.AgentProfile.DicGoGhroughAnimID.TryGetValue(base.Agent.ChaControl.fileParam.personality, out actionMotion))
						{
							this._actionMotion = actionMotion;
						}
						else
						{
							this._actionMotion = tutorial.GoGhroughAnimID;
						}
					}
					else if (tutorial.ThreeStepJumpActionIDList.Contains(id))
					{
						PoseKeyPair actionMotion2;
						if (Singleton<Resources>.Instance.AgentProfile.DicThreeStepJumpAnimID.TryGetValue(base.Agent.ChaControl.fileParam.personality, out actionMotion2))
						{
							this._actionMotion = actionMotion2;
						}
						else
						{
							this._actionMotion = tutorial.ThreeStepJumpAnimID;
						}
					}
				}
				else if (!component.IDList.IsNullOrEmpty<int>())
				{
					foreach (int value in component.IDList)
					{
						if (tutorial.GoGhroughActionIDList.Contains(value))
						{
							PoseKeyPair actionMotion3;
							if (Singleton<Resources>.Instance.AgentProfile.DicGoGhroughAnimID.TryGetValue(base.Agent.ChaControl.fileParam.personality, out actionMotion3))
							{
								this._actionMotion = actionMotion3;
							}
							else
							{
								this._actionMotion = tutorial.GoGhroughAnimID;
							}
						}
						else if (tutorial.ThreeStepJumpActionIDList.Contains(value))
						{
							PoseKeyPair actionMotion4;
							if (Singleton<Resources>.Instance.AgentProfile.DicThreeStepJumpAnimID.TryGetValue(base.Agent.ChaControl.fileParam.personality, out actionMotion4))
							{
								this._actionMotion = actionMotion4;
							}
							else
							{
								this._actionMotion = tutorial.ThreeStepJumpAnimID;
							}
						}
					}
				}
			}
			base.OnStart();
		}
	}
}
