using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IDataFormatService
    {
        Task<object> ParseHL7(string input);
        Task<object> FormatJsonAsync(string input, string mode);
    }
}
