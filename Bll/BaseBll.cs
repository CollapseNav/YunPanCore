using System;
using Dal.Interface;
using Model;
namespace Bll
{
    public class BaseBll<T> where T : BaseEntity
    {
        protected IBaseDal<T> req;

        public BaseBll(IBaseDal<T> re)
        {
            req = re;
        }
        public void SaveChange(){
            req.Save();
        }
    }
}
