using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CodingTracker.Controllers;

internal class BaseController
{
    private readonly string _connectionString;

    public BaseController(string connectionString)
    {
        _connectionString = connectionString;
    }
}
