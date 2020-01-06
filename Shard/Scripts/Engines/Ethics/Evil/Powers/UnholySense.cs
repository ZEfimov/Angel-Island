
/* Scripts\Engines\Ethics\Evil\Powers\UnholySense.cs
 * ChangeLog
 *  5/22/11, Adam
 *		- add a spam trap.
 *		Since thie power is 0 cost and since it enumerates all opposition, we want to curb spam loading the server
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Ethics.Evil
{
	public sealed class UnholySense : Power
	{
		private DateTime lastUsed = DateTime.MinValue;

		public UnholySense()
		{
			m_Definition = new PowerDefinition(
					0,
					2,
					"Unholy Sense",
					"Drewrok Erstok",	// powerwords swapped in RunUO with ("Drewrok Velgo")
					""
				);
		}

		public override void BeginInvoke(Player from)
		{
			// spam trap
			if (DateTime.Now - lastUsed < TimeSpan.FromSeconds(.7))
				return;

			lastUsed = DateTime.Now;

			Ethic opposition = Ethic.Hero;

			int enemyCount = 0;

			int maxRange = 18 + from.Power;

			Player primary = null;

			foreach (Player pl in opposition.Players)
			{
				Mobile mob = pl.Mobile;

				if (mob == null || mob.Map != from.Mobile.Map || !mob.Alive)
					continue;

				if (!mob.InRange(from.Mobile, Math.Max(18, maxRange - pl.Power)))
					continue;

				if (primary == null || pl.Power > primary.Power)
					primary = pl;

				++enemyCount;
			}

			// different message for old ethics system when there are no enemies
			// we are not sure of the message when there are enemies, so we will go with the later ethics messages
			if (Core.OldEthics)
				if (enemyCount == 0)
				{
					from.Mobile.LocalOverheadMessage(Server.Network.MessageType.Regular, 0x59, false, "There are no enemies afoot.");
					FinishInvoke(from);
					return;
				}

			StringBuilder sb = new StringBuilder();

			sb.Append("You sense ");
			sb.Append(enemyCount == 0 ? "no" : enemyCount.ToString());
			sb.Append(enemyCount == 1 ? " enemy" : " enemies");

			if (primary != null)
			{
				sb.Append(", and a strong presense");

				switch (from.Mobile.GetDirectionTo(primary.Mobile))
				{
					case Direction.West:
						sb.Append(" to the west.");
						break;
					case Direction.East:
						sb.Append(" to the east.");
						break;
					case Direction.North:
						sb.Append(" to the north.");
						break;
					case Direction.South:
						sb.Append(" to the south.");
						break;

					case Direction.Up:
						sb.Append(" to the north-west.");
						break;
					case Direction.Down:
						sb.Append(" to the south-east.");
						break;
					case Direction.Left:
						sb.Append(" to the south-west.");
						break;
					case Direction.Right:
						sb.Append(" to the north-east.");
						break;
				}
			}
			else
			{
				sb.Append('.');
			}

			from.Mobile.LocalOverheadMessage(Server.Network.MessageType.Regular, 0x59, false, sb.ToString());

			FinishInvoke(from);
		}
	}
}