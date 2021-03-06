﻿using NadekoBot.Services.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NadekoBot.Services.Database.Repositories.Impl
{
    public class WaifuRepository : Repository<WaifuInfo>, IWaifuRepository
    {
        public WaifuRepository(DbContext context) : base(context)
        {
        }

        public WaifuInfo ByWaifuUserId(ulong userId)
        {
            return _set.Include(wi => wi.Waifu)
                        .Include(wi => wi.Affinity)
                        .Include(wi => wi.Claimer)
                        .FirstOrDefault(wi => wi.Waifu.UserId == (long)userId);
        }

        public IList<WaifuInfo> ByClaimerUserId(ulong userId)
        {
            return _set.Include(wi => wi.Waifu)
                        .Include(wi => wi.Affinity)
                        .Include(wi => wi.Claimer)
                        .Where(wi => wi.Claimer != null && wi.Claimer.UserId == (long)userId)
                        .ToList();
        }

        public IList<WaifuInfo> GetTop(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (count == 0)
                return new List<WaifuInfo>();

            return _set.Include(wi => wi.Waifu)
                        .Include(wi => wi.Affinity)
                        .Include(wi => wi.Claimer)
                    .OrderByDescending(wi => wi.Price)
                    .Take(count)
                    .ToList();
        }
    }
}