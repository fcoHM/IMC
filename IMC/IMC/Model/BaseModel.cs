using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Model
{
    public abstract class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } // Assuming ID is an integer, adjust as necessary
        public override string ToString()
        {
            return $"ID: {ID}";
        }
    }
}
