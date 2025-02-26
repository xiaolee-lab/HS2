using System;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011F0 RID: 4592
	public static class AddObjectLight
	{
		// Token: 0x060096E2 RID: 38626 RVA: 0x003E66F0 File Offset: 0x003E4AF0
		public static OCILight Add(int _no)
		{
			int newIndex = Studio.GetNewIndex();
			Singleton<UndoRedoManager>.Instance.Do(new AddObjectCommand.AddLightCommand(_no, newIndex, Studio.optionSystem.initialPosition));
			return Studio.GetCtrlInfo(newIndex) as OCILight;
		}

		// Token: 0x060096E3 RID: 38627 RVA: 0x003E672C File Offset: 0x003E4B2C
		public static OCILight Load(OILightInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode)
		{
			ChangeAmount source = _info.changeAmount.Clone();
			OCILight result = AddObjectLight.Load(_info, _parent, _parentNode, false, -1);
			_info.changeAmount.Copy(source, true, true, true);
			return result;
		}

		// Token: 0x060096E4 RID: 38628 RVA: 0x003E6760 File Offset: 0x003E4B60
		public static OCILight Load(OILightInfo _info, ObjectCtrlInfo _parent, TreeNodeObject _parentNode, bool _addInfo, int _initialPosition)
		{
			OCILight ocilight = new OCILight();
			Info.LightLoadInfo lightLoadInfo = null;
			if (!Singleton<Info>.Instance.dicLightLoadInfo.TryGetValue(_info.no, out lightLoadInfo))
			{
				return null;
			}
			ocilight.objectInfo = _info;
			GameObject gameObject = Utility.LoadAsset<GameObject>(lightLoadInfo.bundlePath, lightLoadInfo.fileName, lightLoadInfo.manifest);
			gameObject.transform.SetParent(Singleton<Scene>.Instance.commonSpace.transform);
			ocilight.objectLight = gameObject;
			GuideObject guideObject = Singleton<GuideObjectManager>.Instance.Add(gameObject.transform, _info.dicKey);
			guideObject.scaleSelect = 0.1f;
			guideObject.scaleRot = 0.05f;
			guideObject.isActive = false;
			guideObject.enableScale = false;
			guideObject.SetVisibleCenter(true);
			ocilight.guideObject = guideObject;
			ocilight.lightColor = gameObject.GetComponent<LightColor>();
			if (ocilight.lightColor)
			{
				ocilight.lightColor.color = _info.color;
			}
			ocilight.lightTarget = lightLoadInfo.target;
			Info.LightLoadInfo.Target target = lightLoadInfo.target;
			if (target != Info.LightLoadInfo.Target.All)
			{
				if (target != Info.LightLoadInfo.Target.Chara)
				{
					if (target == Info.LightLoadInfo.Target.Map)
					{
						int num = ocilight.light.cullingMask;
						num ^= LayerMask.GetMask(new string[]
						{
							"Chara"
						});
						ocilight.light.cullingMask = num;
					}
				}
				else
				{
					int num2 = ocilight.light.cullingMask;
					num2 ^= LayerMask.GetMask(new string[]
					{
						"Map",
						"MapNoShadow"
					});
					ocilight.light.cullingMask = num2;
				}
			}
			if (_addInfo)
			{
				Studio.AddInfo(_info, ocilight);
			}
			else
			{
				Studio.AddObjectCtrlInfo(ocilight);
			}
			TreeNodeObject parent = (!(_parentNode != null)) ? ((_parent == null) ? null : _parent.treeNodeObject) : _parentNode;
			TreeNodeObject treeNodeObject = Studio.AddNode(lightLoadInfo.name, parent);
			treeNodeObject.enableAddChild = false;
			treeNodeObject.treeState = _info.treeState;
			guideObject.guideSelect.treeNodeObject = treeNodeObject;
			ocilight.treeNodeObject = treeNodeObject;
			if (_initialPosition == 1)
			{
				_info.changeAmount.pos = Singleton<Studio>.Instance.cameraCtrl.targetPos;
			}
			_info.changeAmount.OnChange();
			Studio.AddCtrlInfo(ocilight);
			if (_parent != null)
			{
				_parent.OnLoadAttach((!(_parentNode != null)) ? _parent.treeNodeObject : _parentNode, ocilight);
			}
			ocilight.Update();
			return ocilight;
		}
	}
}
