using System;
using System.Collections.Generic;
using AIProject.Animal;
using UnityEngine;

namespace AIProject.DebugUtil
{
	// Token: 0x02000E1C RID: 3612
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("YK/Debug/MarkerOutput")]
	public class MarkerOutput : MonoBehaviour
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06007020 RID: 28704 RVA: 0x002FEDD3 File Offset: 0x002FD1D3
		// (set) Token: 0x06007021 RID: 28705 RVA: 0x002FEDDB File Offset: 0x002FD1DB
		public Transform CameraTransform { get; set; }

		// Token: 0x06007022 RID: 28706 RVA: 0x002FEDE4 File Offset: 0x002FD1E4
		private void Awake()
		{
			this._canvas = base.GetComponent<Canvas>();
			if (this._canvas == null || this._markerPrefab == null)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x002FEE1B File Offset: 0x002FD21B
		private void Start()
		{
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x002FEE20 File Offset: 0x002FD220
		private void OnUpdate()
		{
			if (this.CameraTransform == null)
			{
				return;
			}
			int num = AgentMarker.AgentMarkerTable.Count - this._markers.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._markerPrefab);
					gameObject.transform.SetParent(this._canvas.transform, false);
					this._markers.Add(gameObject);
				}
			}
			else if (num < 0)
			{
				for (int j = 0; j < this._markers.Count; j++)
				{
					this._markers[j].SetActive(j < this._markers.Count - num);
				}
			}
			int num2 = 0;
			foreach (int key in AgentMarker.Keys)
			{
				AgentActor agentActor;
				if (AgentMarker.AgentMarkerTable.TryGetValue(key, out agentActor))
				{
					Vector2 v = RectTransformUtility.WorldToScreenPoint(Camera.main, agentActor.Position);
					GameObject gameObject2 = this._markers[num2++];
					gameObject2.transform.position = v;
					Quaternion a = Quaternion.LookRotation(agentActor.Position - this.CameraTransform.position);
					float num3 = Quaternion.Angle(a, this.CameraTransform.rotation);
					gameObject2.SetActive(num3 < 60f);
				}
			}
			this.OnUpdateAnimalMarker();
			this.OnUpdateMerchantMarker();
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x002FEFD4 File Offset: 0x002FD3D4
		private void OnUpdateAnimalMarker()
		{
			if (this.CameraTransform == null)
			{
				return;
			}
			int num = AnimalMarker.AnimalMarkerTable.Count - this._animalMarkers.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._markerPrefab);
					gameObject.transform.SetParent(this._canvas.transform, false);
					this._animalMarkers.Add(gameObject);
				}
			}
			else if (num < 0)
			{
				for (int j = 0; j < this._animalMarkers.Count; j++)
				{
					this._animalMarkers[j].SetActive(j < this._animalMarkers.Count + num);
				}
			}
			int num2 = 0;
			foreach (int key in AnimalMarker.Keys)
			{
				AnimalBase animalBase;
				if (AnimalMarker.AnimalMarkerTable.TryGetValue(key, out animalBase))
				{
					Vector2 v = RectTransformUtility.WorldToScreenPoint(Camera.main, animalBase.Position);
					GameObject gameObject2 = this._animalMarkers[num2++];
					gameObject2.transform.position = v;
					Quaternion a = Quaternion.LookRotation(animalBase.Position - this.CameraTransform.position);
					float num3 = Quaternion.Angle(a, this.CameraTransform.rotation);
					gameObject2.SetActive(num3 < 60f);
				}
			}
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x002FF17C File Offset: 0x002FD57C
		private void OnUpdateMerchantMarker()
		{
			if (this.CameraTransform == null)
			{
				return;
			}
			int num = MerchantMarker.MerchantMarkerTable.Count - this._merchantMarkers.Count;
			if (0 < num)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._markerPrefab);
					gameObject.transform.SetParent(this._canvas.transform, false);
					this._merchantMarkers.Add(gameObject);
				}
			}
			else if (num < 0)
			{
				for (int j = 0; j < this._merchantMarkers.Count; j++)
				{
					this._merchantMarkers[j].SetActive(j < this._merchantMarkers.Count + num);
				}
			}
			int num2 = 0;
			foreach (int key in MerchantMarker.Keys)
			{
				MerchantActor merchantActor = null;
				if (MerchantMarker.MerchantMarkerTable.TryGetValue(key, out merchantActor))
				{
					Vector2 v = RectTransformUtility.WorldToScreenPoint(Camera.main, merchantActor.Position);
					GameObject gameObject2 = this._merchantMarkers[num2++];
					gameObject2.transform.position = v;
					Quaternion a = Quaternion.LookRotation(merchantActor.Position - this.CameraTransform.position);
					float num3 = Quaternion.Angle(a, this.CameraTransform.rotation);
					gameObject2.SetActive(num3 < 60f);
				}
			}
		}

		// Token: 0x04005C40 RID: 23616
		private Canvas _canvas;

		// Token: 0x04005C41 RID: 23617
		[SerializeField]
		private GameObject _markerPrefab;

		// Token: 0x04005C42 RID: 23618
		private List<GameObject> _markers = new List<GameObject>();

		// Token: 0x04005C44 RID: 23620
		private List<GameObject> _animalMarkers = new List<GameObject>();

		// Token: 0x04005C45 RID: 23621
		private List<GameObject> _merchantMarkers = new List<GameObject>();
	}
}
