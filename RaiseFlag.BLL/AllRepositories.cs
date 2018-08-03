using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RaiseFlag.BLL.ViewModels;
using RaiseFlag.DAL.Models;
using RaiseFlag.ServiceLayer.Helpers;

namespace RaiseFlag.BLL
{
    public class UsersRepository : IUsersRepository
    {
        IDbConnection dbSql = new SqlConnection(ConfigurationManager.ConnectionStrings["RaiseFlagDapper"].ConnectionString);
        RaiseFlagEntities _db = new RaiseFlagEntities();

        private readonly Lazy<IUtilities> _utilities;
        public UsersRepository(Lazy<IUtilities> utilities)
        {

            _utilities = utilities;
        }

        private IUtilities Utilities => _utilities.Value;
        public bool IsUserNameExist(string userName)
        {
            string sqlString = @"SELECT ID FROM dbo.Users WHERE UserName =@userName";
            var result = dbSql.Query<int>(sqlString, new
            {
                @userName = userName
            }).ToList();
            if (result.Count > 0)
                return true;
            return false;
        }

        public List<UserReport> PostUserLocation(PostLocationViewModel model)
        {
            var users = GetAllGroupUsers(model.groupId);
            var userLocation = new UsersLocation();
            userLocation.UserID = model.userId;
            userLocation.GroupID = model.groupId;
            userLocation.Longitude = Convert.ToDouble(model.longitude);
            userLocation.Latitude = Convert.ToDouble(model.latitude);
            _db.UsersLocations.Add(userLocation);
            _db.SaveChanges();

            return users;

        }

        public List<UserReport> GetAllGroupUsers(long groupId)
        {
            var users = _db.Users.Where(x => x.UsersGroups.All(g => g.GroupID == groupId)).ToList();
            //var maxDistance = _db.Groups.FirstOrDefault(x => x.ID == groupId).DiffDistance;
            var maxDistance = users.FirstOrDefault().UsersGroups.FirstOrDefault(g => g.GroupID == groupId).Group.DiffDistance;

            var result = new List<UserReport>();
            var usersLocations = _db.UsersLocations.Where(x => x.GroupID == groupId).ToList();
            foreach (var item in users)
            {
                var location = usersLocations.FirstOrDefault(z => z.UserID == item.ID)??new UsersLocation();
                var userReport = new UserReport();
                userReport.id = item.ID;
                userReport.groupId = groupId;
                userReport.fullName = item.FullName ?? "";
                userReport.mobile = item.Mobile ?? "";
                userReport.longitude = location.Longitude.HasValue ? location.Longitude.Value.ToString() : "";
                userReport.latitude = location.Latitude.HasValue ? location.Latitude.Value.ToString() : "";
                userReport.outOfRange = Utilities.IsOutOfRange(users,usersLocations,item.ID, groupId, maxDistance);
                userReport.active = item.UsersGroups.FirstOrDefault().Status;
                userReport.tracked = item.UsersGroups.FirstOrDefault().Tracked;
                userReport.notify = item.UsersGroups.FirstOrDefault().Notified;
                result.Add(userReport);
            }

            /*result = users.Select(u => new UserReport
            {
                id = u.ID,
                groupId = groupId,
                fullName = u.FullName??"",
                mobile = u.Mobile??"",
                longitude = u.UsersLocations.FirstOrDefault(l => l.GroupID == groupId).Longitude.HasValue?
                u.UsersLocations.FirstOrDefault(l => l.GroupID == groupId).Longitude.Value.ToString():"",
                latitude = u.UsersLocations.FirstOrDefault(l => l.GroupID == groupId).Latitude.HasValue?
                u.UsersLocations?.FirstOrDefault(l => l.GroupID == groupId).Latitude.Value.ToString():"",
                outOfRange = Utilities.IsOutOfRange(users,u.ID,groupId, maxDistance),
                active = u.UsersGroups.FirstOrDefault().Status,
                tracked = u.UsersGroups.FirstOrDefault().Tracked,
                notify = u.UsersGroups.FirstOrDefault().Notified,

            }).ToList();*/
            return result;
        }
    }
}
