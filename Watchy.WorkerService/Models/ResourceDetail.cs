using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchy.WorkerService.Models
{
	public class ResourceDetail
	{
		public int Id { get; set; }
		public DateTime? VerificationDate { get; set; }
		public double CpuUse { get; set; }
		public double RamUse { get; set; }
		public double DiskUse { get; set; }
	}
}
