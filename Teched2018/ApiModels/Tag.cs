using System;

namespace Teched2018.ApiModels
{
    public class Tag
    {
		public Guid TagId { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; }
    }
}
