
using Dal;
using Dal.Interface;
using Model;

namespace Bll
{
    public class SharedFileBll : BaseBll<SharedFileInfo>
    {
        public SharedFileBll(IBaseDal<SharedFileInfo> sharedfile) : base(sharedfile)
        { }

    }
}
