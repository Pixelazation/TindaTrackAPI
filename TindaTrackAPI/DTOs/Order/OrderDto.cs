﻿using TindaTrackAPI.DTOs.Purchase;
using TindaTrackAPI.Models;

namespace TindaTrackAPI.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
    }
}
