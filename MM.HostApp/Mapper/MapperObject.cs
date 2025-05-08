using Entities.Models;
using MM.HostApp.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MM.HostApp.Mapper
{
    public static class MapperObject
    {
        public static ResponseJar GetResponseJarFromJar(Jar jar)
        {
            return new ResponseJar()
            {
                Id = jar.Id,
                Name = jar.Name,
                Description = jar.Description,
                Total = jar.Total!.Value
            };
        }
        public static List<ResponseJar> GetResponseJarsFromJars(List<Jar> jars)
        {
            List<ResponseJar> responseJars = new List<ResponseJar>();
            foreach (Jar jar in jars)
            {
                var responseJar = GetResponseJarFromJar(jar);
                responseJars.Add(responseJar);
            }
            return responseJars;
        }
        public static Jar GetJarFromRequestJar(RequestJar requestJar)
        {
            return new Jar()
            {
                Name = requestJar.Name,
                Description = requestJar.Description,
                Total = requestJar.Total,
                CustomerId = requestJar.CustomerId
            };
        }
    }
}
