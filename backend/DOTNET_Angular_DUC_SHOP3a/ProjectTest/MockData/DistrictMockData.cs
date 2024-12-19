
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.MockData
{
    public class DistrictMockData
    {
        public static List<ProvinceCity> listProvinceCity = ProvinceCityMockData.GetProvinceCities();
        public static List<District> GetDistricts()
        {
            return new List<District>
            {
                new District { Id =1,Name ="Ba Dinh" ,ProvinceCity= listProvinceCity[0] },
                new District { Id =2,Name ="Cau Giay" ,ProvinceCity= listProvinceCity[0] },
                new District { Id =3,Name ="Hoan Kiem" ,ProvinceCity= listProvinceCity[0] },
                new District { Id =4,Name ="Go Vap" ,ProvinceCity= listProvinceCity[1] },
                new District { Id =5,Name ="Phu Nhuan" ,ProvinceCity= listProvinceCity[1] },
                new District { Id =6,Name ="Tan Binh" ,ProvinceCity= listProvinceCity[1] },
                new District { Id =7,Name ="Do Son" ,ProvinceCity= listProvinceCity[2] },
                new District { Id =8,Name ="Hong Bang" ,ProvinceCity= listProvinceCity[2] },
                new District { Id =9,Name ="Le Chan" ,ProvinceCity= listProvinceCity[2] },
                new District { Id =10,Name ="Ngo Quyen" ,ProvinceCity= listProvinceCity[2] },
            };
        }

        public static DistrictAddUpdateDTO AddDistrict()
        {
            return new DistrictAddUpdateDTO { Id = 0, Name = "Dong Da", ProvinceCityId = 1 };
        }

        public static DistrictAddUpdateDTO UpdateDistrict()
        {
            return new DistrictAddUpdateDTO { Id = 10, Name = "NGO QUYEN", ProvinceCityId = 3 };
        }
    }
}
