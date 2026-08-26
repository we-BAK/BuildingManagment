using System;
using System.Collections.Generic;
using BMS.Models;

namespace BMS.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalBuildings { get; set; }
        public int TotalRooms { get; set; }
        public int TotalShops { get; set; }
        public int ActiveRentalsCount { get; set; }
        public int PendingRequestsCount { get; set; }
        public decimal OccupancyRate { get; set; }
        public decimal TotalMonthlyRevenue { get; set; }

        public List<RoomRentalRequest> PendingRoomRequests { get; set; } = new List<RoomRentalRequest>();
        public List<ShopRequest> PendingShopRequests { get; set; } = new List<ShopRequest>();
        public List<RoomRental> UpcomingExpirations { get; set; } = new List<RoomRental>();
        public List<Notification> RecentNotifications { get; set; } = new List<Notification>();
        public List<Building> RecentBuildings { get; set; } = new List<Building>();
    }
}
