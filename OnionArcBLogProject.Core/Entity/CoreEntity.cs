using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArcBLogProject.Core.Entity.Enum;

namespace OnionArcBLogProject.Core.Entity
{
    public class CoreEntity : IEntity<Guid>
    {
        public CoreEntity()
        {
            List<string> strings = new List<string>();
            strings.Add("BilgeAdam");

            strings.FirstOrDefault(x => x.StartsWith("Bi"));

            
        }
        public Guid Id { get; set; }

        public Status Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedComputerName { get; set; }

        public string CreatedIp { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedComputerName { get; set; }

        public string ModifiedIp { get; set; }

    }
}
