using Metrik.Shared.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metrik.Entities.Concrete
{
    public class User : EntityBase, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
