using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000B96 RID: 2966
	public class AnimalSearchActor : MonoBehaviour
	{
		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005866 RID: 22630 RVA: 0x0025FB09 File Offset: 0x0025DF09
		private PlayerActor Player
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005867 RID: 22631 RVA: 0x0025FB25 File Offset: 0x0025DF25
		private ReadOnlyDictionary<int, AgentActor> AgentActors
		{
			[CompilerGenerated]
			get
			{
				Map instance = Singleton<Map>.Instance;
				return (instance != null) ? instance.AgentTable : null;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x0025FB3B File Offset: 0x0025DF3B
		private List<Actor> Actors
		{
			[CompilerGenerated]
			get
			{
				Map instance = Singleton<Map>.Instance;
				return (instance != null) ? instance.Actors : null;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005869 RID: 22633 RVA: 0x0025FB51 File Offset: 0x0025DF51
		// (set) Token: 0x0600586A RID: 22634 RVA: 0x0025FB59 File Offset: 0x0025DF59
		public List<PlayerActor> VisiblePlayerActors { get; private set; } = new List<PlayerActor>();

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600586B RID: 22635 RVA: 0x0025FB62 File Offset: 0x0025DF62
		// (set) Token: 0x0600586C RID: 22636 RVA: 0x0025FB6A File Offset: 0x0025DF6A
		public List<AgentActor> VisibleAgentActors { get; private set; } = new List<AgentActor>();

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600586D RID: 22637 RVA: 0x0025FB73 File Offset: 0x0025DF73
		// (set) Token: 0x0600586E RID: 22638 RVA: 0x0025FB7B File Offset: 0x0025DF7B
		public List<Actor> SearchActors { get; private set; } = new List<Actor>();

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600586F RID: 22639 RVA: 0x0025FB84 File Offset: 0x0025DF84
		// (set) Token: 0x06005870 RID: 22640 RVA: 0x0025FB8C File Offset: 0x0025DF8C
		public List<Actor> VisibleActors { get; private set; } = new List<Actor>();

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x0025FB95 File Offset: 0x0025DF95
		public Vector3 CenterOffset
		{
			[CompilerGenerated]
			get
			{
				return this.centerOffset;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x0025FB9D File Offset: 0x0025DF9D
		public float NearSearchRadius
		{
			[CompilerGenerated]
			get
			{
				return this.nearSearchRadius;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06005873 RID: 22643 RVA: 0x0025FBA5 File Offset: 0x0025DFA5
		public float FarSearchRadius
		{
			[CompilerGenerated]
			get
			{
				return this.farSearchRadius;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0025FBAD File Offset: 0x0025DFAD
		// (set) Token: 0x06005875 RID: 22645 RVA: 0x0025FBB5 File Offset: 0x0025DFB5
		public bool SearchEnabled { get; private set; } = true;

		// Token: 0x06005876 RID: 22646 RVA: 0x0025FBC0 File Offset: 0x0025DFC0
		private void Start()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			where this.SearchEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0025FC14 File Offset: 0x0025E014
		public void SetSearchEnabled(bool _enabled, bool _clearCollision = false)
		{
			if (_enabled == this.SearchEnabled)
			{
				return;
			}
			this.SearchEnabled = _enabled;
			if (!this.SearchEnabled && _clearCollision)
			{
				this.VisiblePlayerActors.Clear();
				this.VisibleAgentActors.Clear();
				this.VisibleActors.Clear();
				this.nearCollisionStateTable.Clear();
				this.farCollisionStateTable.Clear();
			}
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0025FC80 File Offset: 0x0025E080
		private void SetCollisionState(Dictionary<int, UnityEx.ValueTuple<CollisionState, Actor>> _collisionStateTable, int _key, CollisionState _state, Actor _actor)
		{
			UnityEx.ValueTuple<CollisionState, Actor> value;
			if (_collisionStateTable.TryGetValue(_key, out value))
			{
				value.Item1 = _state;
				value.Item2 = _actor;
				_collisionStateTable[_key] = value;
			}
			else
			{
				_collisionStateTable[_key] = new UnityEx.ValueTuple<CollisionState, Actor>(_state, _actor);
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x0025FCC8 File Offset: 0x0025E0C8
		public Vector3 SearchPoint
		{
			get
			{
				return (!this.animal) ? Vector3.zero : (this.animal.Position + this.animal.Rotation * this.centerOffset);
			}
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x0025FD18 File Offset: 0x0025E118
		private void OnUpdate()
		{
			Transform transform = this.animal.transform;
			foreach (Actor actor in this.SearchActors)
			{
				if (!(actor == null))
				{
					int instanceID = actor.InstanceID;
					float num = Vector3.Distance(actor.Position, this.SearchPoint);
					UnityEx.ValueTuple<CollisionState, Actor> valueTuple;
					if (!this.farCollisionStateTable.TryGetValue(instanceID, out valueTuple))
					{
						UnityEx.ValueTuple<CollisionState, Actor> valueTuple2 = new UnityEx.ValueTuple<CollisionState, Actor>(CollisionState.None, actor);
						this.farCollisionStateTable[instanceID] = valueTuple2;
						valueTuple = valueTuple2;
					}
					if (num <= this.FarSearchRadius)
					{
						switch (valueTuple.Item1)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.SetCollisionState(this.farCollisionStateTable, instanceID, CollisionState.Enter, actor);
							if (actor is PlayerActor)
							{
								this.OnFarEnter(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnFarEnter(actor as AgentActor);
							}
							else
							{
								this.OnFarEnter(actor);
							}
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(this.farCollisionStateTable, instanceID, CollisionState.Stay, actor);
							if (actor is PlayerActor)
							{
								this.OnFarStay(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnFarStay(actor as AgentActor);
							}
							else
							{
								this.OnFarStay(actor);
							}
							break;
						}
					}
					else
					{
						switch (valueTuple.Item1)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.SetCollisionState(this.farCollisionStateTable, instanceID, CollisionState.None, actor);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(this.farCollisionStateTable, instanceID, CollisionState.Exit, actor);
							if (actor is PlayerActor)
							{
								this.OnFarExit(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnFarExit(actor as AgentActor);
							}
							else
							{
								this.OnFarExit(actor);
							}
							break;
						}
					}
					UnityEx.ValueTuple<CollisionState, Actor> valueTuple3;
					if (!this.nearCollisionStateTable.TryGetValue(instanceID, out valueTuple3))
					{
						UnityEx.ValueTuple<CollisionState, Actor> valueTuple2 = new UnityEx.ValueTuple<CollisionState, Actor>(CollisionState.None, actor);
						this.nearCollisionStateTable[instanceID] = valueTuple2;
						valueTuple3 = valueTuple2;
					}
					if (num <= this.NearSearchRadius)
					{
						switch (valueTuple3.Item1)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.SetCollisionState(this.nearCollisionStateTable, instanceID, CollisionState.Enter, actor);
							if (actor is PlayerActor)
							{
								this.OnNearEnter(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnNearEnter(actor as AgentActor);
							}
							else
							{
								this.OnNearEnter(actor);
							}
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(this.nearCollisionStateTable, instanceID, CollisionState.Stay, actor);
							if (actor is PlayerActor)
							{
								this.OnNearStay(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnNearStay(actor as AgentActor);
							}
							else
							{
								this.OnNearStay(actor);
							}
							break;
						}
					}
					else
					{
						switch (valueTuple3.Item1)
						{
						case CollisionState.None:
						case CollisionState.Exit:
							this.SetCollisionState(this.nearCollisionStateTable, instanceID, CollisionState.None, actor);
							break;
						case CollisionState.Enter:
						case CollisionState.Stay:
							this.SetCollisionState(this.nearCollisionStateTable, instanceID, CollisionState.Exit, actor);
							if (actor is PlayerActor)
							{
								this.OnNearExit(actor as PlayerActor);
							}
							else if (actor is AgentActor)
							{
								this.OnNearExit(actor as AgentActor);
							}
							else
							{
								this.OnNearExit(actor);
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x002600E0 File Offset: 0x0025E4E0
		private void OnNearEnter(PlayerActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearPlayerActorEnterEvent != null)
			{
				this.OnNearPlayerActorEnterEvent(_actor);
			}
			this.OnNearEnter(_actor);
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x0026010F File Offset: 0x0025E50F
		private void OnNearStay(PlayerActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearPlayerActorStayEvent != null)
			{
				this.OnNearPlayerActorStayEvent(_actor);
			}
			this.OnNearStay(_actor);
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x0026013E File Offset: 0x0025E53E
		private void OnNearExit(PlayerActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearPlayerActorExitEvent != null)
			{
				this.OnNearPlayerActorExitEvent(_actor);
			}
			this.OnNearExit(_actor);
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x0026016D File Offset: 0x0025E56D
		private void OnNearEnter(AgentActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearAgentActorEnterEvent != null)
			{
				this.OnNearAgentActorEnterEvent(_actor);
			}
			this.OnNearEnter(_actor);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0026019C File Offset: 0x0025E59C
		private void OnNearStay(AgentActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearAgentActorStayEvent != null)
			{
				this.OnNearAgentActorStayEvent(_actor);
			}
			this.OnNearStay(_actor);
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x002601CB File Offset: 0x0025E5CB
		private void OnNearExit(AgentActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearAgentActorExitEvent != null)
			{
				this.OnNearAgentActorExitEvent(_actor);
			}
			this.OnNearExit(_actor);
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x002601FA File Offset: 0x0025E5FA
		private void OnNearEnter(Actor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearActorEnterEvent != null)
			{
				this.OnNearActorEnterEvent(_actor);
			}
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x00260222 File Offset: 0x0025E622
		private void OnNearStay(Actor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearActorStayEvent != null)
			{
				this.OnNearActorStayEvent(_actor);
			}
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x0026024A File Offset: 0x0025E64A
		private void OnNearExit(Actor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnNearActorExitEvent != null)
			{
				this.OnNearActorExitEvent(_actor);
			}
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x00260274 File Offset: 0x0025E674
		private void OnFarEnter(PlayerActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (!this.VisiblePlayerActors.Contains(_actor))
			{
				this.VisiblePlayerActors.Add(_actor);
			}
			if (this.OnFarPlayerActorEnterEvent != null)
			{
				this.OnFarPlayerActorEnterEvent(_actor);
			}
			this.OnFarEnter(_actor);
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x002602CB File Offset: 0x0025E6CB
		private void OnFarStay(PlayerActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnFarPlayerActorStayEvent != null)
			{
				this.OnFarPlayerActorStayEvent(_actor);
			}
			this.OnFarStay(_actor);
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x002602FC File Offset: 0x0025E6FC
		private void OnFarExit(PlayerActor _actor)
		{
			if (_actor == null)
			{
				this.VisiblePlayerActors.RemoveAll((PlayerActor x) => x == null);
				return;
			}
			this.VisiblePlayerActors.Remove(_actor);
			if (this.OnFarPlayerActorExitEvent != null)
			{
				this.OnFarPlayerActorExitEvent(_actor);
			}
			this.OnFarExit(_actor);
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x0026036C File Offset: 0x0025E76C
		private void OnFarEnter(AgentActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (!this.VisibleAgentActors.Contains(_actor))
			{
				this.VisibleAgentActors.Add(_actor);
			}
			if (this.OnFarAgentActorEnterEvent != null)
			{
				this.OnFarAgentActorEnterEvent(_actor);
			}
			this.OnFarEnter(_actor);
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x002603C3 File Offset: 0x0025E7C3
		private void OnFarStay(AgentActor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnFarAgentActorStayEvent != null)
			{
				this.OnFarAgentActorStayEvent(_actor);
			}
			this.OnFarStay(_actor);
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x002603F4 File Offset: 0x0025E7F4
		private void OnFarExit(AgentActor _actor)
		{
			if (_actor == null)
			{
				this.VisibleAgentActors.RemoveAll((AgentActor x) => x == null);
				return;
			}
			this.VisibleAgentActors.Remove(_actor);
			if (this.OnFarAgentActorExitEvent != null)
			{
				this.OnFarAgentActorExitEvent(_actor);
			}
			this.OnFarExit(_actor);
		}

		// Token: 0x0600588A RID: 22666 RVA: 0x00260464 File Offset: 0x0025E864
		private void OnFarEnter(Actor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (!this.VisibleActors.Contains(_actor))
			{
				this.VisibleActors.Add(_actor);
			}
			if (this.OnFarActorEnterEvent != null)
			{
				this.OnFarActorEnterEvent(_actor);
			}
		}

		// Token: 0x0600588B RID: 22667 RVA: 0x002604B4 File Offset: 0x0025E8B4
		private void OnFarStay(Actor _actor)
		{
			if (_actor == null)
			{
				return;
			}
			if (this.OnFarActorStayEvent != null)
			{
				this.OnFarActorStayEvent(_actor);
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x002604DC File Offset: 0x0025E8DC
		private void OnFarExit(Actor _actor)
		{
			if (_actor == null)
			{
				this.VisibleActors.RemoveAll((Actor x) => x == null);
				return;
			}
			this.VisibleActors.Remove(_actor);
			if (this.OnFarActorExitEvent != null)
			{
				this.OnFarActorExitEvent(_actor);
			}
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x00260548 File Offset: 0x0025E948
		public bool OnSearchArea(Vector3 _checkPoint)
		{
			float num = Vector3.Distance(_checkPoint, this.SearchPoint);
			return num <= this.farSearchRadius;
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x00260570 File Offset: 0x0025E970
		public bool CheckPlayerOnNearSearchArea()
		{
			PlayerActor player = this.Player;
			if (!player)
			{
				return false;
			}
			int instanceID = player.InstanceID;
			UnityEx.ValueTuple<CollisionState, Actor> valueTuple;
			if (!this.nearCollisionStateTable.TryGetValue(instanceID, out valueTuple))
			{
				return false;
			}
			CollisionState item = valueTuple.Item1;
			return item == CollisionState.Enter || item == CollisionState.Stay;
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x002605CC File Offset: 0x0025E9CC
		public bool CheckPlayerOnFarSearchArea()
		{
			PlayerActor player = this.Player;
			if (!player)
			{
				return false;
			}
			int instanceID = player.InstanceID;
			UnityEx.ValueTuple<CollisionState, Actor> valueTuple;
			if (!this.farCollisionStateTable.TryGetValue(instanceID, out valueTuple))
			{
				return false;
			}
			CollisionState item = valueTuple.Item1;
			return item == CollisionState.Enter || item == CollisionState.Stay;
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x00260628 File Offset: 0x0025EA28
		public void RefreshSearchActorTable()
		{
			this.SearchActors.Clear();
			if (this.Player != null)
			{
				this.SearchActors.Add(this.Player);
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this.AgentActors)
			{
				if (keyValuePair.Value != null)
				{
					this.SearchActors.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x002606CC File Offset: 0x0025EACC
		public void ClearCollisionState()
		{
			this.nearCollisionStateTable.Clear();
			this.farCollisionStateTable.Clear();
			this.VisiblePlayerActors.Clear();
			this.VisibleAgentActors.Clear();
			this.VisibleActors.Clear();
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00260708 File Offset: 0x0025EB08
		public void ClearCollisionStateWithExit()
		{
			while (!this.VisiblePlayerActors.IsNullOrEmpty<PlayerActor>())
			{
				this.OnFarExit(this.VisiblePlayerActors[0]);
			}
			while (!this.VisibleAgentActors.IsNullOrEmpty<AgentActor>())
			{
				this.OnFarExit(this.VisibleAgentActors[0]);
			}
			while (!this.VisibleActors.IsNullOrEmpty<Actor>())
			{
				this.OnFarExit(this.VisibleActors[0]);
			}
			this.ClearCollisionState();
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x00260790 File Offset: 0x0025EB90
		private void OnDestroy()
		{
			this.OnNearPlayerActorEnterEvent = null;
			this.OnNearPlayerActorStayEvent = null;
			this.OnNearPlayerActorExitEvent = null;
			this.OnNearAgentActorEnterEvent = null;
			this.OnNearAgentActorStayEvent = null;
			this.OnNearAgentActorExitEvent = null;
			this.OnNearActorEnterEvent = null;
			this.OnNearActorStayEvent = null;
			this.OnNearActorExitEvent = null;
			this.OnFarPlayerActorEnterEvent = null;
			this.OnFarPlayerActorStayEvent = null;
			this.OnFarPlayerActorExitEvent = null;
			this.OnFarAgentActorEnterEvent = null;
			this.OnFarAgentActorStayEvent = null;
			this.OnFarAgentActorExitEvent = null;
			this.OnFarActorEnterEvent = null;
			this.OnFarActorStayEvent = null;
			this.OnFarActorExitEvent = null;
		}

		// Token: 0x04005112 RID: 20754
		[SerializeField]
		private AnimalBase animal;

		// Token: 0x04005113 RID: 20755
		private Dictionary<int, UnityEx.ValueTuple<CollisionState, Actor>> nearCollisionStateTable = new Dictionary<int, UnityEx.ValueTuple<CollisionState, Actor>>();

		// Token: 0x04005114 RID: 20756
		private Dictionary<int, UnityEx.ValueTuple<CollisionState, Actor>> farCollisionStateTable = new Dictionary<int, UnityEx.ValueTuple<CollisionState, Actor>>();

		// Token: 0x04005119 RID: 20761
		[SerializeField]
		private Vector3 centerOffset = Vector3.zero;

		// Token: 0x0400511A RID: 20762
		[SerializeField]
		private float nearSearchRadius = 25f;

		// Token: 0x0400511B RID: 20763
		[SerializeField]
		private float farSearchRadius = 50f;

		// Token: 0x0400511C RID: 20764
		[NonSerialized]
		public Action<PlayerActor> OnNearPlayerActorEnterEvent;

		// Token: 0x0400511D RID: 20765
		[NonSerialized]
		public Action<PlayerActor> OnNearPlayerActorStayEvent;

		// Token: 0x0400511E RID: 20766
		[NonSerialized]
		public Action<PlayerActor> OnNearPlayerActorExitEvent;

		// Token: 0x0400511F RID: 20767
		[NonSerialized]
		public Action<AgentActor> OnNearAgentActorEnterEvent;

		// Token: 0x04005120 RID: 20768
		[NonSerialized]
		public Action<AgentActor> OnNearAgentActorStayEvent;

		// Token: 0x04005121 RID: 20769
		[NonSerialized]
		public Action<AgentActor> OnNearAgentActorExitEvent;

		// Token: 0x04005122 RID: 20770
		[NonSerialized]
		public Action<Actor> OnNearActorEnterEvent;

		// Token: 0x04005123 RID: 20771
		[NonSerialized]
		public Action<Actor> OnNearActorStayEvent;

		// Token: 0x04005124 RID: 20772
		[NonSerialized]
		public Action<Actor> OnNearActorExitEvent;

		// Token: 0x04005125 RID: 20773
		[NonSerialized]
		public Action<PlayerActor> OnFarPlayerActorEnterEvent;

		// Token: 0x04005126 RID: 20774
		[NonSerialized]
		public Action<PlayerActor> OnFarPlayerActorStayEvent;

		// Token: 0x04005127 RID: 20775
		[NonSerialized]
		public Action<PlayerActor> OnFarPlayerActorExitEvent;

		// Token: 0x04005128 RID: 20776
		[NonSerialized]
		public Action<AgentActor> OnFarAgentActorEnterEvent;

		// Token: 0x04005129 RID: 20777
		[NonSerialized]
		public Action<AgentActor> OnFarAgentActorStayEvent;

		// Token: 0x0400512A RID: 20778
		[NonSerialized]
		public Action<AgentActor> OnFarAgentActorExitEvent;

		// Token: 0x0400512B RID: 20779
		[NonSerialized]
		public Action<Actor> OnFarActorEnterEvent;

		// Token: 0x0400512C RID: 20780
		[NonSerialized]
		public Action<Actor> OnFarActorStayEvent;

		// Token: 0x0400512D RID: 20781
		[NonSerialized]
		public Action<Actor> OnFarActorExitEvent;
	}
}
