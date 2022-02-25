using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Core.Entities
{
    public class ShipEntity : BaseEntity<int>
    {
        public string Name { get; private set; }
        public double Length { get; private set; }
        public double Width { get; private set; }
        public string Code { get; private set; }

        public ShipEntity(string name, double length, double width, string code)
        {
            this.Name = name;
            this.Length = length;
            this.Width = width;
            this.Code = code;
        }

        public void Update(string name, double length, double width, string code)
        {
            this.Name = name;
            this.Length = length;
            this.Width = width;
            this.Code = code;
            this.Modified();

        }
    }
}
