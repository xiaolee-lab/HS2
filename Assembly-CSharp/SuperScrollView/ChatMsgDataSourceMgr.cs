using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005AC RID: 1452
	public class ChatMsgDataSourceMgr : MonoBehaviour
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x000B97E1 File Offset: 0x000B7BE1
		public static ChatMsgDataSourceMgr Get
		{
			get
			{
				if (ChatMsgDataSourceMgr.instance == null)
				{
					ChatMsgDataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<ChatMsgDataSourceMgr>();
				}
				return ChatMsgDataSourceMgr.instance;
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000B9802 File Offset: 0x000B7C02
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000B980C File Offset: 0x000B7C0C
		public PersonInfo GetPersonInfo(int personId)
		{
			PersonInfo result = null;
			if (this.mPersonInfoDict.TryGetValue(personId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000B9834 File Offset: 0x000B7C34
		public void Init()
		{
			this.mPersonInfoDict.Clear();
			PersonInfo personInfo = new PersonInfo();
			personInfo.mHeadIcon = "grid_pencil_128_g8";
			personInfo.mId = 0;
			personInfo.mName = "Jaci";
			this.mPersonInfoDict.Add(personInfo.mId, personInfo);
			personInfo = new PersonInfo();
			personInfo.mHeadIcon = "grid_pencil_128_g5";
			personInfo.mId = 1;
			personInfo.mName = "Toc";
			this.mPersonInfoDict.Add(personInfo.mId, personInfo);
			this.InitChatDataSource();
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000B98BC File Offset: 0x000B7CBC
		public ChatMsg GetChatMsgByIndex(int index)
		{
			if (index < 0 || index >= this.mChatMsgList.Count)
			{
				return null;
			}
			return this.mChatMsgList[index];
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000B98E4 File Offset: 0x000B7CE4
		public int TotalItemCount
		{
			get
			{
				return this.mChatMsgList.Count;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000B98F4 File Offset: 0x000B7CF4
		private void InitChatDataSource()
		{
			this.mChatMsgList.Clear();
			int num = ChatMsgDataSourceMgr.mChatDemoStrList.Length;
			int num2 = ChatMsgDataSourceMgr.mChatDemoPicList.Length;
			for (int i = 0; i < 100; i++)
			{
				ChatMsg chatMsg = new ChatMsg();
				chatMsg.mMsgType = (MsgTypeEnum)(UnityEngine.Random.Range(0, 99) % 2);
				chatMsg.mPersonId = UnityEngine.Random.Range(0, 99) % 2;
				chatMsg.mSrtMsg = ChatMsgDataSourceMgr.mChatDemoStrList[UnityEngine.Random.Range(0, 99) % num];
				chatMsg.mPicMsgSpriteName = ChatMsgDataSourceMgr.mChatDemoPicList[UnityEngine.Random.Range(0, 99) % num2];
				this.mChatMsgList.Add(chatMsg);
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000B9990 File Offset: 0x000B7D90
		public void AppendOneMsg()
		{
			int num = ChatMsgDataSourceMgr.mChatDemoStrList.Length;
			int num2 = ChatMsgDataSourceMgr.mChatDemoPicList.Length;
			ChatMsg chatMsg = new ChatMsg();
			chatMsg.mMsgType = (MsgTypeEnum)(UnityEngine.Random.Range(0, 99) % 2);
			chatMsg.mPersonId = UnityEngine.Random.Range(0, 99) % 2;
			chatMsg.mSrtMsg = ChatMsgDataSourceMgr.mChatDemoStrList[UnityEngine.Random.Range(0, 99) % num];
			chatMsg.mPicMsgSpriteName = ChatMsgDataSourceMgr.mChatDemoPicList[UnityEngine.Random.Range(0, 99) % num2];
			this.mChatMsgList.Add(chatMsg);
		}

		// Token: 0x0400212F RID: 8495
		private Dictionary<int, PersonInfo> mPersonInfoDict = new Dictionary<int, PersonInfo>();

		// Token: 0x04002130 RID: 8496
		private List<ChatMsg> mChatMsgList = new List<ChatMsg>();

		// Token: 0x04002131 RID: 8497
		private static ChatMsgDataSourceMgr instance = null;

		// Token: 0x04002132 RID: 8498
		private static string[] mChatDemoStrList = new string[]
		{
			"Support ListView and GridView.",
			"Support Infinity Vertical and Horizontal ScrollView.",
			"Support items in different sizes such as widths or heights. Support items with unknown size at init time.",
			"Support changing item count and item size at runtime. Support looping items such as spinners. Support item padding.",
			"Use only one C# script to help the UGUI ScrollRect to support any count items with high performance."
		};

		// Token: 0x04002133 RID: 8499
		private static string[] mChatDemoPicList = new string[]
		{
			"grid_pencil_128_g2",
			"grid_flower_200_3",
			"grid_pencil_128_g3",
			"grid_flower_200_7"
		};
	}
}
