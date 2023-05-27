using Magic_API.Models.Dto;

namespace Magic_API.Data
{
    public static class VillaStore
    {

        public static   List<VillaDto>villaList= new List<VillaDto>
        {
            new VillaDto{ Id=1,Nombre="Jarabacoa", Ocupantes=3, MetrosCuadrados=50},
            new VillaDto{ Id=2,Nombre="Monte plata", Ocupantes=4, MetrosCuadrados=100},
            new VillaDto{ Id=2,Nombre="Azua",Ocupantes=2, MetrosCuadrados=30}
        };
    }
}
