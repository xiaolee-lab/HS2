using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B6 RID: 182
	public static class MovementUtility
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00019F74 File Offset: 0x00018374
		public static GameObject WithinSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance, LayerMask objectLayerMask, Vector3 targetOffset, LayerMask ignoreLayerMask, bool useTargetBone, HumanBodyBones targetBone)
		{
			GameObject result = null;
			Collider[] array = Physics.OverlapSphere(transform.position, viewDistance, objectLayerMask);
			if (array != null)
			{
				float num = float.PositiveInfinity;
				for (int i = 0; i < array.Length; i++)
				{
					float num2;
					GameObject gameObject;
					if ((gameObject = MovementUtility.WithinSight(transform, positionOffset, fieldOfViewAngle, viewDistance, array[i].gameObject, targetOffset, false, 0f, out num2, ignoreLayerMask, useTargetBone, targetBone)) != null && num2 < num)
					{
						num = num2;
						result = gameObject;
					}
				}
			}
			return result;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00019FFC File Offset: 0x000183FC
		public static GameObject WithinSight2D(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance, LayerMask objectLayerMask, Vector3 targetOffset, float angleOffset2D, LayerMask ignoreLayerMask)
		{
			GameObject result = null;
			Collider2D[] array = Physics2D.OverlapCircleAll(transform.position, viewDistance, objectLayerMask);
			if (array != null)
			{
				float num = float.PositiveInfinity;
				for (int i = 0; i < array.Length; i++)
				{
					float num2;
					GameObject gameObject;
					if ((gameObject = MovementUtility.WithinSight(transform, positionOffset, fieldOfViewAngle, viewDistance, array[i].gameObject, targetOffset, true, angleOffset2D, out num2, ignoreLayerMask, false, HumanBodyBones.Hips)) != null && num2 < num)
					{
						num = num2;
						result = gameObject;
					}
				}
			}
			return result;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001A084 File Offset: 0x00018484
		public static GameObject WithinSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance, GameObject targetObject, Vector3 targetOffset, LayerMask ignoreLayerMask, bool useTargetBone, HumanBodyBones targetBone)
		{
			float num;
			return MovementUtility.WithinSight(transform, positionOffset, fieldOfViewAngle, viewDistance, targetObject, targetOffset, false, 0f, out num, ignoreLayerMask, useTargetBone, targetBone);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001A0B4 File Offset: 0x000184B4
		public static GameObject WithinSight2D(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance, GameObject targetObject, Vector3 targetOffset, float angleOffset2D, LayerMask ignoreLayerMask, bool useTargetBone, HumanBodyBones targetBone)
		{
			float num;
			return MovementUtility.WithinSight(transform, positionOffset, fieldOfViewAngle, viewDistance, targetObject, targetOffset, true, angleOffset2D, out num, ignoreLayerMask, useTargetBone, targetBone);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001A0E0 File Offset: 0x000184E0
		public static GameObject WithinSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance, GameObject targetObject, Vector3 targetOffset, bool usePhysics2D, float angleOffset2D, out float angle, int ignoreLayerMask, bool useTargetBone, HumanBodyBones targetBone)
		{
			if (targetObject == null)
			{
				angle = 0f;
				return null;
			}
			Animator componentForType;
			if (useTargetBone && (componentForType = MovementUtility.GetComponentForType<Animator>(targetObject)) != null)
			{
				Transform boneTransform = componentForType.GetBoneTransform(targetBone);
				if (boneTransform != null)
				{
					targetObject = boneTransform.gameObject;
				}
			}
			Vector3 from = targetObject.transform.position - transform.TransformPoint(positionOffset);
			if (usePhysics2D)
			{
				Vector3 eulerAngles = transform.eulerAngles;
				eulerAngles.z -= angleOffset2D;
				angle = Vector3.Angle(from, Quaternion.Euler(eulerAngles) * Vector3.up);
				from.z = 0f;
			}
			else
			{
				angle = Vector3.Angle(from, transform.forward);
				from.y = 0f;
			}
			if (from.magnitude < viewDistance && angle < fieldOfViewAngle * 0.5f)
			{
				if (MovementUtility.LineOfSight(transform, positionOffset, targetObject, targetOffset, usePhysics2D, ignoreLayerMask) != null)
				{
					return targetObject;
				}
				if (MovementUtility.GetComponentForType<Collider>(targetObject) == null && MovementUtility.GetComponentForType<Collider2D>(targetObject) == null && targetObject.gameObject.activeSelf)
				{
					return targetObject;
				}
			}
			return null;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001A22C File Offset: 0x0001862C
		public static GameObject LineOfSight(Transform transform, Vector3 positionOffset, GameObject targetObject, Vector3 targetOffset, bool usePhysics2D, int ignoreLayerMask)
		{
			RaycastHit raycastHit;
			if (usePhysics2D)
			{
				RaycastHit2D raycastHit2D;
				if ((raycastHit2D = Physics2D.Linecast(transform.TransformPoint(positionOffset), targetObject.transform.TransformPoint(targetOffset), ~ignoreLayerMask)) && (raycastHit2D.transform.IsChildOf(targetObject.transform) || targetObject.transform.IsChildOf(raycastHit2D.transform)))
				{
					return targetObject;
				}
			}
			else if (Physics.Linecast(transform.TransformPoint(positionOffset), targetObject.transform.TransformPoint(targetOffset), out raycastHit, ~ignoreLayerMask) && (raycastHit.transform.IsChildOf(targetObject.transform) || targetObject.transform.IsChildOf(raycastHit.transform)))
			{
				return targetObject;
			}
			return null;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001A2FC File Offset: 0x000186FC
		public static GameObject WithinHearingRange(Transform transform, Vector3 positionOffset, float audibilityThreshold, float hearingRadius, LayerMask objectLayerMask)
		{
			GameObject result = null;
			Collider[] array = Physics.OverlapSphere(transform.TransformPoint(positionOffset), hearingRadius, objectLayerMask);
			if (array != null)
			{
				float num = 0f;
				for (int i = 0; i < array.Length; i++)
				{
					float num2 = 0f;
					GameObject gameObject;
					if ((gameObject = MovementUtility.WithinHearingRange(transform, positionOffset, audibilityThreshold, array[i].gameObject, ref num2)) != null && num2 > num)
					{
						num = num2;
						result = gameObject;
					}
				}
			}
			return result;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001A378 File Offset: 0x00018778
		public static GameObject WithinHearingRange2D(Transform transform, Vector3 positionOffset, float audibilityThreshold, float hearingRadius, LayerMask objectLayerMask)
		{
			GameObject result = null;
			Collider2D[] array = Physics2D.OverlapCircleAll(transform.TransformPoint(positionOffset), hearingRadius, objectLayerMask);
			if (array != null)
			{
				float num = 0f;
				for (int i = 0; i < array.Length; i++)
				{
					float num2 = 0f;
					GameObject gameObject;
					if ((gameObject = MovementUtility.WithinHearingRange(transform, positionOffset, audibilityThreshold, array[i].gameObject, ref num2)) != null && num2 > num)
					{
						num = num2;
						result = gameObject;
					}
				}
			}
			return result;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001A3F8 File Offset: 0x000187F8
		public static GameObject WithinHearingRange(Transform transform, Vector3 positionOffset, float audibilityThreshold, GameObject targetObject)
		{
			float num = 0f;
			return MovementUtility.WithinHearingRange(transform, positionOffset, audibilityThreshold, targetObject, ref num);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001A418 File Offset: 0x00018818
		public static GameObject WithinHearingRange(Transform transform, Vector3 positionOffset, float audibilityThreshold, GameObject targetObject, ref float audibility)
		{
			AudioSource[] componentsForType;
			if ((componentsForType = MovementUtility.GetComponentsForType<AudioSource>(targetObject)) != null)
			{
				for (int i = 0; i < componentsForType.Length; i++)
				{
					if (componentsForType[i].isPlaying)
					{
						float num = Vector3.Distance(transform.position, targetObject.transform.position);
						if (componentsForType[i].rolloffMode == AudioRolloffMode.Logarithmic)
						{
							audibility = componentsForType[i].volume / Mathf.Max(componentsForType[i].minDistance, num - componentsForType[i].minDistance);
						}
						else
						{
							audibility = componentsForType[i].volume * Mathf.Clamp01((num - componentsForType[i].minDistance) / (componentsForType[i].maxDistance - componentsForType[i].minDistance));
						}
						if (audibility > audibilityThreshold)
						{
							return targetObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001A4D8 File Offset: 0x000188D8
		public static void DrawLineOfSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float angleOffset, float viewDistance, bool usePhysics2D)
		{
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001A4DC File Offset: 0x000188DC
		public static T GetComponentForType<T>(GameObject target) where T : Component
		{
			Dictionary<Type, Component> dictionary;
			Component component;
			if (MovementUtility.gameObjectComponentMap.TryGetValue(target, out dictionary))
			{
				if (dictionary.TryGetValue(typeof(T), out component))
				{
					return component as T;
				}
			}
			else
			{
				dictionary = new Dictionary<Type, Component>();
				MovementUtility.gameObjectComponentMap.Add(target, dictionary);
			}
			component = target.GetComponent<T>();
			dictionary.Add(typeof(T), component);
			return component as T;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001A560 File Offset: 0x00018960
		public static T[] GetComponentsForType<T>(GameObject target) where T : Component
		{
			Dictionary<Type, Component[]> dictionary;
			Component[] components;
			if (MovementUtility.gameObjectComponentsMap.TryGetValue(target, out dictionary))
			{
				if (dictionary.TryGetValue(typeof(T), out components))
				{
					return components as T[];
				}
			}
			else
			{
				dictionary = new Dictionary<Type, Component[]>();
				MovementUtility.gameObjectComponentsMap.Add(target, dictionary);
			}
			components = target.GetComponents<T>();
			dictionary.Add(typeof(T), components);
			return components as T[];
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001A5D2 File Offset: 0x000189D2
		public static void ClearCache()
		{
			MovementUtility.gameObjectComponentMap.Clear();
			MovementUtility.gameObjectComponentsMap.Clear();
		}

		// Token: 0x0400036E RID: 878
		private static Dictionary<GameObject, Dictionary<Type, Component>> gameObjectComponentMap = new Dictionary<GameObject, Dictionary<Type, Component>>();

		// Token: 0x0400036F RID: 879
		private static Dictionary<GameObject, Dictionary<Type, Component[]>> gameObjectComponentsMap = new Dictionary<GameObject, Dictionary<Type, Component[]>>();
	}
}
