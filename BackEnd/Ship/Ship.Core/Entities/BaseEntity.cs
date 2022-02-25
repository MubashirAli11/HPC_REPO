using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; private set; }


        public BaseEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public void MarkDeleted()
        {
            this.IsDeleted = true;
            this.ModifiedOn = DateTime.UtcNow;
        }

        public void Modified()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }
    }
}
