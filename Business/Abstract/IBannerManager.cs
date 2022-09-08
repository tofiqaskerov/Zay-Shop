using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBannerManager
    {
        Banner GetFirst();
        Banner Get(int id);
        int GetBannerCount();
        List<Banner> GetAll();
        void Add(Banner banner);
        void Update (Banner banner);    
        void Delete(Banner banner);    
    }
}
