using UnityEngine;

namespace Domain.MessagesDTO
{
    public class BuildingCreatedDTO
    {
        public int BuildingId { get; set; }
        public Vector2Int Position { get; set; }
    }
}