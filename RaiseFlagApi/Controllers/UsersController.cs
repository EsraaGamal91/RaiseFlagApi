using RaiseFlag.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RaiseFlag.BLL.ViewModels;
using RaiseFlagApi.Models;
using RaiseFlag.DAL.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using RaiseFlagApi.Helpers;
using System.Text;
namespace RaiseFlagApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly Lazy<IUsersFunction> _usersFunction;
        public UsersController(Lazy<IUsersFunction> usersFunction)
        {

            _usersFunction = usersFunction;
        }

        private IUsersFunction UsersFunction => _usersFunction.Value;
        static int count = 2;
        static int outId = 0;
        //json currentUsersLocations;
        [HttpPost]
        public object PostLocation(PostLocationViewModel model)
        {
            var result = UsersFunction.PostUserLocation(model);
            return result;
        }
        [Route("api/getAll/{groupId}")]
        public object GetAllUsers(long groupId)
        {
            var result = UsersFunction.GetAllUsers(groupId);
            return result;
        }


        [Route("api/getAllJson/{start}")]
        public object GetAllJson(bool start = false)
        {

            string filePathBase = @"C:\Users\Esraa\Documents\Visual Studio 2015\Projects\RaiseFlagApi\RaiseFlagApi\Models\Users.json";
            string filePath = @"C:\Users\Esraa\Documents\Visual Studio 2015\Projects\RaiseFlagApi\RaiseFlagApi\Models\CurrentUsers.json";
            var usersReport = GetUsersFromJson(start ? filePathBase : filePath);
            var Results = ChangeUsersLocations(usersReport);
            return Results;

            
        }

        public static List<UserReport> ChangeUsersLocations(List<UserReport> usersReport)
        {
            int[] arrOutOfRange = { 8, 11, 12, 13 };
            var stepInKilometer = 0.01;
            //usersReport.Select(x => x.outOfRange == false);

            foreach (var item in usersReport)
            {
                var stepsLng = (Math.PI / 360) * stepInKilometer * Math.Cos(Convert.ToDouble(item.longitude));
                var stepsLat = (2 * Math.PI / 360) * stepInKilometer;

                item.longitude = (Convert.ToDouble(item.longitude) + stepsLng).ToString();
                item.latitude = (Convert.ToDouble(item.latitude) + stepsLat).ToString();
                item.outOfRange = false;
            }
            //usersReport.Where(h => h.outOfRange = true).

            if (count == 1)
            {
                if (outId <= 3)
                usersReport.FirstOrDefault(x => x.id == arrOutOfRange[outId]).outOfRange = true;
            }
            if (count > 4) count = 0;
            if (outId > 3) outId = 0;
            count++;
            outId++;
            ReplaceJsonValue(JsonConvert.SerializeObject(usersReport));
            return usersReport;
        }

        public static List<UserReport> GetUsersFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);

            var deserializedProduct = JsonConvert.DeserializeObject<List<UserReport>>(json);
            var result = deserializedProduct;           
            return result;
        }
        public static StringBuilder ParseJson(string extract)
        {
            var settings = new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            using (var jsonWriter = new DateLiteralJsonTextWriter(writer) { Formatting = Formatting.Indented })
            {
                JsonSerializer.CreateDefault(settings).Serialize(jsonWriter, JsonConvert.DeserializeObject(extract, settings));
            }
            return sb;
        }
        public static void ReplaceJsonValue(string result)
        {
            string filepath = @"C:\Users\Esraa\Documents\Visual Studio 2015\Projects\RaiseFlagApi\RaiseFlagApi\Models\CurrentUsers.json";
            File.WriteAllText(filepath, result);
            
        }
    }
}
