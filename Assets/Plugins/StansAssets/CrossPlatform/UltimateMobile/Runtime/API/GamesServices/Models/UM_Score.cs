using UnityEngine;
using System;
using StansAssets.Foundation;

namespace SA.CrossPlatform.GameServices
{
    /// <summary>
    /// An object containing information for a score that was earned by the player.
    /// </summary>
    [Serializable]
    public class UM_Score : UM_iScore
    {
        [SerializeField]
        long m_value;
        [SerializeField]
        long m_rank;
        [SerializeField]
        ulong m_context;
        [SerializeField]
        long m_date;

        public UM_Score(long value, long rank, ulong context, long date)
        {
            m_value = value;
            m_rank = rank;
            m_context = context;
            m_date = date;
        }

        /// <summary>
        /// The position of the score in the results of a leaderboard search.
        /// </summary>
        public long Rank => m_rank;

        /// <summary>
        /// The score earned by the player.
        /// </summary>
        public long Value => m_value;

        /// <summary>
        /// An unsigned long value used by your game.
        /// </summary>
        public ulong Context => m_context;

        /// <summary>
        /// The date and time when the score was earned.
        /// </summary>
        public DateTime Date => TimeUtility.FromUnixTime(m_date);

        /// <summary>
        /// The <see cref="Date"/> field value as unix time stamp
        /// </summary>
        public long DateUnix => m_date;
    }
}
