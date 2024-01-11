using Microsoft.AspNetCore.Mvc;
using ProjectFinal.NET.Data;

namespace ProjectFinal.NET.NewProComponent
{
    public class NewProVM : ViewComponent
    {
        private readonly DotnetContext db;

        public NewProVM (DotnetContext context) => db = context;

    }
}
