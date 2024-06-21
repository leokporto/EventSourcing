using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Core.Application
{
	public interface IServiceResponse
	{
		bool Success { get; }
		string Message { get; }
	}
}
