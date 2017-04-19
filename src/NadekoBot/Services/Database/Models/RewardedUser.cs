﻿using System;

namespace NadekoBot.Services.Database.Models
{
    public class RewardedUser : DbEntity
    {
        public long UserId { get; set; }
        public int AmountRewardedThisMonth { get; set; }
        public DateTime LastReward { get; set; }
    }
}
