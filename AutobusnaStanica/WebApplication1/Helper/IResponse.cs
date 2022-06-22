using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public interface IResponse
    {
        /// <summary>
        /// The response status
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Indicates if the response can update
        /// </summary>
        bool CanUpdate { get; }

        /// <summary>
        /// Updates the response asynchronously 
        /// </summary>
        /// <returns>An awaitable task</returns>
        Task UpdateAsync();
    }
}
