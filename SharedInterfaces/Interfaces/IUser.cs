using System.Collections.Generic;

namespace SharedInterfaces.Interfaces
{
    public interface IUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public IEnumerable<IIngredient> Cupboard { get; set; }
    }
}
