using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.MockData
{
    public class ProvinceCityMockData
    {
        public static List<ProvinceCity> GetProvinceCities()
        {
            return new List<ProvinceCity>
            {
                new ProvinceCity { Id = 1, Name = "Ha Noi" },
                new ProvinceCity { Id = 2, Name = "Sai Gon" },
                new ProvinceCity { Id = 3, Name = "Hai Phong" }
            };
        }

        public static ProvinceCity AddProvinceCity()
        {
            return new ProvinceCity { Id = 0, Name = "Bac Ninh" };
        }

        public static ProvinceCity UpdateProvinceCity()
        {
            return new ProvinceCity { Id = 3, Name = "Phu Tho" };
        }
    }
}
