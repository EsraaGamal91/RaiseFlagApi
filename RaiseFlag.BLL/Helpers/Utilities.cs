using RaiseFlag.DAL.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.ServiceLayer.Helpers
{
   public class Utilities : IUtilities
    {
        public double GetDistance(double currentLng, double currentLat, double userLng, double userLat)
        {
            var sCoord = new GeoCoordinate(currentLng, currentLat);
            var eCoord = new GeoCoordinate(userLng, userLat);

            return sCoord.GetDistanceTo(eCoord);
        }


        public bool IsOutOfRange(List<User> users, List<UsersLocation> usersLocation, long userId,long groupId, double maxDistance= 50.0)
        {
            double diffDistance;
            var userLocation = usersLocation.FirstOrDefault(x => x.ID == userId)?? new UsersLocation();
            foreach (var item in users.Where(x=>x.ID != userId))
            {
                var currLocation = usersLocation.FirstOrDefault(x => x.ID == item.ID) ?? new UsersLocation();


                var lng = userLocation.Longitude.HasValue?userLocation.Longitude.Value : 0;
                var lat = userLocation.Latitude.HasValue ? userLocation.Latitude.Value : 0;
                var currLng = currLocation.Longitude.HasValue ? currLocation.Longitude.Value : 0;
                var currLat = currLocation.Latitude.HasValue ? currLocation.Latitude.Value : 0;
                diffDistance = GetDistance(currLng,currLat,lng,lat);
                if (diffDistance > maxDistance)
                    return true;
            }
            return false;
        }
    }

    public interface IUtilities
    {
        double GetDistance(double currentLng, double currentLat, double userLng, double userLat);
        bool IsOutOfRange(List<User> users,List<UsersLocation> usersLocation, long userId, long groupId, double maxDistance = 50.0);
    }
}
