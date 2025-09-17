using Domain.Enums;
using Domain.Models.Buildings;
using UnityEngine;

namespace Domain.MessagesDTO
{
    public class PlaceBuildingDTO
    {
        public BuildingType BuildingType { get; set; }
        public Vector2Int Position { get; set; }
    }
}