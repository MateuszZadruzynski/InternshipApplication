using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class KeeperAvatars
    {
        [Key]
        public int AvatarId { get; set; }
        public int KeeperId { get; set; }
        public string URL { get; set; }
        public virtual Keeper Keeper { get; set; }
    }
}
