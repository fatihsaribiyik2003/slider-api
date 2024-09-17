using SubuProtokol.Models;
using X.PagedList;

namespace SubuProtokol.WEBUI.Models
{
    public class ProtokolUserModel
    {
        public IPagedList<ProtokolQuery> model1 { get; set; }
        public  ApiUserModel model2 { get; set; }
        
    }
}
