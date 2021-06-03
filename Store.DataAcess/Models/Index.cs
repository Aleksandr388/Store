using System.Collections.Generic;

namespace Store.DataAcess.Models
{
    public class Index<T>
    {
        public IEnumerable<T> NavigationModels { get; set; }
        public Page PageModel { get; set; }
    }
}
