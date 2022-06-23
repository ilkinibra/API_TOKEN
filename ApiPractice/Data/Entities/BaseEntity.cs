using System;

namespace ApiPractice.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime  CreatedTime { get; set; }
    }
}
