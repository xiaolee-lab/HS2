using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.Animal;
using UnityEngine;

namespace Housing
{
	// Token: 0x0200088C RID: 2188
	public class ObjectCtrl
	{
		// Token: 0x06003843 RID: 14403 RVA: 0x0014E45D File Offset: 0x0014C85D
		public ObjectCtrl(IObjectInfo _objectInfo, GameObject _gameObject, CraftInfo _craftInfo)
		{
			this.ObjectInfo = _objectInfo;
			this.GameObject = _gameObject;
			this.CraftInfo = _craftInfo;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x0014E47A File Offset: 0x0014C87A
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x0014E482 File Offset: 0x0014C882
		public IObjectInfo ObjectInfo { get; private set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x0014E48B File Offset: 0x0014C88B
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x0014E493 File Offset: 0x0014C893
		public GameObject GameObject { get; private set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x0014E49C File Offset: 0x0014C89C
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x0014E4A4 File Offset: 0x0014C8A4
		public CraftInfo CraftInfo { get; private set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x0014E4AD File Offset: 0x0014C8AD
		// (set) Token: 0x0600384B RID: 14411 RVA: 0x0014E4B5 File Offset: 0x0014C8B5
		public ObjectCtrl Parent { get; private set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x0014E4BE File Offset: 0x0014C8BE
		public virtual string Name
		{
			[CompilerGenerated]
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x0014E4C8 File Offset: 0x0014C8C8
		public Transform Transform
		{
			[CompilerGenerated]
			get
			{
				Transform result;
				if ((result = this.m_transform) == null)
				{
					GameObject gameObject = this.GameObject;
					result = (this.m_transform = ((gameObject != null) ? gameObject.transform : null));
				}
				return result;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x0014E500 File Offset: 0x0014C900
		public int InfoIndex
		{
			get
			{
				int result;
				if (this.Parent == null)
				{
					result = this.CraftInfo.ObjectInfos.IndexOf(this.ObjectInfo);
				}
				else
				{
					OIFolder oifolder = this.Parent.ObjectInfo as OIFolder;
					result = ((oifolder == null) ? -1 : oifolder.Child.IndexOf(this.ObjectInfo));
				}
				return result;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x0014E566 File Offset: 0x0014C966
		public int Kind
		{
			[CompilerGenerated]
			get
			{
				return this.ObjectInfo.Kind;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x0014E573 File Offset: 0x0014C973
		public virtual bool IsOverlapNow
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0014E578 File Offset: 0x0014C978
		public virtual void OnAttach(ObjectCtrl _parentCtrl, int _insert = -1)
		{
			if (this.Parent != null)
			{
				OIFolder oifolder = this.Parent.ObjectInfo as OIFolder;
				if (oifolder != null)
				{
					oifolder.Child.Remove(this.ObjectInfo);
				}
				OCFolder ocfolder = this.Parent as OCFolder;
				if (ocfolder != null)
				{
					ocfolder.Child.Remove(this.ObjectInfo);
				}
				this.Parent = null;
				Transform transform = this.Transform;
				if (transform != null)
				{
					transform.SetParent(this.CraftInfo.ObjRoot.transform, true);
				}
			}
			else
			{
				this.CraftInfo.ObjectInfos.Remove(this.ObjectInfo);
				this.CraftInfo.ObjectCtrls.Remove(this.ObjectInfo);
			}
			OCFolder ocfolder2 = _parentCtrl as OCFolder;
			if (ocfolder2 == null)
			{
				this.ListAdd(this.CraftInfo.ObjectInfos, this.ObjectInfo, _insert);
				if (!this.CraftInfo.ObjectCtrls.ContainsKey(this.ObjectInfo))
				{
					this.CraftInfo.ObjectCtrls.Add(this.ObjectInfo, this);
				}
				this.Parent = null;
				Transform transform2 = this.Transform;
				if (transform2 != null)
				{
					transform2.SetParent(this.CraftInfo.ObjRoot.transform, true);
				}
			}
			else
			{
				OIFolder oifolder2 = ocfolder2.OIFolder;
				this.ListAdd(oifolder2.Child, this.ObjectInfo, _insert);
				if (!ocfolder2.Child.ContainsKey(this.ObjectInfo))
				{
					ocfolder2.Child.Add(this.ObjectInfo, this);
				}
				this.Parent = ocfolder2;
				Transform transform3 = this.Transform;
				if (transform3 != null)
				{
					transform3.SetParent(this.Parent.Transform, true);
				}
			}
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x0014E734 File Offset: 0x0014CB34
		private void ListAdd(List<IObjectInfo> _lst, IObjectInfo _info, int _insert)
		{
			if (_insert != -1)
			{
				_lst.Remove(_info);
				if (MathfEx.RangeEqualOn<int>(0, _insert, _lst.Count - 1))
				{
					_lst.Insert(_insert, _info);
				}
				else
				{
					_lst.Add(_info);
				}
			}
			else if (!_lst.Contains(_info))
			{
				_lst.Add(_info);
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x0014E790 File Offset: 0x0014CB90
		public virtual bool OnRemoving()
		{
			return true;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x0014E793 File Offset: 0x0014CB93
		public virtual void OnDelete()
		{
			this.OnAttach(null, -1);
			this.CraftInfo.ObjectInfos.Remove(this.ObjectInfo);
			this.CraftInfo.ObjectCtrls.Remove(this.ObjectInfo);
			this.OnDestroy();
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0014E7D1 File Offset: 0x0014CBD1
		public virtual void OnDeleteChild()
		{
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0014E7D3 File Offset: 0x0014CBD3
		public virtual void OnDestroy()
		{
			UnityEngine.Object.DestroyImmediate(this.GameObject);
			this.GameObject = null;
			this.m_transform = null;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x0014E7EE File Offset: 0x0014CBEE
		public virtual void OnSelected()
		{
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x0014E7F0 File Offset: 0x0014CBF0
		public virtual void OnDeselected()
		{
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0014E7F2 File Offset: 0x0014CBF2
		public virtual void RestoreObject(GameObject _gameObject)
		{
			this.GameObject = _gameObject;
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x0014E7FB File Offset: 0x0014CBFB
		public virtual void CalcTransform()
		{
			this.Transform.localPosition = this.LocalPosition;
			this.Transform.localRotation = this.LocalRotation;
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x0014E81F File Offset: 0x0014CC1F
		// (set) Token: 0x0600385C RID: 14428 RVA: 0x0014E82C File Offset: 0x0014CC2C
		public virtual Vector3 LocalPosition
		{
			get
			{
				return this.ObjectInfo.Pos;
			}
			set
			{
				this.ObjectInfo.Pos = value;
				this.CalcTransform();
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x0014E840 File Offset: 0x0014CC40
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x0014E84D File Offset: 0x0014CC4D
		public virtual Vector3 LocalEulerAngles
		{
			get
			{
				return this.ObjectInfo.Rot;
			}
			set
			{
				this.ObjectInfo.Rot = value;
				this.CalcTransform();
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x0014E861 File Offset: 0x0014CC61
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x0014E86E File Offset: 0x0014CC6E
		public virtual Quaternion LocalRotation
		{
			get
			{
				return Quaternion.Euler(this.LocalEulerAngles);
			}
			set
			{
				this.LocalEulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x0014E87D File Offset: 0x0014CC7D
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x0014E88C File Offset: 0x0014CC8C
		public virtual Vector3 Position
		{
			get
			{
				return this.Transform.position;
			}
			set
			{
				this.Transform.position = value;
				this.ObjectInfo.Pos = this.Transform.localPosition;
				if (this.ObjectInfo.Pos.y < 0f)
				{
					this.LocalPosition = new Vector3(this.ObjectInfo.Pos.x, 0f, this.ObjectInfo.Pos.z);
				}
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x0014E90E File Offset: 0x0014CD0E
		// (set) Token: 0x06003864 RID: 14436 RVA: 0x0014E91B File Offset: 0x0014CD1B
		public virtual Vector3 EulerAngles
		{
			get
			{
				return this.Transform.eulerAngles;
			}
			set
			{
				this.Transform.eulerAngles = value;
				this.ObjectInfo.Rot = this.Transform.eulerAngles;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x0014E93F File Offset: 0x0014CD3F
		// (set) Token: 0x06003866 RID: 14438 RVA: 0x0014E94C File Offset: 0x0014CD4C
		public virtual Quaternion Rotation
		{
			get
			{
				return this.Transform.rotation;
			}
			set
			{
				this.EulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x0014E95B File Offset: 0x0014CD5B
		public virtual void GetLocalMinMax(Vector3 _pos, Quaternion _rot, Transform _root, ref Vector3 _min, ref Vector3 _max)
		{
			_min = Vector3.one;
			_max = Vector3.one;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x0014E975 File Offset: 0x0014CD75
		public virtual void GetActionPoint(ref List<ActionPoint> _points)
		{
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x0014E977 File Offset: 0x0014CD77
		public virtual void GetFarmPoint(ref List<FarmPoint> _points)
		{
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x0014E979 File Offset: 0x0014CD79
		public virtual void GetHPoint(ref List<HPoint> _points)
		{
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x0014E97B File Offset: 0x0014CD7B
		public virtual void GetColInfo(ref List<ItemComponent.ColInfo> _infos)
		{
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x0014E97D File Offset: 0x0014CD7D
		public virtual void GetPetHomePoint(ref List<PetHomePoint> _points)
		{
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x0014E97F File Offset: 0x0014CD7F
		public virtual void GetJukePoint(ref List<JukePoint> _points)
		{
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x0014E981 File Offset: 0x0014CD81
		public virtual void GetCraftPoint(ref List<CraftPoint> _points)
		{
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x0014E983 File Offset: 0x0014CD83
		public virtual void GetLightSwitchPoint(ref List<LightSwitchPoint> _points)
		{
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x0014E985 File Offset: 0x0014CD85
		public virtual void GetUsedNum(int _no, ref int _num)
		{
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x0014E987 File Offset: 0x0014CD87
		public virtual bool CheckOverlap(ObjectCtrl _oc, bool _load = false)
		{
			return false;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x0014E98A File Offset: 0x0014CD8A
		public virtual void BeforeCheckOverlap()
		{
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x0014E98C File Offset: 0x0014CD8C
		public virtual void AfterCheckOverlap()
		{
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x0014E98E File Offset: 0x0014CD8E
		public virtual void SetOverlapColliders(bool _flag)
		{
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x0014E990 File Offset: 0x0014CD90
		public virtual void GetOverlapObject(ref List<ObjectCtrl> _lst)
		{
		}

		// Token: 0x040038B0 RID: 14512
		private Transform m_transform;
	}
}
