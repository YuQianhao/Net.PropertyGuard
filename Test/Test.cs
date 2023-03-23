using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyGuard.Attribute;

namespace Test
{
    public class Test
    {

        [PropertyTextLength(min:1,max: 100, message: "这个属性格式不正确。")]
        public string Name { get; set; } = "";

    }
}
