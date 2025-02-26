using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BEF RID: 3055
	public class CameraTable : SerializedMonoBehaviour
	{
		// Token: 0x170011D7 RID: 4567
		public CinemachineVirtualCameraBase this[ShotType key]
		{
			get
			{
				CinemachineVirtualCameraBase result;
				if (this._table.TryGetValue(key, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06005D64 RID: 23908 RVA: 0x00277463 File Offset: 0x00275863
		public ShotType[] Keys
		{
			get
			{
				return this._table.Keys.ToArray<ShotType>();
			}
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x00277478 File Offset: 0x00275878
		public Transform Duplicate()
		{
			Vector3 position = base.transform.position;
			Quaternion rotation = base.transform.rotation;
			return UnityEngine.Object.Instantiate<GameObject>(base.gameObject, position, rotation).transform;
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x002774AF File Offset: 0x002758AF
		public void DestroySelf()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x002774BC File Offset: 0x002758BC
		public Dictionary<ShotType, CinemachineVirtualCameraBase> ToDictionary()
		{
			return this._table.ToDictionary((KeyValuePair<ShotType, CinemachineVirtualCameraBase> x) => x.Key, (KeyValuePair<ShotType, CinemachineVirtualCameraBase> x) => x.Value);
		}

		// Token: 0x040053AD RID: 21421
		[SerializeField]
		private Dictionary<ShotType, CinemachineVirtualCameraBase> _table = new Dictionary<ShotType, CinemachineVirtualCameraBase>();
	}
}
