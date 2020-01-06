/*
 *	This program is the CONFIDENTIAL and PROPRIETARY property 
 *	of Tomasello Software LLC. Any unauthorized use, reproduction or
 *	transfer of this computer program is strictly prohibited.
 *
 *      Copyright (c) 2004 Tomasello Software LLC.
 *	This is an unpublished work, and is subject to limited distribution and
 *	restricted disclosure only. ALL RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights in
 * 	Technical Data and Computer Software clause at DFARS 252.227-7013.
 *
 *	Angel Island UO Shard	Version 1.0
 *			Release A
 *			March 25, 2004
 */

/* Scripts/Engines/Reward System/RewardList.cs
 * Created 5/23/04 by mith
 * ChangeLog
 */
using System;

namespace Server.Engines.RewardSystem
{
	public class RewardList
	{
		private RewardEntry[] m_Entries;

		public RewardEntry[] Entries { get { return m_Entries; } }

		public RewardList(int index, RewardEntry[] entries)
		{
			m_Entries = entries;

			for (int i = 0; i < entries.Length; ++i)
				entries[i].List = this;
		}
	}
}