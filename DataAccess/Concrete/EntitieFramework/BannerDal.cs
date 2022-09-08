using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitieFramework
{
    public class BannerDal : EfEntityRepositoryBase<Banner, AppDbContext>, IBannerDal
    {
        public int GetBannerCount()
        {
            using var _context = new AppDbContext();
            var bannerCount = _context.Banners.Count();
            return bannerCount;
        }
    }
}
